using Restaurant.BillCalculator.Domain.Model;

namespace Restaurant.BillCalculator.Application.Services
{
    public interface IBillCalculatorService
    {
        decimal CalculateTotalPrice(SushiPlate[] sushiPlates = null);
    }
}