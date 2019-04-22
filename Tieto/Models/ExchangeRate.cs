using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class ExchangeRate
    {

        public int ID { get; set; }
        public CurrencyCode CurrencyCode { get; set; }
        public double Rate { get; set; }
        public double DefaultRate { get; set; }
        public bool Altered { get; set; }

    }

    public enum CurrencyCode
    {
        EUR = 0,
        USD = 1,
        CZK = 2,
        CHF = 3,
        GBP = 4
    }
}
