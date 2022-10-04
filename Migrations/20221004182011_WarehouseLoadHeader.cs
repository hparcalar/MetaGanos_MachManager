using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MachManager.Migrations
{
    public partial class WarehouseLoadHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseLoadHeaderId",
                table: "WarehouseLoad",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WarehouseLoadHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LoadDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReceiptNo = table.Column<string>(type: "text", nullable: true),
                    DocumentNo = table.Column<string>(type: "text", nullable: true),
                    LoadType = table.Column<int>(type: "integer", nullable: true),
                    FirmId = table.Column<int>(type: "integer", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    WarehouseId = table.Column<int>(type: "integer", nullable: true),
                    LoadOfficerId = table.Column<int>(type: "integer", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseLoadHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseLoadHeader_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseLoadHeader_Officer_LoadOfficerId",
                        column: x => x.LoadOfficerId,
                        principalTable: "Officer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseLoadHeader_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseLoadHeader_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoad_WarehouseLoadHeaderId",
                table: "WarehouseLoad",
                column: "WarehouseLoadHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoadHeader_FirmId",
                table: "WarehouseLoadHeader",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoadHeader_LoadOfficerId",
                table: "WarehouseLoadHeader",
                column: "LoadOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoadHeader_PlantId",
                table: "WarehouseLoadHeader",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoadHeader_WarehouseId",
                table: "WarehouseLoadHeader",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLoad_WarehouseLoadHeader_WarehouseLoadHeaderId",
                table: "WarehouseLoad",
                column: "WarehouseLoadHeaderId",
                principalTable: "WarehouseLoadHeader",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLoad_WarehouseLoadHeader_WarehouseLoadHeaderId",
                table: "WarehouseLoad");

            migrationBuilder.DropTable(
                name: "WarehouseLoadHeader");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseLoad_WarehouseLoadHeaderId",
                table: "WarehouseLoad");

            migrationBuilder.DropColumn(
                name: "WarehouseLoadHeaderId",
                table: "WarehouseLoad");
        }
    }
}
