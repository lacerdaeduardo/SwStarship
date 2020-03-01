using Xunit;
using FluentAssertions;
using SwStarship.Core.Services;
using Moq;
using System.Linq;
using SwStarship.Core.Interfaces;
using System.Collections.Generic;
using SwStarship.Core.Domain.Models;
using SwStarship.Core.Util;
using SwStarship.Core.Domain.DataResponses;

namespace SwStarship.Core.UnitTest.Services
{
    public class StarshipServiceTests : IClassFixture<DataFixture>
    {
        private readonly Mock<ISwApiClient> _swApiClient;
        private readonly Mock<SupplyStopCalculator> _calculator;
        private readonly DataFixture _fixture;

        #region Setup
        public StarshipServiceTests(DataFixture fixture)
        {
            _fixture = fixture;

            var mockedClient = new Mock<ISwApiClient>();
            var mockedCalculator = new Mock<SupplyStopCalculator>();

            _swApiClient = mockedClient;
            _calculator = mockedCalculator;

        }


        #endregion

        [Fact]
        public async void ShouldRetrieveStarships()
        {
            var mockedData = _fixture.MockedData();
            _swApiClient.Setup(x => x.GetStarshipsAsync()).ReturnsAsync(mockedData);
            var _starshipService = new StarshipService(_swApiClient.Object, _calculator.Object);

            IEnumerable<Starship> starships = await _starshipService.GetAsync();
            
            starships.Should().NotBeEmpty().And.HaveCount(mockedData.Count());            
            _swApiClient.Verify(x => x.GetStarshipsAsync(), Times.Once);
        }        
        
        [Fact]        
        public async void ExpectRetrieveAllStarshipsAndItsNumberOfStops()
        {
            int distance = 100000;
            var mockedData = _fixture.MockedData();
            _swApiClient.Setup(x => x.GetStarshipsAsync()).ReturnsAsync(mockedData);
            var _starshipService = new StarshipService(_swApiClient.Object, _calculator.Object);

            IEnumerable<StarshipResupplyStopsResponse> response = await _starshipService.ProcessAllStarshipsTotalResupplyStopsAsync(distance);

            response.Should().NotBeEmpty().And.HaveCount(mockedData.Count());
            
            response.All(x => x.Starship != null).Should().BeTrue();
            response.All(x => x.NumberOfStops >= 0).Should().BeTrue();
            response.All(x => x.Distance == distance).Should().BeTrue();

            _swApiClient.Verify(x => x.GetStarshipsAsync(), Times.Once);
        }
    }
}
