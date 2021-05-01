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

        /// <summary>
        /// Returns one of the Calculation Strategies based on the time of the payment and selected plates.
        /// </summary>
        /// <param name="paymentDateTime"></param>
        /// <param name="plates"></param>
        /// <returns></returns>
        public CalculationStrategy GetCalculationStrategy(DateTime paymentDateTime, BasePlate[] plates = null)
        {
            if (plates == null) return CalculationStrategy.RegularStrategy;

            //hasSoup = true && other 4 plates
            if (plates.FirstOrDefault(plate => plate is SoupPlate) != null && plates.Length > 4)
                return CalculationStrategy.MenuStrategy;

            return CalculationStrategy.RegularStrategy;
        }
    }

    public enum CalculationStrategy
    {
        RegularStrategy,
        MenuStrategy
    }
}
