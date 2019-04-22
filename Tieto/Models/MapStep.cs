using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class MapStep
    {
        public MapObject Distance { get; set; }
        public MapObject Duration { get; set; }
        public string Html_Instructions { get; set; }
    }
}
