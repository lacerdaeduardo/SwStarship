using EnumsNET;
using System;
using System.Text.RegularExpressions;

namespace SwStarship.Core.Domain.Models
{
    public sealed class Consumable
    {
        public int Quantity { get; set; }
        public TimeUnit TimeUnit { get; set; }

        private Consumable() { }

        public static Consumable Parse(string input)
        {
            if (!ValidateInput(input))
                throw new ArgumentException($"Provided consumable {input} is not valid.");

            var splitedInput = input.Split(' ');
            Consumable instance = new Consumable();
            instance.Quantity = int.Parse(splitedInput[0]);
            instance.TimeUnit = Enums.Parse<TimeUnit>(splitedInput[1], ignoreCase: true, EnumFormat.Name);

            return instance;
        }

        public int TimeUnitToDays()
        {
            return Quantity * (int) this.TimeUnit;
        }


        /// <summary>
        /// Check if string input is contains two word phrase with a digit and a time unit between 
        /// day(s), week(s), month(s), year(s)
        /// </summary>        
        /// <param name="consumable"></param>
        /// <returns>true of false</returns>
        public static bool ValidateInput(string consumable)
        {
            Regex regex = new Regex(@"^\d*\s(\w*year(s)?|\w*month(s)?\w*|\w*week(s)?\w*|\w*day(s)?\w*)$", RegexOptions.IgnoreCase);

            return regex.IsMatch(consumable);
        }
    }
}
