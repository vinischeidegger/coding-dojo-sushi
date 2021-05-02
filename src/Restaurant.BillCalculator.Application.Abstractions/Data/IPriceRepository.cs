using Restaurant.BillCalculator.Domain.Model;

namespace Restaurant.BillCalculator.Application.Data
{
    public interface IPriceRepository
    {
        decimal GetPlatePrice(BasePlate plate);

        decimal GetMenuPrice();
    }
}