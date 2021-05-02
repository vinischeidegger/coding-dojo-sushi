using Restaurant.BillCalculator.Application.Data;
using Restaurant.BillCalculator.Domain.Model;
using Restaurant.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.BillCalculator.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IPriceRepository priceRepository;
        private readonly RegularCalculationService regularBill;

        public OrderService(IOrderRepository orderRepository, IPriceRepository priceRepository)
        {
            this.orderRepository = orderRepository;
            this.priceRepository = priceRepository;
            this.regularBill = new RegularCalculationService(this.priceRepository);

        }

        public void AddOrder(Order order)
        {
            this.orderRepository.AddOrder(order);
        }

        public OptimizedBill PayAllOrders()
        {
            var orders = this.orderRepository.GetAllOrders();
            IEnumerable<Order> pricedOrders = orders.Select(order => {
                    order.Plates = order.Plates.Select(plate => {
                        plate.Price = this.priceRepository.GetPlatePrice(plate);
                        return plate;
                    });
                    return order;
                });

            var result = pricedOrders
                .GroupBy(order => order.Person)
                .ToDictionary(g => g.Key, g => new PersonalPrice () { 
                Person = g.Key,
                Orders = g.ToList(),
                Plates = g.ToList().SelectMany(order => order.Plates)
            });

            foreach(var personPrice in result.Values)
            {
                personPrice.Total = this.regularBill.CalculateTotalPrice(personPrice.Plates.ToArray());
            }

            return new OptimizedBill()
            {
                PersonalPrice = result
            };
        }
    }
}
