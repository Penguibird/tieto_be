﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tieto.Models;

namespace Tieto.Migrations
{
    [DbContext(typeof(TrippiDb))]
    [Migration("20190316223318_Locations_Name_Change")]
    partial class Locations_Name_Change
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tieto.Models.Allowance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Currency");

                    b.Property<double>("MoneyAmount");

                    b.HasKey("ID");

                    b.ToTable("Allowances");
                });

            modelBuilder.Entity("Tieto.Models.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryID");

                    b.Property<string>("GooglePlaceId");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Tieto.Models.Country", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("Rate100ID");

                    b.Property<int?>("Rate33ID");

                    b.Property<int?>("Rate66ID");

                    b.HasKey("ID");

                    b.HasIndex("Rate100ID");

                    b.HasIndex("Rate33ID");

                    b.HasIndex("Rate66ID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Tieto.Models.DayExchange", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.HasKey("ID");

                    b.ToTable("DayExchanges");
                });

            modelBuilder.Entity("Tieto.Models.DayFood", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Breakfast");

                    b.Property<bool>("Dinner");

                    b.Property<bool>("Lunch");

                    b.HasKey("ID");

                    b.ToTable("DayFood");
                });

            modelBuilder.Entity("Tieto.Models.ExchangeRate", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrencyCode");

                    b.Property<int?>("DayExchangeID");

                    b.Property<double>("Rate");

                    b.Property<int?>("TripID");

                    b.HasKey("ID");

                    b.HasIndex("DayExchangeID");

                    b.HasIndex("TripID");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("Tieto.Models.Location", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Arrival");

                    b.Property<int?>("CityID");

                    b.Property<int?>("CrossingFromID");

                    b.Property<int?>("CrossingToID");

                    b.Property<DateTime>("Departure");

                    b.Property<int?>("FoodID");

                    b.Property<int>("InboundTravelType");

                    b.Property<bool>("IsCrossing");

                    b.Property<int?>("TripID");

                    b.HasKey("ID");

                    b.HasIndex("CityID");

                    b.HasIndex("CrossingFromID");

                    b.HasIndex("CrossingToID");

                    b.HasIndex("FoodID");

                    b.HasIndex("TripID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Tieto.Models.LocationFood", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FirstDayID");

                    b.Property<int?>("LastDayID");

                    b.Property<int?>("MiddleDaysID");

                    b.Property<int?>("OnlyDayID");

                    b.HasKey("ID");

                    b.HasIndex("FirstDayID");

                    b.HasIndex("LastDayID");

                    b.HasIndex("MiddleDaysID");

                    b.HasIndex("OnlyDayID");

                    b.ToTable("LocationFood");
                });

            modelBuilder.Entity("Tieto.Models.Trip", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<bool>("Deleted");

                    b.Property<bool>("Exported");

                    b.Property<string>("Project");

                    b.Property<string>("Purpose");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Task");

                    b.Property<string>("Title");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("Tieto.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tieto.Models.City", b =>
                {
                    b.HasOne("Tieto.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID");
                });

            modelBuilder.Entity("Tieto.Models.Country", b =>
                {
                    b.HasOne("Tieto.Models.Allowance", "Rate100")
                        .WithMany()
                        .HasForeignKey("Rate100ID");

                    b.HasOne("Tieto.Models.Allowance", "Rate33")
                        .WithMany()
                        .HasForeignKey("Rate33ID");

                    b.HasOne("Tieto.Models.Allowance", "Rate66")
                        .WithMany()
                        .HasForeignKey("Rate66ID");
                });

            modelBuilder.Entity("Tieto.Models.ExchangeRate", b =>
                {
                    b.HasOne("Tieto.Models.DayExchange")
                        .WithMany("Rates")
                        .HasForeignKey("DayExchangeID");

                    b.HasOne("Tieto.Models.Trip")
                        .WithMany("ExchangeRates")
                        .HasForeignKey("TripID");
                });

            modelBuilder.Entity("Tieto.Models.Location", b =>
                {
                    b.HasOne("Tieto.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityID");

                    b.HasOne("Tieto.Models.City", "CrossingFrom")
                        .WithMany()
                        .HasForeignKey("CrossingFromID");

                    b.HasOne("Tieto.Models.City", "CrossingTo")
                        .WithMany()
                        .HasForeignKey("CrossingToID");

                    b.HasOne("Tieto.Models.LocationFood", "Food")
                        .WithMany()
                        .HasForeignKey("FoodID");

                    b.HasOne("Tieto.Models.Trip")
                        .WithMany("Locations")
                        .HasForeignKey("TripID");
                });

            modelBuilder.Entity("Tieto.Models.LocationFood", b =>
                {
                    b.HasOne("Tieto.Models.DayFood", "FirstDay")
                        .WithMany()
                        .HasForeignKey("FirstDayID");

                    b.HasOne("Tieto.Models.DayFood", "LastDay")
                        .WithMany()
                        .HasForeignKey("LastDayID");

                    b.HasOne("Tieto.Models.DayFood", "MiddleDays")
                        .WithMany()
                        .HasForeignKey("MiddleDaysID");

                    b.HasOne("Tieto.Models.DayFood", "OnlyDay")
                        .WithMany()
                        .HasForeignKey("OnlyDayID");
                });
#pragma warning restore 612, 618
        }
    }
}
