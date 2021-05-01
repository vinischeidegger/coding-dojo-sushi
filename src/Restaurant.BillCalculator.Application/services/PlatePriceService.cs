using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BillCalculator.Application.Services
{
    public class PlatePriceService
    {
        public decimal GetPlatePrice(Plate plate)
        {
            switch (plate.Color)
            {
                case Color.Grey:
                    return 4.95m;
                case Color.Green:
                    return 3.95m;
                case Color.Yellow:
                    return 2.95m;
                case Color.Red:
                    return 1.95m;
                case Color.Blue:
                    return 0.95m;
                default:
                    throw new ArgumentOutOfRangeException("Plate Coler does not exist");
            }
        }
    }
}
