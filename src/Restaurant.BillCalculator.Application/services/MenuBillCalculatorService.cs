using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            BasePlate soupPlate = plates.FirstOrDefault(plate => plate is SoupPlate);
            bool shouldCheckMenuPrices = this.AnalyzeWhetherShouldCheckMenu(plates);
            if(shouldCheckMenuPrices)
            {
                MenuCalculationResult calculationResult = this.splitStrategyService.ExtractOptimalMenu(plates, this.SumPrice, this.menuPrice);
                if (calculationResult.CalculateAsMenu)
                {
                    return this.menuPrice + this.CalculateTotalPrice(calculationResult.RemainingPlates);
                }
                return this.SumPrice(calculationResult.RemainingPlates);
            }
            else
            {
                return this.SumPrice(plates);
            }
        }

        private bool AnalyzeWhetherShouldCheckMenu(BasePlate[] plates)
        {
            bool containsSoup = plates.FirstOrDefault(plate => plate is SoupPlate) != null;
            bool fivePlates = plates.Length >= 5;
            bool redOrBlue = plates.FirstOrDefault(plate => {
                if (plate is SushiPlate)
                {
                    SushiPlate sushiPlate = (SushiPlate)plate;
                    return sushiPlate.Color.Equals(Color.Red) || sushiPlate.Color.Equals(Color.Blue);
                }
                return false;
            }) != null;
            return (containsSoup || (fivePlates && redOrBlue));
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
