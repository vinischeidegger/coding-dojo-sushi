using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test.services
{
    public class MenuSplitStrategyServiceTest
    {
        private readonly MenuSplitStrategyService menuSplitStrategy;

        public MenuSplitStrategyServiceTest()
        {
            this.menuSplitStrategy = new MenuSplitStrategyService();
        }

        private decimal SumPrice(BasePlate[] plates)
        {
            return 0m;
        }

        [Fact]
        public void SplitStrategyWithNullValuesTest()
        {
            //Arrange
            BasePlate[] menuList = null;
            decimal menuPrice = 0m;
            bool expecteMenuCalculation = false;
            BasePlate[] emptyArray = new BasePlate[]{ };

            //Act
            MenuCalculationResult menuCalculationResult = this.menuSplitStrategy.ExtractOptimalMenu(menuList, this.SumPrice, menuPrice);

            //Assert
            Assert.NotNull(menuCalculationResult);
            Assert.Equal(expecteMenuCalculation, menuCalculationResult.CalculateAsMenu);
            Assert.Equal(emptyArray, menuCalculationResult.MenuPlates);
            Assert.Equal(emptyArray, menuCalculationResult.RemainingPlates);
        }

        [Fact]
        public void SplitStrategyWithNullValuesTest2()
        {
            //Arrange
            BasePlate[] menuList = null;
            decimal menuPrice = 0m;
            bool expecteMenuCalculation = false;
            BasePlate[] emptyArray = new BasePlate[] { };

            //Act
            MenuCalculationResult menuCalculationResult = this.menuSplitStrategy.ExtractOptimalMenu(menuList, this.SumPrice, menuPrice);

            //Assert
            Assert.NotNull(menuCalculationResult);
            Assert.Equal(expecteMenuCalculation, menuCalculationResult.CalculateAsMenu);
            Assert.Equal(emptyArray, menuCalculationResult.MenuPlates);
            Assert.Equal(emptyArray, menuCalculationResult.RemainingPlates);
        }
    }
}
