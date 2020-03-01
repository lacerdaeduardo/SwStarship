using System;

namespace SwStarship.Core.Util
{
    public class SupplyStopCalculator
    {
        public int CalculateNumberOfStops(int distance, int mgltDailyDistance, int daysOfSupplyDuration)
        {
            return distance / (mgltDailyDistance * daysOfSupplyDuration);
        }
    }
}
