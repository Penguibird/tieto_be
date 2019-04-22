using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class MapLeg
    {
        public MapObject Distance { get; set; }
        public MapObject Duration { get; set; }
        public MapStep[] Steps { get; set; }


    }
}
