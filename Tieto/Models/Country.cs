﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class Country
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Allowance Rate33 { get; set; }
        public Allowance Rate66 { get; set; }
        public Allowance Rate100 { get; set; }

    }
}
