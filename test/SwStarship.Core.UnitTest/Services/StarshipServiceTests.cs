using Xunit;
using FluentAssertions;
using SwStarship.Core.Services;
using Moq;
using System.Linq;
using SwStarship.Core.Interfaces;
using System.Collections.Generic;
using SwStarship.Core.Domain.Models;

namespace SwStarship.Core.UnitTest.Services
{
    public class StarshipServiceTests
    {
        private readonly Mock<ISwApiClient> _swApiClient;

        #region Setup
        public StarshipServiceTests()
        {
            var mockedClient = new Mock<ISwApiClient>();
            mockedClient.Setup(x => x.GetStarshipsAsync()).ReturnsAsync(MockedData());

            _swApiClient = mockedClient;
        }

        private static IEnumerable<Starship> MockedData()
        {
            return new List<Starship>()
            {
                new Starship(){ Name = "Death Star", Consumables = "7 days", MGLT = 10 },
                new Starship(){ Name = "Death Star", Consumables = "7 days", MGLT = 10 },
                new Starship(){ Name = "Death Star", Consumables = "7 days", MGLT = 10 }
            };
        }
        #endregion

        [Fact]
        public async void ShouldRetrieveStarships()
        {
            // Arrange
            var mockedData = MockedData();
            StarshipService starshipService = new StarshipService(_swApiClient.Object);

            // Act
            IEnumerable<Starship> starships = await starshipService.GetAsync();
            
            //Assert
            starships.Should().NotBeEmpty().And.HaveCount(mockedData.Count());            
            _swApiClient.Verify(x => x.GetStarshipsAsync(), Times.Once);
        }        
    }
}
