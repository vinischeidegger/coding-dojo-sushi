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

            this.PopulatePrices(plates);

            BasePlate soupPlate = plates.FirstOrDefault(plate => plate is SoupPlate);
            bool containsSoup = soupPlate != null;
            if (containsSoup)
            {
                MenuCalculationResult calculationResult = ExtractPartialMenu(soupPlate, plates);
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

        private MenuCalculationResult ExtractPartialMenu(BasePlate soup, BasePlate[] plates)
        {

            List<BasePlate> basePlatesList = new List<BasePlate>(plates);
            basePlatesList.RemoveAll(plate => plate is SoupPlate);
            // Optimize price plate ordering by most expensive
            

            bool calculateAsMenu = false;

            basePlatesList.Sort((x, y) => y.Price.CompareTo(x.Price));
            int maxCount = basePlatesList.Count > 4 ? 4 : basePlatesList.Count;
            List<BasePlate> menuPlatesList = basePlatesList.GetRange(0, maxCount);
            menuPlatesList.Add(soup);
            BasePlate[] menuPlates = menuPlatesList.ToArray();
            decimal originalPrice = this.SumPrice(menuPlates) + soup.Price;
            calculateAsMenu = originalPrice > this.menuPrice;
            BasePlate[] remainingItems = null;

            if (calculateAsMenu)
            {
                List<BasePlate> workingList = new List<BasePlate>(plates);
                menuPlatesList.ForEach(plate => workingList.Remove(plate));
                remainingItems = workingList.ToArray();
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
