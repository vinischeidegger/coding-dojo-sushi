using Restaurant.BillCalculator.Domain.Model;

namespace Restaurant.BillCalculator.Application.Test
{
    public class PlateTestBase
    {
        private protected readonly static Plate greyPlate = new Plate(Color.Grey);
        private protected readonly static Plate greenPlate = new Plate(Color.Green);
        private protected readonly static Plate yellowPlate = new Plate(Color.Yellow);
        private protected readonly static Plate redPlate = new Plate(Color.Red);
        private protected readonly static Plate bluePlate = new Plate(Color.Blue);
    }
}