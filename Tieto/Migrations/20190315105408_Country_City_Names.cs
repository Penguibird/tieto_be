using Microsoft.EntityFrameworkCore.Migrations;

namespace Tieto.Migrations
{
    public partial class Country_City_Names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Allowances_AllowanceID",
                table: "Countries");

            migrationBuilder.RenameColumn(
                name: "AllowanceID",
                table: "Countries",
                newName: "Rate66ID");

            migrationBuilder.RenameIndex(
                name: "IX_Countries_AllowanceID",
                table: "Countries",
                newName: "IX_Countries_Rate66ID");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rate100ID",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rate33ID",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Allowances",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Rate100ID",
                table: "Countries",
                column: "Rate100ID");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Rate33ID",
                table: "Countries",
                column: "Rate33ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Allowances_Rate100ID",
                table: "Countries",
                column: "Rate100ID",
                principalTable: "Allowances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Allowances_Rate33ID",
                table: "Countries",
                column: "Rate33ID",
                principalTable: "Allowances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Allowances_Rate66ID",
                table: "Countries",
                column: "Rate66ID",
                principalTable: "Allowances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Allowances_Rate100ID",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Allowances_Rate33ID",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Allowances_Rate66ID",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_Rate100ID",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_Rate33ID",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Rate100ID",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Rate33ID",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Allowances");

            migrationBuilder.RenameColumn(
                name: "Rate66ID",
                table: "Countries",
                newName: "AllowanceID");

            migrationBuilder.RenameIndex(
                name: "IX_Countries_Rate66ID",
                table: "Countries",
                newName: "IX_Countries_AllowanceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Allowances_AllowanceID",
                table: "Countries",
                column: "AllowanceID",
                principalTable: "Allowances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
