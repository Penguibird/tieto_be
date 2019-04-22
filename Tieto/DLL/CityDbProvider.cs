using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tieto.Models;

namespace Tieto.DLL
{
    public class CityDbProvider : BaseDbProvider, ICityDbProvider
    {
        public Country GetCountryByName(string name)
        {
            Country country;
            try
            {
                country = DbContext.Countries.Include(c => c.Rate100).Include(c => c.Rate66).Include(c => c.Rate33).FirstOrDefault(c => c.Name == name);
                return country;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
