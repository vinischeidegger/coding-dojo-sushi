using Restaurant.BillCalculator.Domain.Model;
using System.Linq;

namespace Restaurant.BillCalculator.Application.Services
{
    public static class PlateExtensionMethods
    {
        /// <summary>
        /// Analyze whether the collection could be considered for Menu Pricing Strategy
        /// </summary>
        /// <param name="plates"></param>
        /// <returns></returns>
        public static bool ShouldBeConsiderForMenu(this BasePlate[] plates)
        {
            bool containsSoup = plates.FirstOrDefault(plate => plate is SoupPlate) != null;
            bool fivePlates = plates.Length >= 5;
            bool redOrBlue = plates.FirstOrDefault(plate => plate.IsOfColor(Color.Red) || plate.IsOfColor(Color.Blue)) != null;

            return (containsSoup || (fivePlates && redOrBlue));
        }

        public static bool IsOfColor(this BasePlate plate, Color color)
        {
            if (plate is SushiPlate)
            {
                SushiPlate sushiPlate = (SushiPlate)plate;
                return sushiPlate.Color.Equals(color);
            }
            return false;
        }
    }
}
