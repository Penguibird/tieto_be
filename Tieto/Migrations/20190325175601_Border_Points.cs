using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class Border_Points : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CrossedAt",
                table: "Locations",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "CrossedBorder",
                table: "Locations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CrossedAt",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CrossedBorder",
                table: "Locations");
        }
    }
}
