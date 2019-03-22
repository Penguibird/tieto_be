using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class DayExchange
    {

        public int ID { get; set; }
        public IEnumerable<ExchangeRate> Rates { get; set; }
        public DateTime Date { get; set; }

    }
}
