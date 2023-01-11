using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class MissingRelationFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MachineSpiral_ItemId",
                table: "MachineSpiral",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentItemCategory_DepartmentId",
                table: "DepartmentItemCategory",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentItemCategory_ItemCategoryId",
                table: "DepartmentItemCategory",
                column: "ItemCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentItemCategory_Department_DepartmentId",
                table: "DepartmentItemCategory",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentItemCategory_ItemCategory_ItemCategoryId",
                table: "DepartmentItemCategory",
                column: "ItemCategoryId",
                principalTable: "ItemCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSpiral_Item_ItemId",
                table: "MachineSpiral",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentItemCategory_Department_DepartmentId",
                table: "DepartmentItemCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentItemCategory_ItemCategory_ItemCategoryId",
                table: "DepartmentItemCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineSpiral_Item_ItemId",
                table: "MachineSpiral");

            migrationBuilder.DropIndex(
                name: "IX_MachineSpiral_ItemId",
                table: "MachineSpiral");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentItemCategory_DepartmentId",
                table: "DepartmentItemCategory");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentItemCategory_ItemCategoryId",
                table: "DepartmentItemCategory");
        }
    }
}
