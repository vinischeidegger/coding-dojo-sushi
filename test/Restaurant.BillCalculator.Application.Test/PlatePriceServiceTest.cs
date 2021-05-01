using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test
{
    public class PlatePriceServiceTest
    {
        private readonly static Plate greyPlate = new Plate(Color.Grey);
        private readonly static Plate greenPlate = new Plate(Color.Green);
        private readonly static Plate yellowPlate = new Plate(Color.Yellow);
        private readonly static Plate redPlate = new Plate(Color.Red);
        private readonly static Plate bluePlate = new Plate(Color.Blue);

        /// <summary>
        /// Method to test plate prices
        /// </summary>
        [Fact]
        public void CalculatePlatePriceTest()
        {
            // Arrange
            PlatePriceService calculatorService = new PlatePriceService();
            decimal expectedGreyPrice = 4.95m;
            decimal expectedGreenPrice = 3.95m;
            decimal expectedYellowPrice = 2.95m;
            decimal expectedRedPrice = 1.95m;
            decimal expectedBluePrice = 0.95m;

            // Act
            decimal greyPlatePrice = calculatorService.GetPlatePrice(greyPlate);
            decimal greenPlatePrice = calculatorService.GetPlatePrice(greenPlate);
            decimal yellowPlatePrice = calculatorService.GetPlatePrice(yellowPlate);
            decimal redPlatePrice = calculatorService.GetPlatePrice(redPlate);
            decimal bluePlatePrice = calculatorService.GetPlatePrice(bluePlate);

            // Assert
            Assert.Equal(expectedGreyPrice, greyPlatePrice);
            Assert.Equal(expectedGreenPrice, greenPlatePrice);
            Assert.Equal(expectedYellowPrice, yellowPlatePrice);
            Assert.Equal(expectedRedPrice, redPlatePrice);
            Assert.Equal(expectedBluePrice, bluePlatePrice);
        }

    }
}
