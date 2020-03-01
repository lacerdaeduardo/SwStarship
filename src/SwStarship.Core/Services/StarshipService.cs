using SwStarship.Core.Domain.DataResponses;
using SwStarship.Core.Domain.Models;
using SwStarship.Core.Interfaces;
using SwStarship.Core.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwStarship.Core.Services
{
    public class StarshipService
    {
        private readonly ISwApiClient _swApiClient;
        private readonly SupplyStopCalculator _supplyStopCalculator;

        public StarshipService(ISwApiClient swApiClient,
            SupplyStopCalculator supplyStopCalculator)
        {
            _swApiClient = swApiClient;
            _supplyStopCalculator = supplyStopCalculator;
        }


        /// <summary>
        /// Gets all Starships from SwAPI
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Starship>> GetAsync()
        {
            return _swApiClient.GetStarshipsAsync();
        }

        /// <summary>
        /// Process for all Starships the total amount of resupply stops needed for the provided distance
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StarshipResupplyStopsResponse>> ProcessAllStarshipsTotalResupplyStopsAsync(int distance)
        {
            var starships = await this.GetAsync();
            var response = new List<StarshipResupplyStopsResponse>();

            starships.ToList().ForEach(starship =>
            {
                var consumable = Consumable.Parse(starship.Consumables);
                var numberOfStops = _supplyStopCalculator.CalculateNumberOfStops(distance, starship.GetDailyMGLT(), consumable.TimeUnitToDays());

                response.Add(new StarshipResupplyStopsResponse()
                {
                    Starship = starship,
                    Distance = distance,
                    NumberOfStops = numberOfStops
                });
            });

            return response;
        }
    }
}
