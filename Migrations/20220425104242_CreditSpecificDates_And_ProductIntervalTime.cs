using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class CreditSpecificDates_And_ProductIntervalTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductIntervalTime",
                table: "EmployeeCredit",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductIntervalType",
                table: "EmployeeCredit",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecificRangeDates",
                table: "EmployeeCredit",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductIntervalTime",
                table: "EmployeeCredit");

            migrationBuilder.DropColumn(
                name: "ProductIntervalType",
                table: "EmployeeCredit");

            migrationBuilder.DropColumn(
                name: "SpecificRangeDates",
                table: "EmployeeCredit");
        }
    }
}
