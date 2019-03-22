using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class Locations_Name_Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Cities_CityID",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Cities_CrossingFromID",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Cities_CrossingToID",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_LocationFood_FoodID",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Trips_TripID",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameIndex(
                name: "IX_Location_TripID",
                table: "Locations",
                newName: "IX_Locations_TripID");

            migrationBuilder.RenameIndex(
                name: "IX_Location_FoodID",
                table: "Locations",
                newName: "IX_Locations_FoodID");

            migrationBuilder.RenameIndex(
                name: "IX_Location_CrossingToID",
                table: "Locations",
                newName: "IX_Locations_CrossingToID");

            migrationBuilder.RenameIndex(
                name: "IX_Location_CrossingFromID",
                table: "Locations",
                newName: "IX_Locations_CrossingFromID");

            migrationBuilder.RenameIndex(
                name: "IX_Location_CityID",
                table: "Locations",
                newName: "IX_Locations_CityID");

            migrationBuilder.AddColumn<string>(
                name: "GooglePlaceId",
                table: "Cities",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MoneyAmount",
                table: "Allowances",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Cities_CityID",
                table: "Locations",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Cities_CrossingFromID",
                table: "Locations",
                column: "CrossingFromID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Cities_CrossingToID",
                table: "Locations",
                column: "CrossingToID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_LocationFood_FoodID",
                table: "Locations",
                column: "FoodID",
                principalTable: "LocationFood",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Trips_TripID",
                table: "Locations",
                column: "TripID",
                principalTable: "Trips",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Cities_CityID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Cities_CrossingFromID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Cities_CrossingToID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_LocationFood_FoodID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Trips_TripID",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "GooglePlaceId",
                table: "Cities");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_TripID",
                table: "Location",
                newName: "IX_Location_TripID");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_FoodID",
                table: "Location",
                newName: "IX_Location_FoodID");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_CrossingToID",
                table: "Location",
                newName: "IX_Location_CrossingToID");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_CrossingFromID",
                table: "Location",
                newName: "IX_Location_CrossingFromID");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_CityID",
                table: "Location",
                newName: "IX_Location_CityID");

            migrationBuilder.AlterColumn<int>(
                name: "MoneyAmount",
                table: "Allowances",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Cities_CityID",
                table: "Location",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Location_LocationFood_FoodID",
                table: "Location",
                column: "FoodID",
                principalTable: "LocationFood",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Trips_TripID",
                table: "Location",
                column: "TripID",
                principalTable: "Trips",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
