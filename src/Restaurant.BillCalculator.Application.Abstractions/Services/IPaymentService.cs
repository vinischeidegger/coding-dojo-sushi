using Restaurant.BillCalculator.Domain.Model;
using System;

namespace Restaurant.BillCalculator.Application.Services
{
    public interface IPaymentService
    {
        decimal PayBill(BasePlate[] plates = null);
        decimal PayBill(DateTime paymentTime, BasePlate[] plates = null);
    }
}