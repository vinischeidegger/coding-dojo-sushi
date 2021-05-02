using Restaurant.BillCalculator.Domain.Model;

namespace Restaurant.BillCalculator.Application.Services
{
    public interface ICalculationService
    {
        decimal CalculateTotalPrice(BasePlate[] sushiPlates = null);
    }
}