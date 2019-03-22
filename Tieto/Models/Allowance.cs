using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class Allowance
    {

        public int ID { get; set; }
        public double MoneyAmount { get; set; }
        public CurrencyCode Currency { get; set; }

    }
}
