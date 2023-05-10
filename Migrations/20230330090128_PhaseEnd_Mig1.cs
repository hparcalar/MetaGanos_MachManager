using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MachManager.Migrations
{
    public partial class PhaseEnd_Mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemOrderId",
                table: "WarehouseLoadHeader",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemOrderDetailId",
                table: "WarehouseLoad",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "Item",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Complaint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComplaintCode = table.Column<string>(type: "text", nullable: true),
                    ComplaintDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    OwnerEmployeeId = table.Column<int>(type: "integer", nullable: true),
                    OwnerUserId = table.Column<int>(type: "integer", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    ComplaintStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complaint_Employee_OwnerEmployeeId",
                        column: x => x.OwnerEmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Complaint_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Complaint_Officer_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Officer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Complaint_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TargetDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    NotificationStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeNotification_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeNotification_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    ReceiptType = table.Column<int>(type: "integer", nullable: true),
                    ReceiptNo = table.Column<string>(type: "text", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FirmId = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    TaxTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    OverallTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    ItemOrderStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOrder_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOrder_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductRating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Rate = table.Column<int>(type: "integer", nullable: true),
                    RatingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRating_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductRating_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductRating_Officer_UserId",
                        column: x => x.UserId,
                        principalTable: "Officer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SysNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NotificationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NotificationType = table.Column<int>(type: "integer", nullable: true),
                    WarningType = table.Column<int>(type: "integer", nullable: true),
                    NotificationStatus = table.Column<int>(type: "integer", nullable: true),
                    GotoLink = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysNotification_Officer_UserId",
                        column: x => x.UserId,
                        principalTable: "Officer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SysNotification_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SysPublication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlantId = table.Column<int>(type: "integer", nullable: true),
                    PublicationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    WarningType = table.Column<int>(type: "integer", nullable: true),
                    Attachment = table.Column<byte[]>(type: "bytea", nullable: true),
                    AttachmentContentType = table.Column<string>(type: "text", nullable: true),
                    AttachmentFileName = table.Column<string>(type: "text", nullable: true),
                    PublicationStatus = table.Column<int>(type: "integer", nullable: true),
                    ReplaceWithHomeVideo = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysPublication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysPublication_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemOrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemOrderId = table.Column<int>(type: "integer", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: true),
                    LineNumber = table.Column<int>(type: "integer", nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    TaxRate = table.Column<decimal>(type: "numeric", nullable: true),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    TaxTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    OverallTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    ItemOrderStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOrderDetail_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemOrderDetail_ItemOrder_ItemOrderId",
                        column: x => x.ItemOrderId,
                        principalTable: "ItemOrder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoadHeader_ItemOrderId",
                table: "WarehouseLoadHeader",
                column: "ItemOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoad_ItemOrderDetailId",
                table: "WarehouseLoad",
                column: "ItemOrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_ItemId",
                table: "Complaint",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_OwnerEmployeeId",
                table: "Complaint",
                column: "OwnerEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_OwnerUserId",
                table: "Complaint",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_PlantId",
                table: "Complaint",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeNotification_EmployeeId",
                table: "EmployeeNotification",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeNotification_PlantId",
                table: "EmployeeNotification",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_FirmId",
                table: "ItemOrder",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_PlantId",
                table: "ItemOrder",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrderDetail_ItemId",
                table: "ItemOrderDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrderDetail_ItemOrderId",
                table: "ItemOrderDetail",
                column: "ItemOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRating_EmployeeId",
                table: "ProductRating",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRating_ItemId",
                table: "ProductRating",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRating_UserId",
                table: "ProductRating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SysNotification_PlantId",
                table: "SysNotification",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_SysNotification_UserId",
                table: "SysNotification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SysPublication_PlantId",
                table: "SysPublication",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLoad_ItemOrderDetail_ItemOrderDetailId",
                table: "WarehouseLoad",
                column: "ItemOrderDetailId",
                principalTable: "ItemOrderDetail",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLoadHeader_ItemOrder_ItemOrderId",
                table: "WarehouseLoadHeader",
                column: "ItemOrderId",
                principalTable: "ItemOrder",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLoad_ItemOrderDetail_ItemOrderDetailId",
                table: "WarehouseLoad");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLoadHeader_ItemOrder_ItemOrderId",
                table: "WarehouseLoadHeader");

            migrationBuilder.DropTable(
                name: "Complaint");

            migrationBuilder.DropTable(
                name: "EmployeeNotification");

            migrationBuilder.DropTable(
                name: "ItemOrderDetail");

            migrationBuilder.DropTable(
                name: "ProductRating");

            migrationBuilder.DropTable(
                name: "SysNotification");

            migrationBuilder.DropTable(
                name: "SysPublication");

            migrationBuilder.DropTable(
                name: "ItemOrder");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseLoadHeader_ItemOrderId",
                table: "WarehouseLoadHeader");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseLoad_ItemOrderDetailId",
                table: "WarehouseLoad");

            migrationBuilder.DropColumn(
                name: "ItemOrderId",
                table: "WarehouseLoadHeader");

            migrationBuilder.DropColumn(
                name: "ItemOrderDetailId",
                table: "WarehouseLoad");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Item");
        }
    }
}
