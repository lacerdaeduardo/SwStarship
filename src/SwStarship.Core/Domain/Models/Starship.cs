namespace SwStarship.Core.Domain.Models
{
    public class Starship
    {
        private const int hoursInDay = 24;

        public string Name { get; set; }
        public int MGLT { get; set; }
        public string Consumables { get; set; }

        public int GetDailyMGLT() => this.MGLT * hoursInDay;                
    }
}
