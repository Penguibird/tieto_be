using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class Trip_Exchange_Rate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
