using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class Location_Trip_Refeerence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Trips_TripID",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "TripID",
                table: "Locations",
                newName: "TripId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_TripID",
                table: "Locations",
                newName: "IX_Locations_TripId");

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "Locations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Trips_TripId",
                table: "Locations",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Trips_TripId",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "Locations",
                newName: "TripID");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_TripId",
                table: "Locations",
                newName: "IX_Locations_TripID");

            migrationBuilder.AlterColumn<int>(
                name: "TripID",
                table: "Locations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Trips_TripID",
                table: "Locations",
                column: "TripID",
                principalTable: "Trips",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
