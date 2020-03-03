using FluentAssertions;
using Xunit;
using SwStarship.Core.Domain.Models;
using System;

namespace SwStarship.Core.UnitTest.Models
{
    public class ConsumableTests
    {
        [Theory]
        [InlineData("3 years", true)]
        [InlineData("15 hours", true)]
        [InlineData("1 week", true)]
        [InlineData("10 years", true)]
        [InlineData("7 days", true)]
        [InlineData("unknown", true)]
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
        public void ShouldConvertYearsToDays(string consumableInput, double expectedDays)
        {
            var consumable = Consumable.Parse(consumableInput);
            var daysResult = consumable.ConvertTimeUnitToDays();

            daysResult.Should().Be(expectedDays);
        }

        [Theory]
        [InlineData("6 Months", 6 * 30)]
        [InlineData("1 Month", 30)]
        [InlineData("0 Month", 0)]
        public void ShouldConvertMonthsToDays(string consumableInput, double expectedDays)
        {
            var consumable = Consumable.Parse(consumableInput);
            var daysResult = consumable.ConvertTimeUnitToDays();

            daysResult.Should().Be(expectedDays);
        }

        [Theory]
        [InlineData("10 Weeks", 10 * 7)]
        [InlineData("1 Week", 7)]
        [InlineData("0 week", 0)]
        public void ShouldConvertWeeksToDays(string consumableInput, double expectedDays)
        {
            var consumable = Consumable.Parse(consumableInput);
            var daysResult = consumable.ConvertTimeUnitToDays();

            daysResult.Should().Be(expectedDays);
        }

        [Theory]
        [InlineData("10 Days", 10)]
        [InlineData("1 Day", 1)]
        [InlineData("0 day", 0)]
        public void ExpectToKeepSameDayValue(string consumableInput, double expectedDays)
        {
            var consumable = Consumable.Parse(consumableInput);
            var daysResult = consumable.ConvertTimeUnitToDays();

            daysResult.Should().Be(expectedDays);
        }

        [Theory]
        [InlineData("10 Hours", 10.0 / 24)]
        [InlineData("1 Hour", 1.0 / 24)]
        [InlineData("0 Hour", 0.0 / 24)]
        public void ShouldConvertHoursToDays(string consumableInput, double expectedDays)
        {
            var consumable = Consumable.Parse(consumableInput);
            var daysResult = consumable.ConvertTimeUnitToDays();

            daysResult.Should().Be(expectedDays);
        }

        [Theory]
        [InlineData("unknown", 0)]
        public void ExpectUnkownToBeZero(string consumableInput, double expectedDays)
        {
            var consumable = Consumable.Parse(consumableInput);
            var daysResult = consumable.ConvertTimeUnitToDays();

            daysResult.Should().Be(expectedDays);
        }

        [Theory]
        [InlineData("0 random")]
        [InlineData("-10 noValidUnit")]
        [InlineData("random")]
        public void ExpectValidateAnWrongConsumableInput(string consumableInput)
        {
            Exception ex = Assert.Throws<ArgumentException>(() => Consumable.Parse(consumableInput));
            ex.Message.Should().Be($"Provided consumable {consumableInput} is not valid.");
            ex.Should().BeOfType<ArgumentException>();
        }
    }
}
