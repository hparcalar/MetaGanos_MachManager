using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class WarehouseLoad_MachineRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "WarehouseLoad",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoad_MachineId",
                table: "WarehouseLoad",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLoad_Machine_MachineId",
                table: "WarehouseLoad",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLoad_Machine_MachineId",
                table: "WarehouseLoad");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseLoad_MachineId",
                table: "WarehouseLoad");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "WarehouseLoad");
        }
    }
}
