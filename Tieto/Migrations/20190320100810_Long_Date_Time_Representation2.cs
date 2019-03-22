using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class Long_Date_Time_Representation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalDate",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "DepartureDate",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Locations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ArrivalDate",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ArrivalTime",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartureDate",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartureTime",
                table: "Locations",
                nullable: true);
        }
    }
}
