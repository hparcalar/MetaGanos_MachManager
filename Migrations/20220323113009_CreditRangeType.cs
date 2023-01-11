using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class CreditRangeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditRangeType",
                table: "ItemCategory",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditRangeType",
                table: "DepartmentItemCategory",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditRangeType",
                table: "ItemCategory");

            migrationBuilder.DropColumn(
                name: "CreditRangeType",
                table: "DepartmentItemCategory");
        }
    }
}
