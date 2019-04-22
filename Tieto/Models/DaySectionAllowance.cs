using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class DaySectionAllowance
    {

        public DaySectionAllowance() : this(new Allowance(), 0, 0)
        {

        }
        public DaySectionAllowance(Allowance allowance, int hours, int foods)
        {
            MoneyAmount = allowance.MoneyAmount;
            Currency = allowance.Currency;
            Hours = hours;
            Foods = foods;
        }

        public int ID { get; set; }
        public double MoneyAmount { get; set; }
        public CurrencyCode Currency { get; set; }
        public int Hours { get; set; }
        public int Foods { get; set; }

    }
}
