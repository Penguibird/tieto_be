using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class Default_Crosset_At : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DefaultCrossedAt",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DefaultRate",
                table: "ExchangeRates",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultCrossedAt",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "DefaultRate",
                table: "ExchangeRates");
        }
    }
}
