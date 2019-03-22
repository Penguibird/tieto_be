using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.DLL
{
    public interface IExchangeRateDbProvider
    {
        void Create(DayExchange dayExchange);

        DayExchange Read(int id);

        bool ContainsToday(DateTime dateTime);

        IEnumerable<ExchangeRate> GetToday(DateTime dateTime);
    }
}
