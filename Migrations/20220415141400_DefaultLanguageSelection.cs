using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MachManager.Migrations
{
    public partial class DefaultLanguageSelection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultLanguage",
                table: "Machine",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultLanguage",
                table: "Dealer",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Officer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlantId = table.Column<int>(type: "integer", nullable: false),
                    OfficerCode = table.Column<string>(type: "text", nullable: true),
                    OfficerName = table.Column<string>(type: "text", nullable: true),
                    OfficerPassword = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DefaultLanguage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Officer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Officer_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Officer_PlantId",
                table: "Officer",
                column: "PlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Officer");

            migrationBuilder.DropColumn(
                name: "DefaultLanguage",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "DefaultLanguage",
                table: "Dealer");
        }
    }
}
