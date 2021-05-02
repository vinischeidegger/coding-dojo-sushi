using Moq;
using Restaurant.BillCalculator.Application.Data;
using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test.Services
{
    public class RegularBillCalculatorServiceTests : PlateTestBase
    {
        private const decimal GREY_PLATE_PRICE = 4.95m;
        private const decimal GREEN_PLATE_PRICE = 3.95m;
        private const decimal YELLOW_PLATE_PRICE = 2.95m;
        private const decimal RED_PLATE_PRICE = 1.95m;
        private const decimal BLUE_PLATE_PRICE = 0.95m;

        private readonly RegularBillCalculatorService calculatorService;
        private readonly Mock<IPriceRepository> platePriceServiceMock;

        public RegularBillCalculatorServiceTests()
        {
            this.platePriceServiceMock = new Mock<IPriceRepository>();
            this.platePriceServiceMock.Setup(svc => svc.GetPlatePrice(greyPlate)).Returns(GREY_PLATE_PRICE);
            this.platePriceServiceMock.Setup(svc => svc.GetPlatePrice(greenPlate)).Returns(GREEN_PLATE_PRICE);
            this.platePriceServiceMock.Setup(svc => svc.GetPlatePrice(yellowPlate)).Returns(YELLOW_PLATE_PRICE);
            this.platePriceServiceMock.Setup(svc => svc.GetPlatePrice(redPlate)).Returns(RED_PLATE_PRICE);
            this.platePriceServiceMock.Setup(svc => svc.GetPlatePrice(bluePlate)).Returns(BLUE_PLATE_PRICE);
            this.calculatorService = new RegularBillCalculatorService(this.platePriceServiceMock.Object);
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
            SushiPlate[] plates = null;

            // Act
            decimal total = calculatorService.CalculateTotalPrice(plates);

            // Assert
            Assert.Equal(0, total);
        }

        /// <summary>
        /// Method to test bill calculation with Single Input
        /// </summary>
        [Fact]
        public void PriceServiceShouldCalculateCorrectValuesForSinglePlate_Test()
        {
            // Arrange
            SushiPlate[] plates = new SushiPlate[] {greyPlate};

            // Act
            decimal total = calculatorService.CalculateTotalPrice(plates);

            // Assert
            Assert.Equal(GREY_PLATE_PRICE, total);
        }

        /// <summary>
        /// Method to test bill calculation with Multiple Input
        /// </summary>
        [Fact]
        public void PriceServiceShouldCalculateCorrectValuesForMultiplePlates_Test()
        {
            // Arrange
            SushiPlate[] plates = new SushiPlate[] { greyPlate, greenPlate };
            decimal expectedTotal = GREY_PLATE_PRICE + GREEN_PLATE_PRICE;

            // Act
            decimal total = calculatorService.CalculateTotalPrice(plates);

            // Assert
            Assert.Equal(expectedTotal, total);
        }

        /// <summary>
        /// Method to test bill calculation as per User Story 1 - Example 1
        /// </summary>
        [Fact]
        public void PriceServiceShouldCalculateCorrectValuesAsReqExample1_UsingLiteral_Test()
        {
            // Arrange
            int bluePlateQuantity = 5;
            IEnumerable<SushiPlate> bluePlates = Enumerable.Repeat(bluePlate, bluePlateQuantity);
            SushiPlate[] plates = bluePlates.ToArray();
            decimal expectedTotal = 4.75m;// BLUE_PLATE_PRICE * bluePlateQuantity;

            // Act
            decimal total = calculatorService.CalculateTotalPrice(plates);

            // Assert
            Assert.Equal(expectedTotal, total);
        }

        /// <summary>
        /// Method to test bill calculation as per User Story 1 - Example 2
        /// </summary>
        [Fact]
        public void PriceServiceShouldCalculateCorrectValuesAsReqExample2_UsingLiteral_Test()
        {
            // Arrange
            int greyPlateQuantity = 5;
            IEnumerable<SushiPlate> greyPlates = Enumerable.Repeat(greyPlate, greyPlateQuantity);
            SushiPlate[] plates = greyPlates.ToArray();
            decimal expectedTotal = 24.75m;// GREY_PLATE_PRICE * greyPlateQuantity;

            // Act
            decimal total = calculatorService.CalculateTotalPrice(plates);

            // Assert
            Assert.Equal(expectedTotal, total);
        }

        /// <summary>
        /// Method to test bill calculation as per User Story 1 - Example 3
        /// </summary>
        [Fact]
        public void PriceServiceShouldCalculateCorrectValuesAsReqExample3_UsingLiteral_Test()
        {
            // Arrange
            SushiPlate[] plates = new SushiPlate[] { greyPlate, greenPlate, yellowPlate, redPlate, bluePlate };
            decimal expectedTotal = 14.75m;// GREY_PLATE_PRICE * greyPlateQuantity;

            // Act
            decimal total = calculatorService.CalculateTotalPrice(plates);

            // Assert
            Assert.Equal(expectedTotal, total);
        }

    }
}
