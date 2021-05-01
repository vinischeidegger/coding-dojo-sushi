using Restaurant.BillCalculator.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test.services
{
    public class CalculationStrategySelectorServiceTests
    {
        private readonly CalculationStrategySelectorService strategySelector;

        public CalculationStrategySelectorServiceTests()
        {
            this.strategySelector = new CalculationStrategySelectorService();
        }

        [Fact]
        public void MenuShouldBeUsedWhenThereIsSoupAndTimeIsWithinRange_Test()
        {
            // Arrange


            // Act
            this.strategySelector.GetCalculationStrategy(DateTime.Now, null);

            //Assert
        }

    }
}
