﻿using Moq;
using Restaurant.BillCalculator.Application.Data;
using Restaurant.BillCalculator.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test.Services
{
    public class BillPaymentServiceTests
    {
        private readonly BillPaymentService billPaymentService;
        private readonly Mock<ICalculationStrategySelectorService> strategySelectorMock;
        private readonly Mock<IClock> clockMock;
        private readonly RegularBillCalculatorService regularStrategy;

        public BillPaymentServiceTests()
        {
            this.regularStrategy = new RegularBillCalculatorService(new InMemoryPriceRepository());
            this.strategySelectorMock = new Mock<ICalculationStrategySelectorService>();
            this.clockMock = new Mock<IClock>();
            this.billPaymentService = new BillPaymentService(strategySelectorMock.Object, this.clockMock.Object);
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
