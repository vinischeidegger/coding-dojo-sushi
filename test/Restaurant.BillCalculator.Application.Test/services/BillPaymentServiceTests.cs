using Moq;
using Restaurant.BillCalculator.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test
{
    public class BillPaymentServiceTests
    {
        private readonly BillPaymentService billPaymentService;
        private readonly Mock<ICalculationStrategySelectorService> strategySelectorMock;
        private readonly RegularBillCalculatorService regularStrategy;

        public BillPaymentServiceTests()
        {
            this.regularStrategy = new RegularBillCalculatorService(new PlatePriceService());
            this.strategySelectorMock = new Mock<ICalculationStrategySelectorService>();
            this.billPaymentService = new BillPaymentService(strategySelectorMock.Object);
        }

        [Fact]
        public void MenuShouldBeUsedWhenThereIsSoupAndTimeIsWithinRange_Test()
        {
            // Arrange
            /*strategySelectorMock.Setup(strategySelector => strategySelector.GetCalculationStrategy()).Returns(this.regularStrategy);
            Domain.Model.BasePlate[] plates = null;

            // Act
            decimal paidValue = billPaymentService.PayBill(plates);*/

            //Assert
        }
    }
}
