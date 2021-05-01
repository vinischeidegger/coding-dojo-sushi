using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BillCalculator.Application.Services
{
    public class PlatePriceService : IPlatePriceService
    {
        private const string NULL_PLATE_EXCEPTION = "The plate cannot be null";
        private const string INVALID_ENUM_VALUE_EXCEPTION = "Plate Coler does not exist";

        public decimal GetPlatePrice(SushiPlate plate)
        {
            if (plate == null)  throw new ArgumentNullException(nameof(plate));

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
                    throw new ArgumentOutOfRangeException(INVALID_ENUM_VALUE_EXCEPTION);
            }
        }
    }
}
