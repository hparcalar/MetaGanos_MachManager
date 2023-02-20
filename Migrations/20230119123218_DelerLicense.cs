using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class DelerLicense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRoot",
                table: "Dealer",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LicenseKey",
                table: "Dealer",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRoot",
                table: "Dealer");

            migrationBuilder.DropColumn(
                name: "LicenseKey",
                table: "Dealer");
        }
    }
}
