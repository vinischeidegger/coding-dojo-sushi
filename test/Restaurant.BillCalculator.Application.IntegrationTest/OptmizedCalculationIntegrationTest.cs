using Moq;
using Restaurant.BillCalculator.Application.Data;
using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using Restaurant.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Restaurant.BillCalculator.Application.IntegrationTest
{
    public class OptmizedCalculationIntegrationTest
    {
        private protected readonly static SushiPlate greyPlate = new SushiPlate(Color.Grey);
        private protected readonly static SushiPlate greenPlate = new SushiPlate(Color.Green);
        private protected readonly static SushiPlate yellowPlate = new SushiPlate(Color.Yellow);
        private protected readonly static SushiPlate redPlate = new SushiPlate(Color.Red);
        private protected readonly static SushiPlate bluePlate = new SushiPlate(Color.Blue);
        private protected readonly static SoupPlate soupPlate = new SoupPlate();

        private readonly CalculationStrategySelectorService calculationStrategySelector;
        private readonly PaymentService paymentService;
        private readonly OrderService orderService;
        private readonly InMemoryPriceRepository priceRepository;
        private readonly RegularCalculationService regularBillCalculator;
        private readonly MenuCalculationService menuBillCalculator;
        private readonly InMemoryOrderRepository orderRepository;
        private Mock<IClock> clockMock;

        public OptmizedCalculationIntegrationTest()
        {
            this.priceRepository = new InMemoryPriceRepository();
            this.regularBillCalculator = new RegularCalculationService(priceRepository);
            this.menuBillCalculator = new MenuCalculationService(priceRepository, new MenuSplitStrategyService());
            this.calculationStrategySelector = new CalculationStrategySelectorService(this.regularBillCalculator, this.menuBillCalculator);
            this.clockMock = new Mock<IClock>();
            this.paymentService = new PaymentService(this.calculationStrategySelector, clockMock.Object);
            this.orderRepository = new InMemoryOrderRepository();
            this.orderService = new OrderService(this.paymentService, this.orderRepository, this.priceRepository, clockMock.Object);
        }

        [Fact]
        public void optmizedBillIntegrationTest()
        {
            //Arrange
            Order order1 = new Order()
            {
                Person = "Person A",
                Plates = new BasePlate[] { greyPlate, greyPlate }
            };
            Order order2 = new Order()
            {
                Person = "Person A",
                Plates = new BasePlate[] { soupPlate, greenPlate, greenPlate, redPlate, redPlate, bluePlate }
            };
            Order order3 = new Order()
            {
                Person = "Person B",
                Plates = new BasePlate[] { greyPlate, greyPlate, greenPlate, greenPlate, yellowPlate, yellowPlate, redPlate, redPlate }
            };
            Order order4 = new Order()
            {
                Person = "Person B",
                Plates = new BasePlate[] { soupPlate, soupPlate, greyPlate, greyPlate, greenPlate, greenPlate, yellowPlate, yellowPlate, yellowPlate, redPlate, redPlate }
            };
            List<Order> mockOrders = new List<Order> { order1, order2, order3, order4 };
            this.clockMock.Setup(clk => clk.Now).Returns(new DateTime(2021, 5, 5, 13, 45, 0));
            int expectedPersonCount = 2;
            decimal personAExpectedValue = 13.35m;
            decimal personBExpectedValue = 15.35m;
            decimal personCExpectedValue = 18.95m;

            //Act
            mockOrders.ForEach(o => this.orderService.AddOrder(o));
            OptimizedBill optimizedBill = this.orderService.PayAllOrders();
            int personCount = optimizedBill.PersonalPrice.Count;
            PersonalPrice personABill = optimizedBill.PersonalPrice["Person A"];
            PersonalPrice personBBill = optimizedBill.PersonalPrice["Person B"];

            //Assert
            Assert.Equal(expectedPersonCount, personCount);
            Assert.Equal(8, personABill.Plates.ToList().Count);
            Assert.Equal(personAExpectedValue, personABill.Total);
            Assert.Equal(personBExpectedValue, personBBill.Total);
        }
    }
}
