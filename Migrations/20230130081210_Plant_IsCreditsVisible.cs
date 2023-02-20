using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class Plant_IsCreditsVisible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCreditsVisible",
                table: "Plant",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCreditsVisible",
                table: "Plant");
        }
    }
}
