using System;

namespace Restaurant.BillCalculator.Application.Services
{
    public class SystemClock: IClock
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}