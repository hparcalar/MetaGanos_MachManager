using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class DepartmentCredit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentCredit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveCredit = table.Column<int>(type: "int", nullable: false),
                    ItemCategoryId = table.Column<int>(type: "int", nullable: true),
                    ItemGroupId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    RangeCredit = table.Column<int>(type: "int", nullable: false),
                    RangeIndex = table.Column<int>(type: "int", nullable: false),
                    RangeType = table.Column<int>(type: "int", nullable: false),
                    RangeLength = table.Column<int>(type: "int", nullable: false),
                    CreditByRange = table.Column<int>(type: "int", nullable: false),
                    ProductIntervalType = table.Column<int>(type: "int", nullable: true),
                    ProductIntervalTime = table.Column<int>(type: "int", nullable: true),
                    SpecificRangeDates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditLoadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreditStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreditEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentCredit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentCredit_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentCredit_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentCredit_ItemCategory_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentCredit_ItemGroup_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentCredit_DepartmentId",
                table: "DepartmentCredit",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentCredit_ItemCategoryId",
                table: "DepartmentCredit",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentCredit_ItemGroupId",
                table: "DepartmentCredit",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentCredit_ItemId",
                table: "DepartmentCredit",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentCredit");
        }
    }
}
