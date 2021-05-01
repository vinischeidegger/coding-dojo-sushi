using Restaurant.BillCalculator.Domain.Model;
using System;

namespace Restaurant.BillCalculator.Application.Services
{
    public class BillCalculatorService
    {

        public decimal CalculateTotalPrice(Plate[] plates = default)
        {
            if (plates == null) return 0m;
            throw new NotImplementedException("Method not implemented");
        }
    }
}
