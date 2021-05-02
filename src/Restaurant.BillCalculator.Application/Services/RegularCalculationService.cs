using Restaurant.BillCalculator.Application.Data;
using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Linq;

namespace Restaurant.BillCalculator.Application.Services
{
    /// <summary>
    /// This class represent the Regular Bill Calculation Service Strategy.
    /// It uses a price service to get prices and then calculate the Total price by multiplying price times quantity
    /// </summary>
    public class RegularCalculationService : IRegularCalculationService
    {
        private readonly IPriceRepository platePriceService;

        public RegularCalculationService(IPriceRepository platePriceService)
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
