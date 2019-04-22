using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class Trip_Location_Finalization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Countries_CrossingFromID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Countries_CrossingToID",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CrossingFromID",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CrossingToID",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CrossingFromID",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CrossingToID",
                table: "Locations");

            migrationBuilder.AddColumn<bool>(
                name: "SectionModified",
                table: "Locations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SectionModified",
                table: "Locations");

            migrationBuilder.AddColumn<int>(
                name: "CrossingFromID",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CrossingToID",
                table: "Locations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CrossingFromID",
                table: "Locations",
                column: "CrossingFromID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CrossingToID",
                table: "Locations",
                column: "CrossingToID");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Countries_CrossingFromID",
                table: "Locations",
                column: "CrossingFromID",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Countries_CrossingToID",
                table: "Locations",
                column: "CrossingToID",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
