﻿using System.Collections.Generic;

namespace SwStarship.Core.Generics
{
    public class PagedList<T> where T : class
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
