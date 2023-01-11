using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class CreditByRange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditByRange",
                table: "ItemCategory",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditByRange",
                table: "DepartmentItemCategory",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditByRange",
                table: "ItemCategory");

            migrationBuilder.DropColumn(
                name: "CreditByRange",
                table: "DepartmentItemCategory");
        }
    }
}
