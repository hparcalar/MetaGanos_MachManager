using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MachManager.Migrations
{
    public partial class WarehouseHotSalesCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WarehouseHotSalesCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemCategoryId = table.Column<int>(type: "integer", nullable: true),
                    ItemGroupId = table.Column<int>(type: "integer", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    WarehouseId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseHotSalesCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseHotSalesCategory_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseHotSalesCategory_ItemCategory_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseHotSalesCategory_ItemGroup_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseHotSalesCategory_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseHotSalesCategory_ItemCategoryId",
                table: "WarehouseHotSalesCategory",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseHotSalesCategory_ItemGroupId",
                table: "WarehouseHotSalesCategory",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseHotSalesCategory_ItemId",
                table: "WarehouseHotSalesCategory",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseHotSalesCategory_WarehouseId",
                table: "WarehouseHotSalesCategory",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseHotSalesCategory");
        }
    }
}
