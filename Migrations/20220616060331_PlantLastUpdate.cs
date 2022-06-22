using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class PlantLastUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Plant",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "Plant");
        }
    }
}
