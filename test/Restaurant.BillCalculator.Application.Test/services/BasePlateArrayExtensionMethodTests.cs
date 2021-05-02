using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test.services
{
    public class BasePlateArrayExtensionMethodTests
    {
        [Fact]
        public void Test()
        {
            BasePlate[] plates = new BasePlate[] { };
            plates.ShouldBeConsiderForMenu();
        }

    }
}
