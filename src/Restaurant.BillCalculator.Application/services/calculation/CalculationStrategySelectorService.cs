using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.BillCalculator.Application.Services
{
    public class CalculationStrategySelectorService : ICalculationStrategySelectorService
    {
        public CalculationStrategySelectorService()
        {
        }

        public CalculationStrategy GetCalculationStrategy(DateTime paymentDateTime, BasePlate[] plates)
        {
            return CalculationStrategy.MenuStrategy;
        }
    }

    public enum CalculationStrategy
    {
        RegularStrategy,
        MenuStrategy
    }
}
