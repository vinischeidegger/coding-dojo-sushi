using Restaurant.BillCalculator.Domain.Model;
using Restaurant.Domain.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BillCalculator.Application.Services
{
    public class BillPaymentService
    {
        private readonly ICalculationStrategySelectorService calculationStrategySelector;

        public BillPaymentService(ICalculationStrategySelectorService calculationStrategySelector)
        {
            this.calculationStrategySelector = calculationStrategySelector;
        }

        public decimal PayBill(BasePlate[] plates = null)
        {
            CalculationStrategy calculationStrategy = this.calculationStrategySelector.GetCalculationStrategy(DateTime.Now, plates);
            return 0m;
        }
    }
}
