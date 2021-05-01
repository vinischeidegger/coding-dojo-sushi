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
        public void CalculateTotalPriceTest()
        {
            // Arrange
            BillCalculatorService calculatorService = new BillCalculatorService();

            Plate[] plates = null;

            // Act
            calculatorService.CalculateTotalPrice();

            // Assert
        }
    }
}
