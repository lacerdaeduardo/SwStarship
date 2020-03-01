using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using RestSharp;
using SwStarship.Core.Domain.Models;
using SwStarship.Core.Generics;
using SwStarship.Core.Util;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using Xunit;

namespace SwStarship.Core.UnitTest.Util
{
    public class SwApiClientTests : IClassFixture<DataFixture>
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IRestClient> _restClientMock;
        private readonly DataFixture _fixture;

        public SwApiClientTests(DataFixture fixture)
        {
            _fixture = fixture;
            _configurationMock = new Mock<IConfiguration>();
            _restClientMock = new Mock<IRestClient>();

            _restClientMock.SetupGet(x => x.BaseUrl).Returns(new Uri("https://swapi.co/api/starships"));

            _configurationMock = new Mock<IConfiguration>();
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(x => x.Value).Returns("/starships");

            _configurationMock.Setup(a => a.GetSection(It.Is<string>(s => s == "SwStarshipsEndpoint"))).Returns(configurationSection.Object);
        }

        [Fact]
        public async void ShouldGetStarshipsFromSwApi()
        {
            var mockedData = _fixture.MockedData();

            var mockedResponse1 = new Mock<IRestResponse<PagedList<Starship>>>();
            mockedResponse1.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);
            mockedResponse1.SetupGet(x => x.Data).Returns(new PagedList<Starship>()
            {
                Next = "page2",
                Count = 20,
                Results = mockedData.Take(1)
            });

            var mockedResponse2 = new Mock<IRestResponse<PagedList<Starship>>>();
            mockedResponse2.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);
            mockedResponse2.SetupGet(x => x.Data).Returns(new PagedList<Starship>()
            {
                Next = null,
                Count = 20,
                Results = mockedData.Skip(1)
            });

            _restClientMock.SetupSequence(x => x.ExecuteGetAsync<PagedList<Starship>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockedResponse1.Object)
                .ReturnsAsync(mockedResponse2.Object);

            SwApiClient swApiClient = new SwApiClient(_configurationMock.Object, _restClientMock.Object);

            var data = await swApiClient.GetStarshipsAsync();

            data.Should().NotBeEmpty().And.HaveCount(mockedData.Count());
        }


    }
}
