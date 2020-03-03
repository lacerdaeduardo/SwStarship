namespace SwStarship.Core.Domain.Models
{
    public class Starship
    {
        private const int hoursInDay = 24;

        public string Name { get; set; }
        public string MGLT { get; set; }
        public string Consumables { get; set; }

        public int GetDailyMGLT()
        {
            int parsedValue;

            if (int.TryParse(this.MGLT, out parsedValue))
            {
                return parsedValue * hoursInDay;
            }

            return 0;           
        }
    }
}
