using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class Trip
    {

        public int ID { get; set; }
        public IEnumerable<Location> Locations { get; set; }

    }
}
