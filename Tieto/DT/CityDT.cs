using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.DT
{
    public class CityDT
    {
        public Prediction[] Predictions;
    }

    public class Prediction
    {
        public string place_id { get; set; }
        public StructuredFormatting structured_formatting { get; set; }
    }
    
    public class StructuredFormatting
    {
        public string main_text { get; set; }
        public string secondary_text { get; set; }
    }
}
