using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class PlantPrintFilesExt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantPrintFile_Department_DepartmentId",
                table: "PlantPrintFile");

            migrationBuilder.DropIndex(
                name: "IX_PlantPrintFile_DepartmentId",
                table: "PlantPrintFile");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "PlantPrintFile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "PlantPrintFile",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlantPrintFile_DepartmentId",
                table: "PlantPrintFile",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantPrintFile_Department_DepartmentId",
                table: "PlantPrintFile",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");
        }
    }
}
