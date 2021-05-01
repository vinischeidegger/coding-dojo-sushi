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

        public BillPaymentServiceTests()
        {
            this.billPaymentService = new BillPaymentService();
        }

        [Fact]
        public void MenuShouldBeUsedWhenThereIsSoupAndTimeIsWithinRange_Test()
        {
            Domain.Model.BasePlate[] plates = null;
            billPaymentService.PayBill(plates);
        }
    }
}
