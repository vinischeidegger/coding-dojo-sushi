using Moq;
using Restaurant.BillCalculator.Application.Data;
using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Restaurant.BillCalculator.Application.IntegrationTest
{
    public class PaymentIntegrationTest
    {
        private protected readonly static SushiPlate greyPlate = new SushiPlate(Color.Grey);
        private protected readonly static SushiPlate greenPlate = new SushiPlate(Color.Green);
        private protected readonly static SushiPlate yellowPlate = new SushiPlate(Color.Yellow);
        private protected readonly static SushiPlate redPlate = new SushiPlate(Color.Red);
        private protected readonly static SushiPlate bluePlate = new SushiPlate(Color.Blue);
        private protected readonly static SoupPlate soupPlate = new SoupPlate();

        private readonly CalculationStrategySelectorService calculationStrategySelector;
        private readonly PaymentService paymentService;
        private readonly InMemoryPriceRepository platePriceService;
        private readonly RegularCalculationService regularBillCalculator;
        private readonly MenuCalculationService menuBillCalculator;

        private Mock<IClock> clockMock;

        public PaymentIntegrationTest()
        {
            this.platePriceService = new InMemoryPriceRepository();
            this.regularBillCalculator = new RegularCalculationService(platePriceService);
            this.menuBillCalculator = new MenuCalculationService(platePriceService, new MenuSplitStrategyService());
            this.calculationStrategySelector = new CalculationStrategySelectorService(this.regularBillCalculator, this.menuBillCalculator);
            this.clockMock = new Mock<IClock>();
            this.paymentService = new PaymentService(this.calculationStrategySelector, clockMock.Object);
        }

        [Fact]
        public void UserStory2Integration_NoPlates_Test()
        {
            // Arrange
            decimal expectedValue = 0m;

            //Act
            decimal paidValue = this.paymentService.PayBill();

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
            decimal paidValue = this.paymentService.PayBill(plates.ToArray());

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
            decimal total = this.paymentService.PayBill(plates.ToArray());

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
            decimal total = this.paymentService.PayBill(plates.ToArray());

            // Assert
            Assert.Equal(expectedTotal, total);
        }

        [Fact]
        public void UserStory3Integration_Example1_Test()
        {
            // Arrange
            int greyPlateQuantity = 2;
            IEnumerable<SushiPlate> greyPlates = Enumerable.Repeat(greyPlate, greyPlateQuantity);
            int greenPlateQuantity = 2;
            IEnumerable<SushiPlate> greenPlates = Enumerable.Repeat(greenPlate, greenPlateQuantity);
            int bluePlateQuantity = 1;
            IEnumerable<SushiPlate> bluePlates = Enumerable.Repeat(bluePlate, bluePlateQuantity);
            List<BasePlate> plates = new List<BasePlate> { };
            plates.AddRange(greyPlates);
            plates.AddRange(greenPlates);
            plates.AddRange(bluePlates);
            this.clockMock.Setup(clk => clk.Now).Returns(new DateTime(2021, 5, 3, 11, 0, 0));
            decimal expectedValue = 8.50m;

            // Act
            decimal paidValue = this.paymentService.PayBill(plates.ToArray());

            // Assert
            Assert.Equal(expectedValue, paidValue);

        }

        [Fact]
        public void UserStory3Integration_Example2_Test()
        {
            // Arrange
            int greyPlateQuantity = 3;
            IEnumerable<SushiPlate> greyPlates = Enumerable.Repeat(greyPlate, greyPlateQuantity);
            int greenPlateQuantity = 2;
            IEnumerable<SushiPlate> greenPlates = Enumerable.Repeat(greenPlate, greenPlateQuantity);
            List<BasePlate> plates = new List<BasePlate> { };
            plates.AddRange(greyPlates);
            plates.AddRange(greenPlates);
            this.clockMock.Setup(clk => clk.Now).Returns(new DateTime(2021, 5, 3, 11, 0, 0));
            decimal expectedValue = (3 * 4.95m) + (2 * 3.95m);

            // Act
            decimal paidValue = this.paymentService.PayBill(plates.ToArray());

            // Assert
            Assert.Equal(expectedValue, paidValue);

        }

        [Fact]
        public void UserStory4_Example1_PersonA_Test()
        {
            // Arrange
            int greyPlateQuantity = 2;
            IEnumerable<SushiPlate> greyPlates = Enumerable.Repeat(greyPlate, greyPlateQuantity);
            int greenPlateQuantity = 2;
            IEnumerable<SushiPlate> greenPlates = Enumerable.Repeat(greenPlate, greenPlateQuantity);
            int redPlateQuantity = 2;
            IEnumerable<SushiPlate> redPlates = Enumerable.Repeat(redPlate, redPlateQuantity);
            int bluePlateQuantity = 1;
            IEnumerable<SushiPlate> bluePlates = Enumerable.Repeat(bluePlate, bluePlateQuantity);
            List<BasePlate> plates = new List<BasePlate> { soupPlate };
            plates.AddRange(greyPlates);
            plates.AddRange(greenPlates);
            plates.AddRange(redPlates);
            plates.AddRange(bluePlates);
            this.clockMock.Setup(clk => clk.Now).Returns(new DateTime(2021, 5, 5, 13, 45, 0));
            decimal expectedValue = 13.35m;

            // Act
            decimal paidValue = this.paymentService.PayBill(plates.ToArray());

            // Assert
            Assert.Equal(expectedValue, paidValue);

        }

        [Fact]
        public void UserStory4_Example1_PersonB_Test()
        {
            // Arrange
            int greyPlateQuantity = 2;
            IEnumerable<SushiPlate> greyPlates = Enumerable.Repeat(greyPlate, greyPlateQuantity);
            int greenPlateQuantity = 2;
            IEnumerable<SushiPlate> greenPlates = Enumerable.Repeat(greenPlate, greenPlateQuantity);
            int yellowPlateQuantity = 2;
            IEnumerable<SushiPlate> yellowPlates = Enumerable.Repeat(yellowPlate, yellowPlateQuantity);
            int redPlateQuantity = 2;
            IEnumerable<SushiPlate> redPlates = Enumerable.Repeat(redPlate, redPlateQuantity);
            List<BasePlate> plates = new List<BasePlate> { };
            plates.AddRange(greyPlates);
            plates.AddRange(greenPlates);
            plates.AddRange(redPlates);
            plates.AddRange(yellowPlates);
            this.clockMock.Setup(clk => clk.Now).Returns(new DateTime(2021, 5, 5, 13, 45, 0));
            //decimal expectedValue = 15.35m;
            decimal expectedValue = 16.35m;

            // Act
            decimal paidValue = this.paymentService.PayBill(plates.ToArray());

            // Assert
            Assert.Equal(expectedValue, paidValue);

        }

        [Fact]
        public void UserStory4_Example1_PersonC_Test()
        {
            // Arrange
            int greyPlateQuantity = 2;
            IEnumerable<SushiPlate> greyPlates = Enumerable.Repeat(greyPlate, greyPlateQuantity);
            int greenPlateQuantity = 2;
            IEnumerable<SushiPlate> greenPlates = Enumerable.Repeat(greenPlate, greenPlateQuantity);
            int yellowPlateQuantity = 3;
            IEnumerable<SushiPlate> yellowPlates = Enumerable.Repeat(yellowPlate, yellowPlateQuantity);
            int redPlateQuantity = 2;
            IEnumerable<SushiPlate> redPlates = Enumerable.Repeat(redPlate, redPlateQuantity);
            List<BasePlate> plates = new List<BasePlate> { soupPlate, soupPlate };
            plates.AddRange(greyPlates);
            plates.AddRange(greenPlates);
            plates.AddRange(yellowPlates);
            plates.AddRange(redPlates);
            this.clockMock.Setup(clk => clk.Now).Returns(new DateTime(2021, 5, 5, 13, 45, 0));
            decimal expectedValue = 18.95m;

            // Act
            decimal paidValue = this.paymentService.PayBill(plates.ToArray());

            // Assert
            Assert.Equal(expectedValue, paidValue);

        }


    }

}
