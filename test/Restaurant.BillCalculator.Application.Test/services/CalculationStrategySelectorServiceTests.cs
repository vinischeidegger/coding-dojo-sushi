﻿using Restaurant.BillCalculator.Application.Services;
using Restaurant.BillCalculator.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Restaurant.BillCalculator.Application.Test.services
{
    public class CalculationStrategySelectorServiceTests: PlateTestBase
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
            DateTime elevenAm = new DateTime(2021, 5, 1, 11, 0,0 );
            BasePlate[] plates = new BasePlate[] { soupPlate };
            CalculationStrategy expectedCalculationStrategy = CalculationStrategy.MenuStrategy;

            // Act
            CalculationStrategy chosenCalculationStrategy = this.strategySelector.GetCalculationStrategy(elevenAm, null);

            //Assert
            Assert.Equal(expectedCalculationStrategy, chosenCalculationStrategy);
        }

    }
}
