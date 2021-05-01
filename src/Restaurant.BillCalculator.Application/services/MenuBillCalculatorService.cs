using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BillCalculator.Application.Services
{
    public class MenuBillCalculatorService : IMenuBillCalculatorService
    {
        private IPlatePriceService platePriceService;

        public MenuBillCalculatorService(IPlatePriceService platePriceService)
        {
            this.platePriceService = platePriceService;
        }

        public decimal CalculateTotalPrice(BasePlate[] plates = null)
        {
            if (plates == null) return 0m;

            throw new NotImplementedException();
        }
    }
}
