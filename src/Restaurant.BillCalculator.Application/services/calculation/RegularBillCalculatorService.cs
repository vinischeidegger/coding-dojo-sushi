using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Linq;

namespace Restaurant.BillCalculator.Application.Services
{
    public class RegularBillCalculatorService : IBillCalculatorService
    {
        private readonly IPlatePriceService platePriceService;

        public RegularBillCalculatorService(IPlatePriceService platePriceService)
        {
            this.platePriceService = platePriceService;
        }

        public decimal CalculateTotalPrice(SushiPlate[] sushiPlates = default)
        {
            if (sushiPlates == null) return 0m;
            return sushiPlates.Select(plate => { plate.Price = this.platePriceService.GetPlatePrice(plate); return plate.Price; }).Sum();
        }
    }
}
