﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class LocationFood
    {
        public int ID { get; set; }
        public DayFood FirstDay { get; set; }
        public IList<DayFood> MiddleDays { get; set; }
        public DayFood LastDay { get; set; }
        public DayFood OnlyDay { get; set; }

        public LocationFood()
        {
        }
    }
}
