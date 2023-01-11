using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class RangeOfCredits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreditEndDate",
                table: "EmployeeCredit",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreditLoadDate",
                table: "EmployeeCredit",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreditStartDate",
                table: "EmployeeCredit",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RangeCredit",
                table: "EmployeeCredit",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RangeIndex",
                table: "EmployeeCredit",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RangeType",
                table: "EmployeeCredit",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditEndDate",
                table: "EmployeeCredit");

            migrationBuilder.DropColumn(
                name: "CreditLoadDate",
                table: "EmployeeCredit");

            migrationBuilder.DropColumn(
                name: "CreditStartDate",
                table: "EmployeeCredit");

            migrationBuilder.DropColumn(
                name: "RangeCredit",
                table: "EmployeeCredit");

            migrationBuilder.DropColumn(
                name: "RangeIndex",
                table: "EmployeeCredit");

            migrationBuilder.DropColumn(
                name: "RangeType",
                table: "EmployeeCredit");
        }
    }
}
