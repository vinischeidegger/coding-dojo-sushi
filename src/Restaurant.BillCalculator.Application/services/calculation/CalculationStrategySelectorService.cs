using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.BillCalculator.Application.Services
{
    public class CalculationStrategySelectorService : ICalculationStrategySelectorService
    {
        private readonly TimeSpan menuStartTime;
        private readonly TimeSpan menuEndTime;

        public CalculationStrategySelectorService()
        {
            this.menuStartTime = new TimeSpan(11, 0, 0); //10 o'clock
            this.menuEndTime = new TimeSpan(17, 0, 0); //12 o'clock

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

            TimeSpan timeOfDay = paymentDateTime.TimeOfDay;

            //within menu hours?
            if (timeOfDay >= menuStartTime && timeOfDay < menuEndTime)
                //hasSoup = true && other 4 plates?
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
