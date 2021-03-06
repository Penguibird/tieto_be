﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Tieto.Models
{
    public class TrippiDb : DbContext
    {

        /*public TrippiDb(DbContextOptions<TrippiDb> options) : base (options)
        {

        }*/

        public TrippiDb()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = /*configuration.GetConnectionString(*/@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=trippi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//);
                optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Trip>().HasData(new Trip { Locations = new List<Location> { } });
        }

        public DbSet<Allowance> Allowances { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DayExchange> DayExchanges { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

    }
}
