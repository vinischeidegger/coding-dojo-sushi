using Restaurant.BillCalculator.Domain.Model;
using System.Linq;

namespace Restaurant.BillCalculator.Application.Services
{
    public static class BasePlateArrayMenuExtensionMethods
    {
        public static bool ShouldBeConsiderForMenu(this BasePlate[] plates)
        {
            bool containsSoup = plates.FirstOrDefault(plate => plate is SoupPlate) != null;
            bool fivePlates = plates.Length >= 5;
            bool redOrBlue = plates.FirstOrDefault(plate => {
                if (plate is SushiPlate)
                {
                    SushiPlate sushiPlate = (SushiPlate)plate;
                    return sushiPlate.Color.Equals(Color.Red) || sushiPlate.Color.Equals(Color.Blue);
                }
                return false;
            }) != null;

            return (containsSoup || (fivePlates && redOrBlue));
        }
    }
}
