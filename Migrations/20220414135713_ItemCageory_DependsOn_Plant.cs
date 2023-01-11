using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class ItemCageory_DependsOn_Plant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlantId",
                table: "ItemCategory",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategory_PlantId",
                table: "ItemCategory",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCategory_Plant_PlantId",
                table: "ItemCategory",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCategory_Plant_PlantId",
                table: "ItemCategory");

            migrationBuilder.DropIndex(
                name: "IX_ItemCategory_PlantId",
                table: "ItemCategory");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "ItemCategory");
        }
    }
}
