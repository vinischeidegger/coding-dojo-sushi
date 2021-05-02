using Restaurant.BillCalculator.Domain.Model;
using System.Collections.Generic;

namespace Restaurant.Domain.Model
{
    public class PersonalPrice
    {
        public string Person { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<BasePlate> Plates { get; set; }
        public decimal Total { get; set; }
    }
}