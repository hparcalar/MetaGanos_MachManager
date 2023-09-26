using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MachManager.Migrations
{
    public partial class warehouseReturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Return",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    WarehouseId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Return", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Return_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Return_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Return_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Return_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Return_EmployeeId",
                table: "Return",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Return_ItemId",
                table: "Return",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Return_PlantId",
                table: "Return",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Return_WarehouseId",
                table: "Return",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Return");
        }
    }
}
