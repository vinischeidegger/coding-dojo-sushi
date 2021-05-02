using Restaurant.BillCalculator.Application.Data;
using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Reflection;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test.Services
{
    public class PlatePriceServiceTests : PlateTestBase
    {
        InMemoryPriceRepository calculatorService;

        public PlatePriceServiceTests()
        {
            this.calculatorService = new InMemoryPriceRepository();
        }

        /// <summary>
        /// Method to test plate prices
        /// </summary>
        [Fact]
        public void PriceServiceShouldReturnCorrectValues_Test()
        {
            // Arrange
            decimal expectedGreyPrice = 4.95m;
            decimal expectedGreenPrice = 3.95m;
            decimal expectedYellowPrice = 2.95m;
            decimal expectedRedPrice = 1.95m;
            decimal expectedBluePrice = 0.95m;

            // Act
            decimal greyPlatePrice = this.calculatorService.GetPlatePrice(greyPlate);
            decimal greenPlatePrice = this.calculatorService.GetPlatePrice(greenPlate);
            decimal yellowPlatePrice = this.calculatorService.GetPlatePrice(yellowPlate);
            decimal redPlatePrice = this.calculatorService.GetPlatePrice(redPlate);
            decimal bluePlatePrice = this.calculatorService.GetPlatePrice(bluePlate);

            // Assert
            Assert.Equal(expectedGreyPrice, greyPlatePrice);
            Assert.Equal(expectedGreenPrice, greenPlatePrice);
            Assert.Equal(expectedYellowPrice, yellowPlatePrice);
            Assert.Equal(expectedRedPrice, redPlatePrice);
            Assert.Equal(expectedBluePrice, bluePlatePrice);
        }

        /// <summary>
        /// Method to test null plate exception
        /// </summary>
        [Fact]
        public void PriceServiceShouldThrowExceptionWhenParamIsNull_Test()
        {
            // Arrange
            MethodInfo methodInfo = calculatorService.GetType().GetMethod("GetPlatePrice");
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            ParameterInfo parameterInfo = parameterInfos[0];
            string plateParamName = parameterInfo.Name;
            SushiPlate plate = null;

            // Act
            Action nullPlateMethodCall = () => this.calculatorService.GetPlatePrice(plate);

            // Assert
            ArgumentNullException argumentNullException = Assert.Throws<ArgumentNullException>(nullPlateMethodCall);
            Assert.Contains(plateParamName, argumentNullException.Message);
        }
    }
}
