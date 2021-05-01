using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using System;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test
{
    public class BillCalculatorServiceTest
    {
        /// <summary>
        /// Method to test simple calculation
        /// </summary>
        [Fact]
        public void TotalPriceShouldbeZeroWhenNoPlates_Test()
        {
            // Arrange
            BillCalculatorService calculatorService = new BillCalculatorService();

            // Act
            decimal total = calculatorService.CalculateTotalPrice();

            // Assert
            Assert.Equal(0, total);
        }

        /// <summary>
        /// Method to test simple calculation
        /// </summary>
        [Fact]
        public void TotalPriceShouldbeZeroWhenPlatesIsNull_Test()
        {
            // Arrange
            BillCalculatorService calculatorService = new BillCalculatorService();
            Plate[] plates = null;

            // Act
            decimal total = calculatorService.CalculateTotalPrice(plates);

            // Assert
            Assert.Equal(0, total);
        }
    }
}
