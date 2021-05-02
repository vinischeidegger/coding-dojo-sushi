using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Linq;

namespace Restaurant.BillCalculator.Application.Services
{
    /// <summary>
    /// This class represents the Menu Calculator Service Strategy. It should be used to calculate meal packages prices.
    /// </summary>
    public class MenuBillCalculatorService : IMenuBillCalculatorService
    {
        private readonly IPlatePriceService platePriceService;
        private readonly IMenuSplitStrategyService splitStrategyService;
        private decimal menuPrice;

        public MenuBillCalculatorService(IPlatePriceService platePriceService, IMenuSplitStrategyService splitStrategyService)
        {
            this.platePriceService = platePriceService;
            this.splitStrategyService = splitStrategyService;
            if (platePriceService != null)
                this.menuPrice = platePriceService.GetMenuPrice();
        }

        public decimal CalculateTotalPrice(BasePlate[] plates = null)
        {
            if (plates == null) return 0m;

            this.PopulatePrices(plates);

            return this.CalculatePrices(plates);
        }

        private decimal CalculatePrices(BasePlate[] plates)
        {
            if (plates == null || plates.Length == 0) return 0m;

            if (plates.ShouldBeConsiderForMenu())
            {
                MenuCalculationResult calculationResult = this.splitStrategyService.ExtractOptimalMenu(plates, this.SumPrice, this.menuPrice);
                if (calculationResult.CalculateAsMenu)
                {
                    return this.menuPrice + this.CalculatePrices(calculationResult.RemainingPlates);
                }
                return this.SumPrice(calculationResult.RemainingPlates);
            }
            else
            {
                return this.SumPrice(plates);
            }
        }

        private void PopulatePrices(BasePlate[] plates)
        {
            Array.ForEach(plates, plate => plate.Price = this.platePriceService.GetPlatePrice(plate) );
        }

        private decimal SumPrice(BasePlate[] plates)
        {
            return plates.Select(plate => plate.Price).Sum();
        }
    }
}
