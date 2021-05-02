using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BillCalculator.Application.Data
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly IList<Order> orders;

        public InMemoryOrderRepository()
        {
            this.orders = new List<Order>();
        }

        public void AddOrder(Order order)
        {
            this.orders.Add(order);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return this.orders;
        }
    }
}
