using Moq;
using Restaurant.BillCalculator.Application.Data;
using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

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
        private readonly PaymentService billPaymentService;
        private readonly InMemoryPriceRepository platePriceService;
        private readonly RegularBillCalculatorService regularBillCalculator;
        private readonly MenuBillCalculatorService menuBillCalculator;

        private Mock<IClock> clockMock;

        public OptmizedCalculationIntegrationTest()
        {
            this.platePriceService = new InMemoryPriceRepository();
            this.regularBillCalculator = new RegularBillCalculatorService(platePriceService);
            this.menuBillCalculator = new MenuBillCalculatorService(platePriceService, new MenuSplitStrategyService());
            this.calculationStrategySelector = new CalculationStrategySelectorService(this.regularBillCalculator, this.menuBillCalculator);
            this.clockMock = new Mock<IClock>();
            this.billPaymentService = new PaymentService(this.calculationStrategySelector, clockMock.Object);
        }


    }
}
