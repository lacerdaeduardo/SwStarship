using Xunit;
using FluentAssertions;
using SwStarship.Core.Domain.Models;

namespace SwStarship.Core.UnitTest.Models
{
    public class StarshipTests
    {
        [Theory]
        [InlineData("Mocked Starship", "10", 10 * 24)]
        [InlineData("Mocked Starship2", "5", 5 * 24)]
        [InlineData("Mocked Starship3", "33", 33 * 24)]
        [InlineData("Mocked Starship4", "250", 250 * 24)]
        [InlineData("Mocked Starship4", "unknown", 0)]
        public void ExpectGetCorrectDailyMGLT(string name, string mglt, int expectedResult)
        {
            Starship starship = new Starship() { Name = name, MGLT = mglt };

            starship.GetDailyMGLT().Should().Be(expectedResult);
        }
    }
}
