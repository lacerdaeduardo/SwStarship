using EnumsNET;
using System;
using System.Text.RegularExpressions;

namespace SwStarship.Core.Domain.Models
{
    public sealed class Consumable
    {
        public double Quantity { get; set; }
        public TimeUnit TimeUnit { get; set; }

        private Consumable() { }

        public static Consumable Parse(string input)
        {
            if (!ValidateInput(input))
            {
                throw new ArgumentException($"Provided consumable {input} is not valid.");
            }

            Consumable instance = new Consumable();

            if (IsUnknown(input))
            {
                instance.Quantity = 0;
                instance.TimeUnit = TimeUnit.Unknown;

                return instance;
            }

            var splitedInput = input.Split(' ');
            instance.Quantity = int.Parse(splitedInput[0]);
            instance.TimeUnit = Enums.Parse<TimeUnit>(splitedInput[1], ignoreCase: true, EnumFormat.Name);

            return instance;
        }


        /// <summary>
        /// Get the equivalent quantity/time for one day
        /// </summary>
        /// <returns></returns>
        public double ConvertTimeUnitToDays()
        {
            return this.TimeUnit == TimeUnit.Hour ?
                CalculateByHourFormula() :
                CalculateByDefaultFormula();
        }

        private double CalculateByHourFormula()
        {
            return Quantity / (int)TimeUnit.Hour;
        }

        private double CalculateByDefaultFormula()
        {
            return Quantity * (int)this.TimeUnit;
        }


        /// <summary>
        /// Check if string input is contains two word phrase with a digit and a time unit between or the word unknown
        /// day(s), week(s), month(s), year(s)
        /// </summary>        
        /// <param name="consumable"></param>
        /// <returns>true of false</returns>
        public static bool ValidateInput(string consumable)
        {
            bool isValidString = new Regex(@"^\d*\s(year(s)?|month(s)?|week(s)?|day(s)?|hour(s)?)$",
                                           RegexOptions.IgnoreCase).IsMatch(consumable);

            return isValidString || IsUnknown(consumable);
        }

        private static bool IsUnknown(string consumable)
        {
            return "unknown".Equals(consumable, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
