using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BillCalculator.Application.Services
{
    public class MenuSplitStrategyService : IMenuSplitStrategyService
    {
        public MenuCalculationResult ExtractOptimalMenu(BasePlate soupPlate, BasePlate[] plates, Func<BasePlate[], decimal> regularPriceCalculation, decimal menuPrice)
        {
            bool calculateAsMenu = false;
            List<BasePlate> basePlatesList = new List<BasePlate>(plates);
            List<BasePlate> menuPlatesList;
            BasePlate[] menuPlates;
            if (soupPlate != null)
            {
                //Soup menu
                basePlatesList.RemoveAll(plate => plate is SoupPlate);

                // Optimize price plate ordering by most expensive
                basePlatesList.Sort((x, y) => y.Price.CompareTo(x.Price));
                int maxCount = basePlatesList.Count > 4 ? 4 : basePlatesList.Count;
                menuPlatesList = basePlatesList.GetRange(0, maxCount);
                menuPlatesList.Add(soupPlate);
                menuPlates = menuPlatesList.ToArray();
                decimal originalPrice = regularPriceCalculation(menuPlates);
                calculateAsMenu = originalPrice > menuPrice;
            }
            else
            {
                //Five plates menu
                basePlatesList.Sort((x, y) => y.Price.CompareTo(x.Price));
                menuPlatesList = basePlatesList.GetRange(0, 5);
                menuPlates = menuPlatesList.ToArray();
                decimal originalPrice = regularPriceCalculation(menuPlates);
                calculateAsMenu = originalPrice > menuPrice;
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
    }
}
