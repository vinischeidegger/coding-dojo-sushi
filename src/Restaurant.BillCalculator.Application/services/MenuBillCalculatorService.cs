using Restaurant.BillCalculator.Application.model;
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
        private IPlatePriceService platePriceService;
        private decimal menuPrice;

        public MenuBillCalculatorService(IPlatePriceService platePriceService)
        {
            this.platePriceService = platePriceService;
            if (platePriceService != null)
                this.menuPrice = platePriceService.GetMenuPrice();
        }

        public decimal CalculateTotalPrice(BasePlate[] plates = null)
        {
            if (plates == null) return 0m;

            this.PopulatePrices(plates);

            BasePlate soupPlate = plates.FirstOrDefault(plate => plate is SoupPlate);
            bool shouldCheckMenuPrices = this.AnalyzeWhetherShouldCheckMenu(soupPlate, plates);
            if(shouldCheckMenuPrices)
            {
                MenuCalculationResult calculationResult = this.ExtractPartialMenu(soupPlate, plates);
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

        private bool AnalyzeWhetherShouldCheckMenu(BasePlate soupPlate, BasePlate[] plates)
        {
            bool containsSoup = soupPlate != null;
            bool fivePlates = plates.Length >= 5;
            bool redOrBlue = plates.FirstOrDefault(plate => {
                if (plate is SushiPlate)
                {
                    SushiPlate sushiPlate = (SushiPlate)plate;
                    return sushiPlate.Color.Equals(Color.Red) || sushiPlate.Color.Equals(Color.Blue);
                }
                return false;
            }) != null;
            return ((containsSoup || fivePlates) && redOrBlue);
        }

        private MenuCalculationResult ExtractPartialMenu(BasePlate soup, BasePlate[] plates)
        {
            bool calculateAsMenu = false;
            List<BasePlate> basePlatesList = new List<BasePlate>(plates);
            List<BasePlate> menuPlatesList;
            BasePlate[] menuPlates;
            if (soup != null)
            {
                //Soup menu
                basePlatesList.RemoveAll(plate => plate is SoupPlate);

                // Optimize price plate ordering by most expensive
                basePlatesList.Sort((x, y) => y.Price.CompareTo(x.Price));
                int maxCount = basePlatesList.Count > 4 ? 4 : basePlatesList.Count;
                menuPlatesList = basePlatesList.GetRange(0, maxCount);
                menuPlatesList.Add(soup);
                menuPlates = menuPlatesList.ToArray();
                decimal originalPrice = this.SumPrice(menuPlates);
                calculateAsMenu = originalPrice > this.menuPrice;
            }
            else
            {
                //Five plates menu
                basePlatesList.Sort((x, y) => y.Price.CompareTo(x.Price));
                menuPlatesList = basePlatesList.GetRange(0, 5);
                menuPlates = menuPlatesList.ToArray();
                decimal originalPrice = this.SumPrice(menuPlates);
                calculateAsMenu = originalPrice > this.menuPrice;
            }

            BasePlate[] remainingItems = null;
            if (calculateAsMenu)
            {
                List<BasePlate> fullListToRemoveMenu = new List<BasePlate>(plates);
                menuPlatesList.ForEach(plate => fullListToRemoveMenu.Remove(plate));
                remainingItems = fullListToRemoveMenu.ToArray();
            }
            else
            {
                menuPlates = new BasePlate[] { };
                remainingItems = plates;
            }

            return new MenuCalculationResult()
            {
                CalculateAsMenu = calculateAsMenu,
                MenuPlates = menuPlates,
                RemainingPlates = remainingItems
            };
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
