using Moq;
using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using Restaurant.Domain.model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test.Services
{
    public class CalculationStrategySelectorServiceTests : PlateTestBase
    {
        private readonly CalculationStrategySelectorService strategySelector;
        private readonly Mock<IRegularCalculationService> regularCalculatorMock;
        private readonly Mock<IMenuCalculationService> menuCalculatorMock;

        public CalculationStrategySelectorServiceTests()
        {
            this.regularCalculatorMock = new Mock<IRegularCalculationService>();
            this.menuCalculatorMock = new Mock<IMenuCalculationService>();
            this.strategySelector = new CalculationStrategySelectorService(this.regularCalculatorMock.Object, this.menuCalculatorMock.Object);
        }

        [Fact]
        public void MenuShouldBeUsedWhenThereIsSoupAndTimeIsWithinRange_Monday_AtStart_Test()
        {
            // Arrange
            DateTime elevenAm = new DateTime(2021, 5, 3, 11, 0, 0);
            BasePlate[] plates = new BasePlate[] { soupPlate, greyPlate, greyPlate, greenPlate, greenPlate, bluePlate };
            CalculationStrategy expectedCalculationStrategy = CalculationStrategy.MenuStrategy;

            // Act
            CalculationStrategy chosenCalculationStrategy = this.strategySelector.GetCalculationStrategy(elevenAm, plates);

            //Assert
            Assert.Equal(expectedCalculationStrategy, chosenCalculationStrategy);
        }

        [Fact]
        public void MenuShouldBeUsedWhenThereIsSoupAndTimeIsWithinRange_Monday_JustBeforeEnd_Test()
        {
            // Arrange
            DateTime elevenAm = new DateTime(2021, 5, 3, 16, 59, 59); // Monday
            BasePlate[] plates = new BasePlate[] { soupPlate, greyPlate, greyPlate, greenPlate, greenPlate, bluePlate };
            CalculationStrategy expectedCalculationStrategy = CalculationStrategy.MenuStrategy;

            // Act
            CalculationStrategy chosenCalculationStrategy = this.strategySelector.GetCalculationStrategy(elevenAm, plates);

            //Assert
            Assert.Equal(expectedCalculationStrategy, chosenCalculationStrategy);
        }

        [Fact]
        public void MenuShouldBeUsedWhenThereIsSoupAndTimeIsNotWithinRange_Saturday_AtStart_Test()
        {
            // Arrange
            DateTime elevenAm = new DateTime(2021, 5, 1, 11, 0, 0);
            BasePlate[] plates = new BasePlate[] { soupPlate, greyPlate, greyPlate, greenPlate, greenPlate, bluePlate };
            CalculationStrategy expectedCalculationStrategy = CalculationStrategy.RegularStrategy;

            // Act
            CalculationStrategy chosenCalculationStrategy = this.strategySelector.GetCalculationStrategy(elevenAm, plates);

            //Assert
            Assert.Equal(expectedCalculationStrategy, chosenCalculationStrategy);
        }

        [Fact]
        public void MenuShouldBeUsedWhenThereIsSoupAndTimeIsNotWithinRange_Saturday_JustBeforeEnd_Test()
        {
            // Arrange
            DateTime elevenAm = new DateTime(2021, 5, 1, 16, 59, 59);
            BasePlate[] plates = new BasePlate[] { soupPlate, greyPlate, greyPlate, greenPlate, greenPlate, bluePlate };
            CalculationStrategy expectedCalculationStrategy = CalculationStrategy.RegularStrategy;

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
            ICalculationService menuBillCalculatorService = this.strategySelector.GetBillCalculatorStrategy(menuStrategy);
            ICalculationService regularBillCalculatorService = this.strategySelector.GetBillCalculatorStrategy(regularStrategy);
            bool isMenuType = menuBillCalculatorService is IMenuCalculationService;
            bool isRegularType = regularBillCalculatorService is IRegularCalculationService;

            //Assert
            Assert.True(isMenuType);
            Assert.True(isRegularType);
        }
    }
}
