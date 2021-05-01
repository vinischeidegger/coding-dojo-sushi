using Moq;
using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Restaurant.BillCalculator.Application.IntegrationTest
{
    public class IntegrationTest
    {
        private protected readonly static SushiPlate greyPlate = new SushiPlate(Color.Grey);
        private protected readonly static SushiPlate greenPlate = new SushiPlate(Color.Green);
        private protected readonly static SushiPlate yellowPlate = new SushiPlate(Color.Yellow);
        private protected readonly static SushiPlate redPlate = new SushiPlate(Color.Red);
        private protected readonly static SushiPlate bluePlate = new SushiPlate(Color.Blue);
        private protected readonly static SoupPlate soupPlate = new SoupPlate();

        private readonly CalculationStrategySelectorService calculationStrategySelector;
        private readonly BillPaymentService billPaymentService;
        private readonly PlatePriceService platePriceService;
        private readonly RegularBillCalculatorService regularBillCalculator;
        private readonly MenuBillCalculatorService menuBillCalculator;

        private Mock<IClock> clockMock;

        public IntegrationTest()
        {
            this.platePriceService = new PlatePriceService();
            this.regularBillCalculator = new RegularBillCalculatorService(platePriceService);
            this.menuBillCalculator = new MenuBillCalculatorService(platePriceService);
            this.calculationStrategySelector = new CalculationStrategySelectorService(this.regularBillCalculator, this.menuBillCalculator);
            this.clockMock = new Mock<IClock>();
            this.billPaymentService = new BillPaymentService(this.calculationStrategySelector, clockMock.Object);
        }

        [Fact]
        public void UserStory2Integration_NoPlates_Test()
        {
            // Arrange
            decimal expectedValue = 0m;

            //Act
            decimal paidValue = this.billPaymentService.PayBill();

            //Assert
            Assert.Equal(expectedValue, paidValue);
        }

        [Fact]
        public void UserStory2Integration_Example1_Test()
        {
            // Arrange
            int greyPlateQuantity = 2;
            IEnumerable<SushiPlate> greyPlates = Enumerable.Repeat(greyPlate, greyPlateQuantity);
            int greenPlateQuantity = 2;
            IEnumerable<SushiPlate> greenPlates = Enumerable.Repeat(greenPlate, greenPlateQuantity);
            int bluePlateQuantity = 2;
            IEnumerable<SushiPlate> bluePlates = Enumerable.Repeat(bluePlate, bluePlateQuantity);
            List<BasePlate> plates = new List<BasePlate> { soupPlate };
            plates.AddRange(greyPlates);
            plates.AddRange(greenPlates);
            plates.AddRange(bluePlates);
            this.clockMock.Setup(clk => clk.Now).Returns(new DateTime(2021, 5, 3, 11, 0, 0));
            decimal expectedValue = 10.40m;

            // Act
            decimal paidValue = this.billPaymentService.PayBill(plates.ToArray());

            // Assert
            Assert.Equal(expectedValue, paidValue);

        }

        [Fact]
        public void UserStory2Integration_Example2_Test()
        {
            // Arrange
            int greyPlateQuantity = 2;
            IEnumerable<SushiPlate> greyPlates = Enumerable.Repeat(greyPlate, greyPlateQuantity);
            int greenPlateQuantity = 3;
            IEnumerable<SushiPlate> greenPlates = Enumerable.Repeat(greenPlate, greenPlateQuantity);
            int redPlateQuantity = 2;
            IEnumerable<SushiPlate> redPlates = Enumerable.Repeat(redPlate, redPlateQuantity);
            List<BasePlate> plates = new List<BasePlate> { soupPlate };
            plates.AddRange(greyPlates);
            plates.AddRange(greenPlates);
            plates.AddRange(redPlates);
            this.clockMock.Setup(clk => clk.Now).Returns(new DateTime(2021, 5, 3, 11, 0, 0));
            decimal expectedTotal = 16.35m;

            // Act
            decimal total = this.billPaymentService.PayBill(plates.ToArray());

            // Assert
            Assert.Equal(expectedTotal, total);
        }

        [Fact]
        public void UserStory2Integration_Example3_Test()
        {
            // Arrange
            int greyPlateQuantity = 2;
            IEnumerable<SushiPlate> greyPlates = Enumerable.Repeat(greyPlate, greyPlateQuantity);
            int greenPlateQuantity = 3;
            IEnumerable<SushiPlate> greenPlates = Enumerable.Repeat(greenPlate, greenPlateQuantity);
            int redPlateQuantity = 2;
            IEnumerable<SushiPlate> redPlates = Enumerable.Repeat(redPlate, redPlateQuantity);
            List<BasePlate> plates = new List<BasePlate> { soupPlate };
            plates.AddRange(greyPlates);
            plates.AddRange(greenPlates);
            plates.AddRange(redPlates);
            this.clockMock.Setup(clk => clk.Now).Returns(new DateTime(2021, 5, 1, 11, 0, 0));
            decimal expectedTotal = 28.15m;

            // Act
            decimal total = this.billPaymentService.PayBill(plates.ToArray());

            // Assert
            Assert.Equal(expectedTotal, total);
        }
    }

}
