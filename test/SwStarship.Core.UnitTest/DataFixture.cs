﻿using SwStarship.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwStarship.Core.UnitTest
{
    public class DataFixture 
    {
        public IEnumerable<Starship> MockedData()
        {
            return new List<Starship>()
            {
                new Starship(){ Name = "Death Star", Consumables = "7 days", MGLT = "10" },
                new Starship(){ Name = "Y-Wing", Consumables = "7 weeks", MGLT = "25" },
                new Starship(){ Name = "Millennium Falcon", Consumables = "2 months", MGLT = "90" },
                new Starship(){ Name = "Theta-class T-2c shuttle", Consumables = "2 months", MGLT = "unknown" }
            };
        }

    }
}
