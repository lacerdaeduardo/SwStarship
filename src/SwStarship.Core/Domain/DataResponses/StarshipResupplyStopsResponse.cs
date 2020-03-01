using SwStarship.Core.Domain.Models;
namespace SwStarship.Core.Domain.DataResponses
{
    public class StarshipResupplyStopsResponse
    {
        public Starship Starship { get; set; }
        public int Distance { get; set; }
        public int NumberOfStops { get; set; }
    }
}
