using FluentAssertions;
using Xunit;
using SwStarship.Core.Domain.Models;

namespace SwStarship.Core.UnitTest.Models
{
    public class ConsumableTests
    {
        [Theory]
        [InlineData("3 years", true)]
        [InlineData("1 week", true)]
        [InlineData("10 years", true)]
        [InlineData("7 days", true)]
        [InlineData("randomString", false)]
        [InlineData("1 life", false)]
        public void ShouldValidateConsumableStringFormat(string input, bool expectedResult)
        {
            bool result = Consumable.ValidateInput(input);

            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("3 years", 3 * 365)]
        [InlineData("1 year", 365)]
        [InlineData("0 year", 0)]
        public void ShouldConvertYearsToDays(string consumableInput, int expectedDays)
        {
            var consumable = Consumable.Parse(consumableInput);
            int daysResult = consumable.TimeUnitToDays();

            daysResult.Should().Be(expectedDays);
        }

        [Theory]
        [InlineData("6 Months", 6 * 30)]
        [InlineData("1 Month", 30)]
        [InlineData("0 Month", 0)]
        public void ShouldConvertMonthsToDays(string consumableInput, int expectedDays)
        {
            var consumable = Consumable.Parse(consumableInput);
            int daysResult = consumable.TimeUnitToDays();

            daysResult.Should().Be(expectedDays);
        }

        [Theory]
        [InlineData("10 Weeks", 10 * 7)]
        [InlineData("1 Week", 7)]
        [InlineData("0 week", 0)]
        public void ShouldConvertWeeksToDays(string consumableInput, int expectedDays)
        {
            var consumable = Consumable.Parse(consumableInput);
            int daysResult = consumable.TimeUnitToDays();

            daysResult.Should().Be(expectedDays);
        }

        [Theory]
        [InlineData("10 Days", 10)]
        [InlineData("1 Day", 1)]
        [InlineData("0 day", 0)]
        public void ExpectToKeepSameDayValue(string consumableInput, int expectedDays)
        {
            var consumable = Consumable.Parse(consumableInput);
            int daysResult = consumable.TimeUnitToDays();

            daysResult.Should().Be(expectedDays);
        }
    }
}
