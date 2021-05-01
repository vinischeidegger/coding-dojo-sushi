using Restaurant.BillCalculator.Domain.Model;

namespace Restaurant.BillCalculator.Application.Services
{
    public interface IPlatePriceService
    {
        decimal GetPlatePrice(Plate plate);
    }
}