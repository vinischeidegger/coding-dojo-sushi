using Restaurant.BillCalculator.Domain.Model;
using System;

namespace Restaurant.BillCalculator.Application.Services
{
    public interface IMenuSplitStrategyService
    {
        MenuCalculationResult ExtractOptimalMenu(BasePlate[] plates, Func<BasePlate[], decimal> regularPriceCalculation, decimal menuPrice);
    }
}