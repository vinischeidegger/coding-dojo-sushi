using Restaurant.BillCalculator.Domain.Model;
using System;

namespace Restaurant.BillCalculator.Application.Services
{
    public interface ICalculationStrategySelectorService
    {
        CalculationStrategy GetCalculationStrategy(DateTime paymentDateTime, BasePlate[] plates);
    }
}