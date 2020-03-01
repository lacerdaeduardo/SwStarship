using Xunit;
using FluentAssertions;
using SwStarship.Core.Domain.Models;

namespace SwStarship.Core.UnitTest.Models
{
    public class StarshipTests
    {
        [Theory]
        [InlineData("Mocked Starship", "3 years", 10, 10 * 24)]
        [InlineData("Mocked Starship2", "3 years", 5, 5 * 24)]
        [InlineData("Mocked Starship3", "3 years", 33, 33 * 24)]
        [InlineData("Mocked Starship4", "3 years", 250, 250 * 24)]
        public void ExpectGetCorrectDailyMGLT(string name, string consumables, int mglt, int expectedResult)
        {
            Starship starship = new Starship() { Name = name, Consumables = consumables, MGLT = mglt };

            starship.GetDailyMGLT().Should().Be(expectedResult);
        }
    }
}
