using System;

namespace SwStarship.Core.Util
{
    public class SupplyStopCalculator
    {
        public int? CalculateNumberOfStops(double distance, double mgltDailyDistance, double daysOfSupplyDuration)
        {
            if (mgltDailyDistance == 0 || daysOfSupplyDuration == 0)
            {
                return null;
            }

            // rounds the number to the previous int value, so 11.57 will be 11.
            var result = Math.Floor(distance / (mgltDailyDistance * daysOfSupplyDuration));
            return Convert.ToInt32(result);
        }
    }
}
