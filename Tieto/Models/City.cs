using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class City
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public int CountryID { get; set; }
        [NotMapped]
        public Country Country { get; set; }
        public string GooglePlaceId { get; set; }

    }
}
