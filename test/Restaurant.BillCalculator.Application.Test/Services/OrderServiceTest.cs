using Moq;
using Restaurant.BillCalculator.Application.Data;
using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using Restaurant.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test.Services
{
    public class OrderServiceTest : PlateTestBase
    {
        private readonly OrderService orderService;
        private Mock<IOrderRepository> orderRepositoryMock;
        private Mock<IPriceRepository> priceRepositoryMock;
        private readonly Mock<IPaymentService> paymentService;

        public OrderServiceTest()
        {
            this.orderRepositoryMock = new Mock<IOrderRepository>();
            this.priceRepositoryMock = new Mock<IPriceRepository>();
            this.paymentService = new Mock<IPaymentService>();
            this.orderService = new OrderService(paymentService.Object, orderRepositoryMock.Object, priceRepositoryMock.Object);
        }

        [Fact]
        public void CheckRepositoryIsCalledTest()
        {
            //Arrange
            Order order = new Order() {
                Person = "Person A",
                Plates = new BasePlate[] { }
            };

            //Act
            this.orderService.AddOrder( order );

            //Assert
            this.orderRepositoryMock.Verify(rep => rep.AddOrder(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void CheckOptimizedBillPersonQuantityTest()
        {
            //Arrange
            Order order1 = new Order()
            {
                Person = "Person A",
                Plates = new BasePlate[] { greyPlate }
            };
            Order order2 = new Order()
            {
                Person = "Person A",
                Plates = new BasePlate[] { greyPlate }
            };
            Order order3 = new Order()
            {
                Person = "Person B",
                Plates = new BasePlate[] { greyPlate }
            };
            IEnumerable<Order> mockOrders = new List<Order> { order1, order2, order3 };
            this.orderRepositoryMock.Setup(repo => repo.GetAllOrders()).Returns(mockOrders);
            this.priceRepositoryMock.Setup(price => price.GetPlatePrice(greyPlate)).Returns(4.95m);

            //Act
            OptimizedBill optimizedBill = this.orderService.PayAllOrders();

            //Assert
            Assert.Equal(2, optimizedBill.PersonalPrice.Count);
            
        }

        [Fact]
        public void CheckPersonOrderIsSummedTest()
        {
            //Arrange
            Order order1 = new Order()
            {
                Person = "Person A",
                Plates = new BasePlate[] { greyPlate, greyPlate }
            };
            Order order2 = new Order()
            {
                Person = "Person A",
                Plates = new BasePlate[] { soupPlate, greenPlate, greenPlate }
            };
            Order order3 = new Order()
            {
                Person = "Person B",
                Plates = new BasePlate[] { greyPlate }
            };
            IEnumerable<Order> mockOrders = new List<Order> { order1, order2, order3 };
            this.orderRepositoryMock.Setup(repo => repo.GetAllOrders()).Returns(mockOrders);
            this.priceRepositoryMock.Setup(price => price.GetPlatePrice(greyPlate)).Returns(4.95m);

            //Act
            OptimizedBill optimizedBill = this.orderService.PayAllOrders();

            //Assert
            Assert.Equal(2, optimizedBill.PersonalPrice.Count);
            Assert.Equal(5, optimizedBill.PersonalPrice["Person A"].Plates.ToList().Count);

        }

    }
}
