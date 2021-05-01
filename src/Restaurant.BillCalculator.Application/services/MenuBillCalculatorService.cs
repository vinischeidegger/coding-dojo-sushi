using Restaurant.BillCalculator.Application.model;
using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.BillCalculator.Application.Services
{
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

            BasePlate soupPlate = plates.FirstOrDefault(plate => plate is SoupPlate);
            if (soupPlate != null)
            {
                MenuCalculationResult calculationResult = ExtractPartialMenu(soupPlate, plates);
                if (calculationResult.CalculateAsMenu)
                {

                }
                return 10.40m;
            }
            else
            {
                return this.SumPrice(plates);
            }
        }

        private MenuCalculationResult ExtractPartialMenu(BasePlate soup, BasePlate[] plates)
        {
            List<BasePlate> originalList = new List<BasePlate>(plates);

            List<BasePlate> basePlatesList = new List<BasePlate>(plates);
            basePlatesList.RemoveAll(plate => plate is SoupPlate);
            // Optimize price plate ordering by most expensive
            basePlatesList.Sort((x, y) => x.Price.CompareTo(y.Price));
            originalList.Remove(soup);

            bool calculateAsMenu = false;

            BasePlate[] menuPlates = basePlatesList.GetRange(0, 4).ToArray();
            BasePlate[] remainingItems = null;
            MenuCalculationResult calculationResult = new MenuCalculationResult()
            {
                CalculateAsMenu = calculateAsMenu,
                MenuPlates = menuPlates,
                RemainingPlates = remainingItems
            };
            return calculationResult;
        }

        private decimal SumPrice(BasePlate[] plates)
        {
            return plates.Select(plate => { plate.Price = this.platePriceService.GetPlatePrice(plate); return plate.Price; }).Sum();
        }
    }
}
