using SwStarship.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwStarship.Core.Interfaces
{
    public interface ISwApiClient
    {
        Task<IEnumerable<Starship>> GetStarshipsAsync();
    }
}
