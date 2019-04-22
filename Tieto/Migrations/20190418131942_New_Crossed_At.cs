using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class New_Crossed_At : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CrossedAtDate",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CrossedAtTime",
                table: "Locations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CrossedAtDate",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CrossedAtTime",
                table: "Locations");
        }
    }
}
