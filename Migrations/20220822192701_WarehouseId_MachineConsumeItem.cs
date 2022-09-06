using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class WarehouseId_MachineConsumeItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "MachineItemConsume",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MachineItemConsume_WarehouseId",
                table: "MachineItemConsume",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineItemConsume_Warehouse_WarehouseId",
                table: "MachineItemConsume",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineItemConsume_Warehouse_WarehouseId",
                table: "MachineItemConsume");

            migrationBuilder.DropIndex(
                name: "IX_MachineItemConsume_WarehouseId",
                table: "MachineItemConsume");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "MachineItemConsume");
        }
    }
}
