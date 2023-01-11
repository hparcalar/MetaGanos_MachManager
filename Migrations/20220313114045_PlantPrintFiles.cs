using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MachManager.Migrations
{
    public partial class PlantPrintFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlantLogo",
                table: "Plant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlantPrintFileId",
                table: "Department",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlantPrintFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrintFileCode = table.Column<string>(type: "text", nullable: true),
                    PrintFileName = table.Column<string>(type: "text", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantPrintFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantPrintFile_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlantPrintFile_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantFileProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    PlantPrintFileId = table.Column<int>(type: "integer", nullable: true),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true),
                    ProcessStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantFileProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantFileProcess_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlantFileProcess_PlantPrintFile_PlantPrintFileId",
                        column: x => x.PlantPrintFileId,
                        principalTable: "PlantPrintFile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Department_PlantPrintFileId",
                table: "Department",
                column: "PlantPrintFileId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantFileProcess_DepartmentId",
                table: "PlantFileProcess",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantFileProcess_PlantPrintFileId",
                table: "PlantFileProcess",
                column: "PlantPrintFileId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantPrintFile_DepartmentId",
                table: "PlantPrintFile",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantPrintFile_PlantId",
                table: "PlantPrintFile",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_PlantPrintFile_PlantPrintFileId",
                table: "Department",
                column: "PlantPrintFileId",
                principalTable: "PlantPrintFile",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_PlantPrintFile_PlantPrintFileId",
                table: "Department");

            migrationBuilder.DropTable(
                name: "PlantFileProcess");

            migrationBuilder.DropTable(
                name: "PlantPrintFile");

            migrationBuilder.DropIndex(
                name: "IX_Department_PlantPrintFileId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "PlantLogo",
                table: "Plant");

            migrationBuilder.DropColumn(
                name: "PlantPrintFileId",
                table: "Department");
        }
    }
}
