using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class PhaseEnd_Mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InLoadHeaderId",
                table: "WarehouseLoadHeader",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGenerated",
                table: "WarehouseLoadHeader",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutLoadHeaderId",
                table: "WarehouseLoadHeader",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutWarehouseId",
                table: "WarehouseLoadHeader",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AutoConsumptionWarehouseId",
                table: "Machine",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAutoConsumption",
                table: "Machine",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Employee",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoadHeader_InLoadHeaderId",
                table: "WarehouseLoadHeader",
                column: "InLoadHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoadHeader_OutLoadHeaderId",
                table: "WarehouseLoadHeader",
                column: "OutLoadHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoadHeader_OutWarehouseId",
                table: "WarehouseLoadHeader",
                column: "OutWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Machine_AutoConsumptionWarehouseId",
                table: "Machine",
                column: "AutoConsumptionWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machine_Warehouse_AutoConsumptionWarehouseId",
                table: "Machine",
                column: "AutoConsumptionWarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLoadHeader_Warehouse_OutWarehouseId",
                table: "WarehouseLoadHeader",
                column: "OutWarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLoadHeader_WarehouseLoadHeader_InLoadHeaderId",
                table: "WarehouseLoadHeader",
                column: "InLoadHeaderId",
                principalTable: "WarehouseLoadHeader",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLoadHeader_WarehouseLoadHeader_OutLoadHeaderId",
                table: "WarehouseLoadHeader",
                column: "OutLoadHeaderId",
                principalTable: "WarehouseLoadHeader",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machine_Warehouse_AutoConsumptionWarehouseId",
                table: "Machine");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLoadHeader_Warehouse_OutWarehouseId",
                table: "WarehouseLoadHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLoadHeader_WarehouseLoadHeader_InLoadHeaderId",
                table: "WarehouseLoadHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLoadHeader_WarehouseLoadHeader_OutLoadHeaderId",
                table: "WarehouseLoadHeader");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseLoadHeader_InLoadHeaderId",
                table: "WarehouseLoadHeader");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseLoadHeader_OutLoadHeaderId",
                table: "WarehouseLoadHeader");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseLoadHeader_OutWarehouseId",
                table: "WarehouseLoadHeader");

            migrationBuilder.DropIndex(
                name: "IX_Machine_AutoConsumptionWarehouseId",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "InLoadHeaderId",
                table: "WarehouseLoadHeader");

            migrationBuilder.DropColumn(
                name: "IsGenerated",
                table: "WarehouseLoadHeader");

            migrationBuilder.DropColumn(
                name: "OutLoadHeaderId",
                table: "WarehouseLoadHeader");

            migrationBuilder.DropColumn(
                name: "OutWarehouseId",
                table: "WarehouseLoadHeader");

            migrationBuilder.DropColumn(
                name: "AutoConsumptionWarehouseId",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "IsAutoConsumption",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Employee");
        }
    }
}
