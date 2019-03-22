using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class Date_Time_Split : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Departure",
                table: "Locations",
                newName: "DepartureTime");

            migrationBuilder.RenameColumn(
                name: "Arrival",
                table: "Locations",
                newName: "DepartureDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalDate",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "Locations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalDate",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "DepartureTime",
                table: "Locations",
                newName: "Departure");

            migrationBuilder.RenameColumn(
                name: "DepartureDate",
                table: "Locations",
                newName: "Arrival");
        }
    }
}
