using Microsoft.Extensions.Configuration;
using RestSharp;
using SwStarship.Core.Domain.Models;
using SwStarship.Core.Generics;
using SwStarship.Core.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SwStarship.Core.Util
{
    public class SwApiClient : ISwApiClient
    {
        private readonly IConfiguration _configuration;
        private readonly IRestClient _restClient;
        private readonly string _starshipEndpoint;

        public SwApiClient(IConfiguration configuration, IRestClient restClient)
        {
            _configuration = configuration;
            _restClient = restClient;
            _starshipEndpoint = _configuration.GetSection("SwStarshipsEndpoint").Value;
        }

        public async Task<IEnumerable<Starship>> GetStarshipsAsync()
        {
            List<Starship> starships = new List<Starship>();

            var starshipRequest = new RestRequest(_starshipEndpoint, DataFormat.Json);

            bool isNextPageAvailable = true;

            while (isNextPageAvailable)
            {
                var getResponse = await _restClient.ExecuteGetAsync<PagedList<Starship>>(starshipRequest);                
                if (HttpStatusCode.OK.Equals(getResponse.StatusCode))
                {                    
                    starships.AddRange(getResponse.Data.Results);
                    isNextPageAvailable = !string.IsNullOrEmpty(getResponse.Data.Next);                    
                }
                else
                {
                    isNextPageAvailable = false;
                }                        
            }

            return starships;
        }
    }
}
