using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MachManager.Migrations
{
    public partial class MachineSpiralLoad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineSpiralLoad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LoadDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SpiralNo = table.Column<int>(type: "integer", nullable: true),
                    MachineId = table.Column<int>(type: "integer", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    OfficerId = table.Column<int>(type: "integer", nullable: true),
                    WarehouseId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineSpiralLoad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineSpiralLoad_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineSpiralLoad_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineSpiralLoad_Officer_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "Officer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineSpiralLoad_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineSpiralLoad_ItemId",
                table: "MachineSpiralLoad",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSpiralLoad_MachineId",
                table: "MachineSpiralLoad",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSpiralLoad_OfficerId",
                table: "MachineSpiralLoad",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSpiralLoad_WarehouseId",
                table: "MachineSpiralLoad",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineSpiralLoad");
        }
    }
}
