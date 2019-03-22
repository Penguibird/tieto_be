using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class Crossing_Points_reference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CrossingFromID",
                table: "Location",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CrossingToID",
                table: "Location",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_CrossingFromID",
                table: "Location",
                column: "CrossingFromID");

            migrationBuilder.CreateIndex(
                name: "IX_Location_CrossingToID",
                table: "Location",
                column: "CrossingToID");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Cities_CrossingFromID",
                table: "Location",
                column: "CrossingFromID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Cities_CrossingToID",
                table: "Location",
                column: "CrossingToID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Cities_CrossingFromID",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Cities_CrossingToID",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_CrossingFromID",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_CrossingToID",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "CrossingFromID",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "CrossingToID",
                table: "Location");
        }
    }
}
