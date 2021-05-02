using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BillCalculator.Domain.Model
{
    public class MenuCalculationResult
    {
        public bool CalculateAsMenu { get; set; }
        public BasePlate[] MenuPlates { get; set; }
        public BasePlate[] RemainingPlates { get; set; }
    }
}
