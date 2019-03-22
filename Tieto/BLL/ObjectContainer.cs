using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.DLL;

namespace Tieto.BLL
{
    public static class ObjectContainer
    {

        public static ITripManager GetTripManager()
        {
            return new TripManager();
        }

        public static ILocationDbProvider GetLocationDbProvider()
        {
            return new LocationDbProvider();
        }

        public static ILocationManager GetLocationManager()
        {
            return new LocationManager();
        }

        public static IUserManager GetUserManager()
        {
            return new UserManager();
        }

        public static ITripDbProvider GetTripDbProvider()
        {
            return new TripDbProvider();
        }

        public static IUserDbProvider GetUserDbProvider()
        {
            return new UserDbProvider();
        }

        public static IExchangeRateManager GetExchangeRateManager()
        {
            return new ExchangeRateManager();
        }

        public static IExchangeRateDbProvider GetExchangeRateDbProvider()
        {
            return new ExchangeRateDbProvider();
        }

        public static IPDFManager GetPDFManager()
        {
            return new PDFManager();
        }

        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

    }
}
