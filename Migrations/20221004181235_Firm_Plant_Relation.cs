using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class Firm_Plant_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlantId",
                table: "Firm",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Firm_PlantId",
                table: "Firm",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Firm_Plant_PlantId",
                table: "Firm",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Firm_Plant_PlantId",
                table: "Firm");

            migrationBuilder.DropIndex(
                name: "IX_Firm_PlantId",
                table: "Firm");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Firm");
        }
    }
}
