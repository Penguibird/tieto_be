using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.DLL
{
    public class ExchangeRateDbProvider : BaseDbProvider, IExchangeRateDbProvider
    {
        public void Create(DayExchange dayExchange)
        {
            DbContext.DayExchanges.Add(dayExchange);
            DbContext.SaveChanges();
        }

        public DayExchange Read(int id)
        {
            return DbContext.DayExchanges.Find(id);
        }

        public bool ContainsToday(long dateTime)
        {
            var l = DbContext.DayExchanges.Where(d => d.Date == dateTime).ToList();

            return l.Count == 1;
        }

        public IEnumerable<ExchangeRate> GetToday(long dateTime)
        {
            var l = DbContext.DayExchanges.Include("Rates").Where(d => d.Date == dateTime).ToList();
            return l.ElementAt(0).Rates;
        }
    }
}
