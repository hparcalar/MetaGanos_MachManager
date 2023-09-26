using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class Notifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeenDate",
                table: "Notification",
                newName: "DeleteDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Notification",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Notification");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "Notification",
                newName: "SeenDate");
        }
    }
}
