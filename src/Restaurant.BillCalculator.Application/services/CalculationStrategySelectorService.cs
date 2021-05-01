using Restaurant.BillCalculator.Domain.Model;
using Restaurant.Domain.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.BillCalculator.Application.Services
{
    /// <summary>
    /// This class is the calculation strategy selector service. It contains the rules used to define the strategy used to calculate the bill value.
    /// </summary>
    public class CalculationStrategySelectorService : ICalculationStrategySelectorService
    {
        private readonly TimeSpan menuStartTime;
        private readonly TimeSpan menuEndTime;
        private readonly DayOfWeek menuStartDay;
        private readonly DayOfWeek menuEndDay;

        private readonly Dictionary<CalculationStrategy, IBillCalculatorService> strategySelector;

        public CalculationStrategySelectorService(IRegularBillCalculatorService regularBillCalculator, IMenuBillCalculatorService menuBillCalculator)
        {
            this.menuStartTime = new TimeSpan(11, 0, 0); //10 o'clock
            this.menuEndTime = new TimeSpan(17, 0, 0); //12 o'clock
            this.menuStartDay = DayOfWeek.Monday;
            this.menuEndDay = DayOfWeek.Friday;
            this.strategySelector = new Dictionary<CalculationStrategy, IBillCalculatorService> 
            {
                { CalculationStrategy.RegularStrategy, regularBillCalculator },
                { CalculationStrategy.MenuStrategy, menuBillCalculator}
            };
        }

        /// <summary>
        /// Returns one of the Calculation Strategies based on the time of the payment and selected plates.
        /// </summary>
        /// <param name="paymentDateTime"></param>
        /// <param name="plates"></param>
        /// <returns></returns>
        public CalculationStrategy GetCalculationStrategy(DateTime paymentDateTime, BasePlate[] plates = null)
        {
            return this.GetStrategyOnPaymentTimeForPlates(paymentDateTime, plates);
        }

        /// <summary>
        /// Returns the instance of the IBillCalculatorService to be used for the calculation of the bill
        /// </summary>
        /// <param name="paymentDateTime"></param>
        /// <param name="plates"></param>
        /// <returns></returns>
        public IBillCalculatorService GetBillCalculatorStrategy(CalculationStrategy calculationStrategy)
        {
            return this.strategySelector[calculationStrategy];
        }

        private CalculationStrategy GetStrategyOnPaymentTimeForPlates(DateTime paymentDateTime, BasePlate[] plates)
        {
            if (plates == null) return CalculationStrategy.RegularStrategy;

            TimeSpan timeOfDay = paymentDateTime.TimeOfDay;
            DayOfWeek dayOfWeek = paymentDateTime.DayOfWeek;

            //within menu hours?
            if (dayOfWeek >= menuStartDay && dayOfWeek <= menuEndDay)
                //within menu hours?
                if (timeOfDay >= menuStartTime && timeOfDay < menuEndTime)
                    //hasSoup = true && other 4 plates?
                    if (plates.FirstOrDefault(plate => plate is SoupPlate) != null && plates.Length > 4)
                        return CalculationStrategy.MenuStrategy;

            return CalculationStrategy.RegularStrategy;
        }
    }

}
