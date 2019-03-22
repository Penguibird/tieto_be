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
    [Migration("20190307180950_Trip_Deletion")]
    partial class Trip_Deletion
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

                    b.Property<int>("MoneyAmount");

                    b.HasKey("ID");

                    b.ToTable("Allowances");
                });

            modelBuilder.Entity("Tieto.Models.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryID");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Tieto.Models.Country", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AllowanceID");

                    b.HasKey("ID");

                    b.HasIndex("AllowanceID");

                    b.ToTable("Countries");
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

            modelBuilder.Entity("Tieto.Models.Location", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Arrival");

                    b.Property<int?>("CityID");

                    b.Property<DateTime>("Departure");

                    b.Property<int?>("FoodID");

                    b.Property<int>("InboundTravelType");

                    b.Property<bool>("IsCrossing");

                    b.Property<int?>("TripID");

                    b.HasKey("ID");

                    b.HasIndex("CityID");

                    b.HasIndex("FoodID");

                    b.HasIndex("TripID");

                    b.ToTable("Location");
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

                    b.Property<bool>("Deleted");

                    b.Property<string>("Project");

                    b.Property<string>("Purpose");

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
                    b.HasOne("Tieto.Models.Allowance", "Allowance")
                        .WithMany()
                        .HasForeignKey("AllowanceID");
                });

            modelBuilder.Entity("Tieto.Models.Location", b =>
                {
                    b.HasOne("Tieto.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityID");

                    b.HasOne("Tieto.Models.LocationFood", "Food")
                        .WithMany()
                        .HasForeignKey("FoodID");

                    b.HasOne("Tieto.Models.Trip", "Trip")
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
