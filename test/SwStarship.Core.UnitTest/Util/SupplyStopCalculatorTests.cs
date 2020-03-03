using Xunit;
using FluentAssertions;
using SwStarship.Core.Util;

namespace SwStarship.Core.UnitTest.Util
{
    public class SupplyStopCalculatorTests
    {

        [Theory]
        [InlineData(1000000, 1920, 7, 74)] 
        [InlineData(1000000, 1800, 60, 9)]
        [InlineData(1000000, 480, 180, 11)]
        [InlineData(1000000, 0, 50, null)]
        [InlineData(1000000, 50, 0, null)]
        [InlineData(1000000, 0, 0, null)]
        public void ShouldCalculateTheNumberOfStopsRequired(int distance, int mgltDailyDistance, int daysOfSupplyDuration, int? expectedResult)
        {
            SupplyStopCalculator calculator = new SupplyStopCalculator();
            int? result = calculator.CalculateNumberOfStops(distance, mgltDailyDistance, daysOfSupplyDuration);

            result.Should().Be(expectedResult);
        }

    }
}
