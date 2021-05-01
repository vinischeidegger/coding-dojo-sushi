using Moq;
using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using System;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test
{
    public class BillCalculatorServiceTest : PlateTestBase
    {
        private readonly BillCalculatorService calculatorService;
        private readonly Mock<IPlatePriceService> platePriceServiceMock;

        public BillCalculatorServiceTest()
        {
            this.platePriceServiceMock = new Mock<IPlatePriceService>();
            this.calculatorService = new BillCalculatorService(this.platePriceServiceMock.Object);
        }

        /// <summary>
        /// Method to test calculation without parameters
        /// </summary>
        [Fact]
        public void TotalPriceShouldbeZeroWhenNoPlates_Test()
        {
            // Arrange
            decimal zeroValueExpected = 0m;

            // Act
            decimal total = calculatorService.CalculateTotalPrice();

            // Assert
            Assert.Equal(zeroValueExpected, total);
        }

        /// <summary>
        /// Method to test null parameters in bill calculation
        /// </summary>
        [Fact]
        public void TotalPriceShouldbeZeroWhenPlatesIsNull_Test()
        {
            // Arrange
            Plate[] plates = null;

            // Act
            decimal total = calculatorService.CalculateTotalPrice(plates);

            // Assert
            Assert.Equal(0, total);
        }

        /// <summary>
        /// Method to test null parameters in bill calculation
        /// </summary>
        [Fact]
        public void PriceServiceShouldCalculateCorrectValuesForSinglePlate_Test()
        {
            // Arrange
            decimal greyPlatePrice = 4.95m;
            this.platePriceServiceMock.Setup(svc => svc.GetPlatePrice(greyPlate)).Returns(greyPlatePrice);
            Plate[] plates = new Plate[] {greyPlate};

            // Act
            decimal total = calculatorService.CalculateTotalPrice(plates);

            // Assert
            Assert.Equal(greyPlatePrice, total);
        }

    }
}
