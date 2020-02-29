using Xunit;
using FluentAssertions;
using SwStarship.Core.Domain.Models;

namespace SwStarship.Core.UnitTest.Models
{
    public class StarshipTests
    {
        [Fact]
        public void ExpectGetCorrectDailyMGLT()
        {
            int initialMGLT = 10;
            int hoursInDay = 24;

            Starship starship = new Starship()
            {
                Name = "Mocked Starship",
                Consumables = "3 years",
                MGLT = 10
            };

            starship.GetDailyMGLT().Should().Be(initialMGLT * hoursInDay);
        }

    }
}
