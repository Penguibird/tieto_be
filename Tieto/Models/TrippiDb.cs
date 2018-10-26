using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Tieto.Models
{
    public class TrippiDb : DbContext
    { 

        public TrippiDb() : base("trippi")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Allowance> Allowance { get; set; }

    }
}
