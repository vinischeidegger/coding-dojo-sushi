using Moq;
using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test.services
{
    public class CalculationStrategySelectorServiceTests : PlateTestBase
    {
        private readonly CalculationStrategySelectorService strategySelector;
        private readonly Mock<IRegularBillCalculatorService> regularCalculatorMock;
        private readonly Mock<IMenuBillCalculatorService> menuCalculatorMock;

        public CalculationStrategySelectorServiceTests()
        {
            this.regularCalculatorMock = new Mock<IRegularBillCalculatorService>();
            this.menuCalculatorMock = new Mock<IMenuBillCalculatorService>();
            this.strategySelector = new CalculationStrategySelectorService(this.regularCalculatorMock.Object, this.menuCalculatorMock.Object);
        }

        [Fact]
        public void MenuShouldBeUsedWhenThereIsSoupAndTimeIsWithinRange_AtStart_Test()
        {
            // Arrange
            DateTime elevenAm = new DateTime(2021, 5, 1, 11, 0, 0);
            BasePlate[] plates = new BasePlate[] { soupPlate, greyPlate, greyPlate, greenPlate, greenPlate, bluePlate };
            CalculationStrategy expectedCalculationStrategy = CalculationStrategy.MenuStrategy;

            // Act
            CalculationStrategy chosenCalculationStrategy = this.strategySelector.GetCalculationStrategy(elevenAm, plates);

            //Assert
            Assert.Equal(expectedCalculationStrategy, chosenCalculationStrategy);
        }

        [Fact]
        public void MenuShouldBeUsedWhenThereIsSoupAndTimeIsWithinRange_JustBeforeEnd_Test()
        {
            // Arrange
            DateTime elevenAm = new DateTime(2021, 5, 1, 16, 59, 59);
            BasePlate[] plates = new BasePlate[] { soupPlate, greyPlate, greyPlate, greenPlate, greenPlate, bluePlate };
            CalculationStrategy expectedCalculationStrategy = CalculationStrategy.MenuStrategy;

            // Act
            CalculationStrategy chosenCalculationStrategy = this.strategySelector.GetCalculationStrategy(elevenAm, plates);

            //Assert
            Assert.Equal(expectedCalculationStrategy, chosenCalculationStrategy);
        }

        [Fact]
        public void MenuShouldNotBeUsedWhenThereIsNoSoupAndTimeIsWithinRange_MoreThan4Plates_Test()
        {
            // Arrange
            DateTime elevenAm = new DateTime(2021, 5, 1, 11, 0, 0);
            BasePlate[] plates = new BasePlate[] { greyPlate, greyPlate, greyPlate, greenPlate, greenPlate, bluePlate };
            CalculationStrategy expectedCalculationStrategy = CalculationStrategy.RegularStrategy;

            // Act
            CalculationStrategy chosenCalculationStrategy = this.strategySelector.GetCalculationStrategy(elevenAm, plates);

            //Assert
            Assert.Equal(expectedCalculationStrategy, chosenCalculationStrategy);
        }

        [Fact]
        public void MenuShouldNotBeUsedWhenThereIsNoSoupAndTimeIsWithinRange_LessThan4Plates_Test()
        {
            // Arrange
            DateTime elevenAm = new DateTime(2021, 5, 1, 11, 0, 0);
            BasePlate[] plates = new BasePlate[] { greyPlate, greyPlate, greyPlate };
            CalculationStrategy expectedCalculationStrategy = CalculationStrategy.RegularStrategy;

            // Act
            CalculationStrategy chosenCalculationStrategy = this.strategySelector.GetCalculationStrategy(elevenAm, plates);

            //Assert
            Assert.Equal(expectedCalculationStrategy, chosenCalculationStrategy);
        }

        [Fact]
        public void MenuShouldNotBeUsedWhenThereIsSoupAndTimeIsNotWithinRange_JustBeforeStart_Test()
        {
            // Arrange
            DateTime elevenAm = new DateTime(2021, 5, 1, 10, 59, 59);
            BasePlate[] plates = new BasePlate[] { soupPlate, greyPlate, greyPlate, greenPlate, greenPlate, bluePlate };
            CalculationStrategy expectedCalculationStrategy = CalculationStrategy.RegularStrategy;

            // Act
            CalculationStrategy chosenCalculationStrategy = this.strategySelector.GetCalculationStrategy(elevenAm, plates);

            //Assert
            Assert.Equal(expectedCalculationStrategy, chosenCalculationStrategy);
        }

        [Fact]
        public void MenuShouldNotBeUsedWhenThereIsSoupAndTimeIsNotWithinRange_JustAfterEnd_Test()
        {
            // Arrange
            DateTime elevenAm = new DateTime(2021, 5, 1, 17, 0, 0);
            BasePlate[] plates = new BasePlate[] { soupPlate, greyPlate, greyPlate, greenPlate, greenPlate, bluePlate };
            CalculationStrategy expectedCalculationStrategy = CalculationStrategy.RegularStrategy;

            // Act
            CalculationStrategy chosenCalculationStrategy = this.strategySelector.GetCalculationStrategy(elevenAm, plates);

            //Assert
            Assert.Equal(expectedCalculationStrategy, chosenCalculationStrategy);
        }

        [Fact]
        public void CalculationStrategyShouldReturnCorrectCalculator()
        {
            //Arrange
            CalculationStrategy menuStrategy  = CalculationStrategy.MenuStrategy;
            CalculationStrategy regularStrategy = CalculationStrategy.RegularStrategy;

            //Act
            IBillCalculatorService menuBillCalculatorService = this.strategySelector.GetBillCalculatorStrategy(menuStrategy);
            IBillCalculatorService regularBillCalculatorService = this.strategySelector.GetBillCalculatorStrategy(regularStrategy);
            bool isMenuType = menuBillCalculatorService is IMenuBillCalculatorService;
            bool isRegularType = regularBillCalculatorService is IRegularBillCalculatorService;

            //Assert
            Assert.True(isMenuType);
            Assert.True(isRegularType);
        }
    }
}
