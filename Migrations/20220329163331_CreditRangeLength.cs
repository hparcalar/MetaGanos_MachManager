using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class CreditRangeLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditRangeLength",
                table: "ItemCategory",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RangeLength",
                table: "EmployeeCredit",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditRangeLength",
                table: "ItemCategory");

            migrationBuilder.DropColumn(
                name: "RangeLength",
                table: "EmployeeCredit");
        }
    }
}
