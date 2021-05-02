using System.Collections.Generic;

namespace Restaurant.BillCalculator.Domain.Model

{
    public class Order
    {
        public string Person { get; set; }

        public IEnumerable<BasePlate> Plates { get; set; }
    }
}