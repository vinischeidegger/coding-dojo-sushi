using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Linq;

namespace Restaurant.BillCalculator.Application.Services
{
    public class BillCalculatorService
    {
        private readonly IPlatePriceService platePriceService;

        public BillCalculatorService(IPlatePriceService platePriceService)
        {
            this.platePriceService = platePriceService;
        }

        public decimal CalculateTotalPrice(Plate[] plates = default)
        {
            if (plates == null) return 0m;
            return plates.Select(plate => { plate.Price = this.platePriceService.GetPlatePrice(plate); return plate.Price; }).Sum();
        }
    }
}
