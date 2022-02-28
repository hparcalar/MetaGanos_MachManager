using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MachManager.Migrations
{
    public partial class CreditConsumings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DealerId",
                table: "CreditLoadHistory",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemCategoryId",
                table: "CreditLoadHistory",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeCredit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActiveCredit = table.Column<int>(type: "integer", nullable: false),
                    ItemCategoryId = table.Column<int>(type: "integer", nullable: true),
                    ItemGroupId = table.Column<int>(type: "integer", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCredit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeCredit_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeCredit_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeCredit_ItemCategory_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeCredit_ItemGroup_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCreditConsume",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConsumedCredit = table.Column<int>(type: "integer", nullable: false),
                    ItemCategoryId = table.Column<int>(type: "integer", nullable: true),
                    ItemGroupId = table.Column<int>(type: "integer", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true),
                    ConsumedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCreditConsume", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeCreditConsume_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeCreditConsume_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeCreditConsume_ItemCategory_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeCreditConsume_ItemGroup_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditLoadHistory_DealerId",
                table: "CreditLoadHistory",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditLoadHistory_ItemCategoryId",
                table: "CreditLoadHistory",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCredit_EmployeeId",
                table: "EmployeeCredit",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCredit_ItemCategoryId",
                table: "EmployeeCredit",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCredit_ItemGroupId",
                table: "EmployeeCredit",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCredit_ItemId",
                table: "EmployeeCredit",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCreditConsume_EmployeeId",
                table: "EmployeeCreditConsume",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCreditConsume_ItemCategoryId",
                table: "EmployeeCreditConsume",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCreditConsume_ItemGroupId",
                table: "EmployeeCreditConsume",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCreditConsume_ItemId",
                table: "EmployeeCreditConsume",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditLoadHistory_Dealer_DealerId",
                table: "CreditLoadHistory",
                column: "DealerId",
                principalTable: "Dealer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditLoadHistory_ItemCategory_ItemCategoryId",
                table: "CreditLoadHistory",
                column: "ItemCategoryId",
                principalTable: "ItemCategory",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditLoadHistory_Dealer_DealerId",
                table: "CreditLoadHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditLoadHistory_ItemCategory_ItemCategoryId",
                table: "CreditLoadHistory");

            migrationBuilder.DropTable(
                name: "EmployeeCredit");

            migrationBuilder.DropTable(
                name: "EmployeeCreditConsume");

            migrationBuilder.DropIndex(
                name: "IX_CreditLoadHistory_DealerId",
                table: "CreditLoadHistory");

            migrationBuilder.DropIndex(
                name: "IX_CreditLoadHistory_ItemCategoryId",
                table: "CreditLoadHistory");

            migrationBuilder.DropColumn(
                name: "DealerId",
                table: "CreditLoadHistory");

            migrationBuilder.DropColumn(
                name: "ItemCategoryId",
                table: "CreditLoadHistory");
        }
    }
}
