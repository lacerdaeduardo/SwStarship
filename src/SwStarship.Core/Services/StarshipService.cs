using SwStarship.Core.Domain.Models;
using SwStarship.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwStarship.Core.Services
{
    public class StarshipService
    {
        private readonly ISwApiClient _swApiClient;

        public StarshipService(ISwApiClient swApiClient)
        {
            _swApiClient = swApiClient;
        }

        public Task<IEnumerable<Starship>> GetAsync()
        {
            return _swApiClient.GetStarshipsAsync();
        }
    }
}
