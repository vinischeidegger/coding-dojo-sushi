using Restaurant.BillCalculator.Domain.Model;
using Restaurant.Domain.model;
using System;

namespace Restaurant.BillCalculator.Application.Services
{
    public interface ICalculationStrategySelectorService
    {
        ICalculationService GetBillCalculatorStrategy(CalculationStrategy calculationStrategy);
        CalculationStrategy GetCalculationStrategy(DateTime paymentDateTime, BasePlate[] plates);
    }
}