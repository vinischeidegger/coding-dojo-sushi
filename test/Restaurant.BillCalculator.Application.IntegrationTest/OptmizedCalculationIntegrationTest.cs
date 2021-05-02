using Moq;
using Restaurant.BillCalculator.Application.Data;
using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
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
            this.orderService = new OrderService(this.paymentService, this.orderRepository, this.priceRepository);
        }

        [Fact]
        public void Test()
        {

        }
    }
}
