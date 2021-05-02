using Restaurant.BillCalculator.Domain.Model;
using System.Collections.Generic;

namespace Restaurant.BillCalculator.Application.Data
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        IEnumerable<Order> GetAllOrders();
    }
}