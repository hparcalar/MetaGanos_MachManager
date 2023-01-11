using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class WarehouseController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlantId",
                table: "Warehouse",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_PlantId",
                table: "Warehouse",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Plant_PlantId",
                table: "Warehouse",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Plant_PlantId",
                table: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_Warehouse_PlantId",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Warehouse");
        }
    }
}
