using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MachManager.Migrations
{
    public partial class MachineItemConsume : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineItemConsume",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConsumedCount = table.Column<int>(type: "integer", nullable: false),
                    MachineId = table.Column<int>(type: "integer", nullable: true),
                    SpiralNo = table.Column<int>(type: "integer", nullable: true),
                    ItemGroupId = table.Column<int>(type: "integer", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true),
                    ConsumedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineItemConsume", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineItemConsume_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineItemConsume_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineItemConsume_ItemGroup_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineItemConsume_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineItemConsume_EmployeeId",
                table: "MachineItemConsume",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineItemConsume_ItemGroupId",
                table: "MachineItemConsume",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineItemConsume_ItemId",
                table: "MachineItemConsume",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineItemConsume_MachineId",
                table: "MachineItemConsume",
                column: "MachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineItemConsume");
        }
    }
}
