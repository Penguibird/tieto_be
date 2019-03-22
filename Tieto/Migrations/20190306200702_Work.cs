using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class Work : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_UserID",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_UserID",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Trips",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Trips_UserID",
                table: "Trips",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_UserID",
                table: "Trips",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
