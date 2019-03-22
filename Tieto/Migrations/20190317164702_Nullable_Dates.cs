using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class Nullable_Dates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Cities_CrossingFromID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Cities_CrossingToID",
                table: "Locations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Departure",
                table: "Locations",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Arrival",
                table: "Locations",
                nullable: true,
                oldClrType: typeof(DateTime));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Countries_CrossingFromID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Countries_CrossingToID",
                table: "Locations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Departure",
                table: "Locations",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Arrival",
                table: "Locations",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

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
        }
    }
}
