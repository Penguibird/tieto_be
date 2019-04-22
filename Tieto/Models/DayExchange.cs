using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class DayExchange
    {

        public int ID { get; set; }
        public IList<ExchangeRate> Rates { get; set; }
        public long Date { get; set; }
        public bool Deleted { get; set; }

    }
}
