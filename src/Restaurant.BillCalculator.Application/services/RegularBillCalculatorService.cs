using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Linq;

namespace Restaurant.BillCalculator.Application.Services
{
    public class RegularBillCalculatorService : IRegularBillCalculatorService
    {
        private readonly IPlatePriceService platePriceService;

        public RegularBillCalculatorService(IPlatePriceService platePriceService)
        {
            this.platePriceService = platePriceService;
        }

        public decimal CalculateTotalPrice(BasePlate[] plates = default)
        {
            if (plates == null) return 0m;
            return plates.Select(plate => { plate.Price = this.platePriceService.GetPlatePrice(plate); return plate.Price; }).Sum();
        }
    }
}
