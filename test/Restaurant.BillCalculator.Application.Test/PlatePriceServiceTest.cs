using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test
{
    public class PlatePriceServiceTest
    {
        private readonly static Plate greyPlate = new Plate(Color.Grey);

        /// <summary>
        /// Method to test plate prices
        /// </summary>
        [Fact]
        public void CalculateGreyPlatePriceTest()
        {
            // Arrange
            PlatePriceService calculatorService = new PlatePriceService();
            decimal expectedGreyPrice = 4.95m;

            // Act
            decimal greyPlatePrice = calculatorService.GetPlatePrice(greyPlate);

            // Assert
            Assert.Equal(expectedGreyPrice, greyPlatePrice);
        }

    }
}
