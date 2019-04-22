using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class Middle_Day_Food_As_List : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationFood_DayFood_MiddleDaysID",
                table: "LocationFood");

            migrationBuilder.DropIndex(
                name: "IX_LocationFood_MiddleDaysID",
                table: "LocationFood");

            migrationBuilder.DropColumn(
                name: "MiddleDaysID",
                table: "LocationFood");

            migrationBuilder.AddColumn<int>(
                name: "LocationFoodID",
                table: "DayFood",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DayFood_LocationFoodID",
                table: "DayFood",
                column: "LocationFoodID");

            migrationBuilder.AddForeignKey(
                name: "FK_DayFood_LocationFood_LocationFoodID",
                table: "DayFood",
                column: "LocationFoodID",
                principalTable: "LocationFood",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayFood_LocationFood_LocationFoodID",
                table: "DayFood");

            migrationBuilder.DropIndex(
                name: "IX_DayFood_LocationFoodID",
                table: "DayFood");

            migrationBuilder.DropColumn(
                name: "LocationFoodID",
                table: "DayFood");

            migrationBuilder.AddColumn<int>(
                name: "MiddleDaysID",
                table: "LocationFood",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationFood_MiddleDaysID",
                table: "LocationFood",
                column: "MiddleDaysID");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationFood_DayFood_MiddleDaysID",
                table: "LocationFood",
                column: "MiddleDaysID",
                principalTable: "DayFood",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
