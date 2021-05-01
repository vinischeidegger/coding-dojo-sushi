using System;

namespace Restaurant.BillCalculator.Application.Services
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}