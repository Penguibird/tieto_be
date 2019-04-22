using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class Trip_Exchange_Rates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Trips_TripID",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_TripID",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "TripID",
                table: "ExchangeRates");

            migrationBuilder.AddColumn<int>(
                name: "ExchangeID",
                table: "Trips",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ExchangeID",
                table: "Trips",
                column: "ExchangeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_DayExchanges_ExchangeID",
                table: "Trips",
                column: "ExchangeID",
                principalTable: "DayExchanges",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_DayExchanges_ExchangeID",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_ExchangeID",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ExchangeID",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "TripID",
                table: "ExchangeRates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_TripID",
                table: "ExchangeRates",
                column: "TripID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Trips_TripID",
                table: "ExchangeRates",
                column: "TripID",
                principalTable: "Trips",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
