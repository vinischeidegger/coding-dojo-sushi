using Restaurant.BillCalculator.Domain.Model;
using Restaurant.Domain.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BillCalculator.Application.Services
{
    /// <summary>
    /// This class repesents the Payment Service
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private readonly ICalculationStrategySelectorService calculationStrategySelector;
        private readonly IClock clock;

        public PaymentService(ICalculationStrategySelectorService calculationStrategySelector, IClock clock)
        {
            this.calculationStrategySelector = calculationStrategySelector;
            this.clock = clock;
        }

        public decimal PayBill(BasePlate[] plates = null)
        {
            return this.PayBill(this.clock.Now, plates);
        }

        public decimal PayBill(DateTime paymentTime, BasePlate[] plates = null)
        {
            CalculationStrategy calculationStrategy = this.calculationStrategySelector.GetCalculationStrategy(this.clock.Now, plates);
            ICalculationService billCalculatorService = this.calculationStrategySelector.GetBillCalculatorStrategy(calculationStrategy);
            return billCalculatorService.CalculateTotalPrice(plates);
        }
    }
}
