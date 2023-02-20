using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MachManager.Migrations
{
    public partial class LicenseKeyData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastSelfValidation",
                table: "Dealer",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LicenseKey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LicenseCode = table.Column<string>(type: "text", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    DealerCode = table.Column<string>(type: "text", nullable: true),
                    DealerName = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LastValidation = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseKey", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LicenseKey");

            migrationBuilder.DropColumn(
                name: "LastSelfValidation",
                table: "Dealer");
        }
    }
}
