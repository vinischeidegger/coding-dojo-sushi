using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BillCalculator.Application.model
{
    internal class MenuCalculationResult
    {
        internal bool CalculateAsMenu { get; set; }
        internal BasePlate[] MenuPlates { get; set; }
        internal BasePlate[] RemainingPlates { get; set; }
    }
}
