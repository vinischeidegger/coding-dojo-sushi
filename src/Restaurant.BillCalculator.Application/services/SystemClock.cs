using System;

namespace Restaurant.BillCalculator.Application.Services
{
    /// <summary>
    /// This is the class to be injected in the payment service in order to use the System Clock.
    /// </summary>
    public class SystemClock: IClock
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}