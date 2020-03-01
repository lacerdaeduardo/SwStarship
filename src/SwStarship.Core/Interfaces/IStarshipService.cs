using SwStarship.Core.Domain.DataResponses;
using SwStarship.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwStarship.Core.Interfaces
{
    public interface IStarshipService
    {
        Task<IEnumerable<Starship>> GetAsync();
        Task<IEnumerable<StarshipResupplyStopsResponse>> ProcessAllStarshipsTotalResupplyStopsAsync(int distance);
    }
}