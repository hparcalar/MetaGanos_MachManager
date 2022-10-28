using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class InitMetaGanos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dealer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealerCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentDealerId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dealer_Dealer_ParentDealerId",
                        column: x => x.ParentDealerId,
                        principalTable: "Dealer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ForexType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ForexCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForexType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysLang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysLang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlantLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Last4CharForCardRead = table.Column<bool>(type: "bit", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DealerId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plant_Dealer_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SysLangDict",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SysLangId = table.Column<int>(type: "int", nullable: true),
                    ExpNo = table.Column<int>(type: "int", nullable: true),
                    Expression = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EqualResponse = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysLangDict", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysLangDict_SysLang_SysLangId",
                        column: x => x.SysLangId,
                        principalTable: "SysLang",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HexKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlantId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeCard_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Firm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirmName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionProtocol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirmLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DebitFormSamplePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlantId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Firm_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewOrder = table.Column<int>(type: "int", nullable: false),
                    ControlTimeType = table.Column<int>(type: "int", nullable: false),
                    ItemChangeTime = table.Column<int>(type: "int", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: true),
                    CreditRangeType = table.Column<int>(type: "int", nullable: true),
                    CreditRangeLength = table.Column<int>(type: "int", nullable: true),
                    CreditByRange = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCategory_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Machine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantId = table.Column<int>(type: "int", nullable: true),
                    MachineCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartVideoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialCustomer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InventoryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InventoryEntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LocationData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rows = table.Column<int>(type: "int", nullable: false),
                    Cols = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SpiralStartIndex = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DefaultLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machine_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Officer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    OfficerCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficerPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DefaultLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "PlantPrintFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrintFileCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrintFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantPrintFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantPrintFile_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerId = table.Column<int>(type: "int", nullable: true),
                    PlantId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouse_Dealer_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Warehouse_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemGroupCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewOrder = table.Column<int>(type: "int", nullable: false),
                    ItemCategoryId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GroupImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemGroup_ItemCategory_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuthUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficerId = table.Column<int>(type: "int", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanRead = table.Column<bool>(type: "bit", nullable: false),
                    CanWrite = table.Column<bool>(type: "bit", nullable: false),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthUnit_Officer_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "Officer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlantId = table.Column<int>(type: "int", nullable: true),
                    PlantPrintFileId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Department_PlantPrintFile_PlantPrintFileId",
                        column: x => x.PlantPrintFileId,
                        principalTable: "PlantPrintFile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemReceipt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptType = table.Column<int>(type: "int", nullable: false),
                    ReceiptNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirmId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    DocumentNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DealerId = table.Column<int>(type: "int", nullable: true),
                    PlantId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemReceipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemReceipt_Dealer_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceipt_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceipt_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceipt_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WarehouseLoadHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceiptNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoadType = table.Column<int>(type: "int", nullable: true),
                    FirmId = table.Column<int>(type: "int", nullable: true),
                    PlantId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    LoadOfficerId = table.Column<int>(type: "int", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseLoadHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseLoadHeader_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseLoadHeader_Officer_LoadOfficerId",
                        column: x => x.LoadOfficerId,
                        principalTable: "Officer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseLoadHeader_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseLoadHeader_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternatingCode1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternatingCode2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriticalMax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CriticalMin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewOrder = table.Column<int>(type: "int", nullable: false),
                    ItemGroupId = table.Column<int>(type: "int", nullable: true),
                    ItemCategoryId = table.Column<int>(type: "int", nullable: true),
                    UnitTypeId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_ItemCategory_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_ItemGroup_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_UnitType_UnitTypeId",
                        column: x => x.UnitTypeId,
                        principalTable: "UnitType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpiralFace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCategoryId = table.Column<int>(type: "int", nullable: true),
                    ItemGroupId = table.Column<int>(type: "int", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpiralFace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpiralFace_ItemCategory_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpiralFace_ItemGroup_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DepartmentItemCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    ItemCategoryId = table.Column<int>(type: "int", nullable: true),
                    CreditRangeType = table.Column<int>(type: "int", nullable: true),
                    CreditByRange = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentItemCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentItemCategory_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentItemCategory_ItemCategory_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DepartmentMachine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    MachineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentMachine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentMachine_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentMachine_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeePassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gsm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveCredit = table.Column<int>(type: "int", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    EmployeeCardId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_EmployeeCard_EmployeeCardId",
                        column: x => x.EmployeeCardId,
                        principalTable: "EmployeeCard",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemReceiptDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineNumber = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    ItemReceiptId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VisualQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ForexId = table.Column<int>(type: "int", nullable: true),
                    ForexRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MachineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemReceiptDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemReceiptDetail_ForexType_ForexId",
                        column: x => x.ForexId,
                        principalTable: "ForexType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceiptDetail_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceiptDetail_ItemReceipt_ItemReceiptId",
                        column: x => x.ItemReceiptId,
                        principalTable: "ItemReceipt",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemReceiptDetail_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MachineSpiral",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosX = table.Column<int>(type: "int", nullable: true),
                    PosY = table.Column<int>(type: "int", nullable: true),
                    PosOrders = table.Column<int>(type: "int", nullable: true),
                    ActiveQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MachineId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    ItemCategoryId = table.Column<int>(type: "int", nullable: true),
                    ItemGroupId = table.Column<int>(type: "int", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: true),
                    IsInFault = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineSpiral", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineSpiral_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineSpiral_ItemCategory_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineSpiral_ItemGroup_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineSpiral_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MachineSpiralLoad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SpiralNo = table.Column<int>(type: "int", nullable: true),
                    MachineId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    OfficerId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineSpiralLoad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineSpiralLoad_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineSpiralLoad_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineSpiralLoad_Officer_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "Officer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineSpiralLoad_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WarehouseHotSalesCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCategoryId = table.Column<int>(type: "int", nullable: true),
                    ItemGroupId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "WarehouseLoad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoadType = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WarehouseLoadHeaderId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    OfficerId = table.Column<int>(type: "int", nullable: true),
                    MachineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseLoad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseLoad_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseLoad_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseLoad_Officer_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "Officer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseLoad_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseLoad_WarehouseLoadHeader_WarehouseLoadHeaderId",
                        column: x => x.WarehouseLoadHeaderId,
                        principalTable: "WarehouseLoadHeader",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CreditLoadHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    EmployeeCardId = table.Column<int>(type: "int", nullable: true),
                    ItemCategoryId = table.Column<int>(type: "int", nullable: true),
                    DealerId = table.Column<int>(type: "int", nullable: true),
                    LoadedCredits = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditLoadHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditLoadHistory_Dealer_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CreditLoadHistory_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CreditLoadHistory_EmployeeCard_EmployeeCardId",
                        column: x => x.EmployeeCardId,
                        principalTable: "EmployeeCard",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CreditLoadHistory_ItemCategory_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCardMatchHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    EmployeeCardId = table.Column<int>(type: "int", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCardMatchHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeCardMatchHistory_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeCardMatchHistory_EmployeeCard_EmployeeCardId",
                        column: x => x.EmployeeCardId,
                        principalTable: "EmployeeCard",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCredit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveCredit = table.Column<int>(type: "int", nullable: false),
                    ItemCategoryId = table.Column<int>(type: "int", nullable: true),
                    ItemGroupId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    RangeCredit = table.Column<int>(type: "int", nullable: false),
                    RangeIndex = table.Column<int>(type: "int", nullable: false),
                    RangeType = table.Column<int>(type: "int", nullable: false),
                    RangeLength = table.Column<int>(type: "int", nullable: false),
                    CreditByRange = table.Column<int>(type: "int", nullable: false),
                    ProductIntervalType = table.Column<int>(type: "int", nullable: true),
                    ProductIntervalTime = table.Column<int>(type: "int", nullable: true),
                    SpecificRangeDates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditLoadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreditStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreditEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsumedCredit = table.Column<int>(type: "int", nullable: false),
                    ItemCategoryId = table.Column<int>(type: "int", nullable: true),
                    ItemGroupId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    ConsumedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "MachineItemConsume",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsumedCount = table.Column<int>(type: "int", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: true),
                    SpiralNo = table.Column<int>(type: "int", nullable: true),
                    ItemGroupId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    ConsumedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineItemConsume", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineItemConsume_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineItemConsume_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineItemConsume_ItemGroup_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineItemConsume_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineItemConsume_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlantFileProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlantPrintFileId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    ProcessStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                        name: "FK_PlantFileProcess_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlantFileProcess_PlantPrintFile_PlantPrintFileId",
                        column: x => x.PlantPrintFileId,
                        principalTable: "PlantPrintFile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthUnit_OfficerId",
                table: "AuthUnit",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditLoadHistory_DealerId",
                table: "CreditLoadHistory",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditLoadHistory_EmployeeCardId",
                table: "CreditLoadHistory",
                column: "EmployeeCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditLoadHistory_EmployeeId",
                table: "CreditLoadHistory",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditLoadHistory_ItemCategoryId",
                table: "CreditLoadHistory",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Dealer_ParentDealerId",
                table: "Dealer",
                column: "ParentDealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_PlantId",
                table: "Department",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_PlantPrintFileId",
                table: "Department",
                column: "PlantPrintFileId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentItemCategory_DepartmentId",
                table: "DepartmentItemCategory",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentItemCategory_ItemCategoryId",
                table: "DepartmentItemCategory",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMachine_DepartmentId",
                table: "DepartmentMachine",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMachine_MachineId",
                table: "DepartmentMachine",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeCardId",
                table: "Employee",
                column: "EmployeeCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PlantId",
                table: "Employee",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCard_PlantId",
                table: "EmployeeCard",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCardMatchHistory_EmployeeCardId",
                table: "EmployeeCardMatchHistory",
                column: "EmployeeCardId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCardMatchHistory_EmployeeId",
                table: "EmployeeCardMatchHistory",
                column: "EmployeeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Firm_PlantId",
                table: "Firm",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemCategoryId",
                table: "Item",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemGroupId",
                table: "Item",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_UnitTypeId",
                table: "Item",
                column: "UnitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategory_PlantId",
                table: "ItemCategory",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroup_ItemCategoryId",
                table: "ItemGroup",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceipt_DealerId",
                table: "ItemReceipt",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceipt_FirmId",
                table: "ItemReceipt",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceipt_PlantId",
                table: "ItemReceipt",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceipt_WarehouseId",
                table: "ItemReceipt",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptDetail_ForexId",
                table: "ItemReceiptDetail",
                column: "ForexId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptDetail_ItemId",
                table: "ItemReceiptDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptDetail_ItemReceiptId",
                table: "ItemReceiptDetail",
                column: "ItemReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReceiptDetail_MachineId",
                table: "ItemReceiptDetail",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Machine_PlantId",
                table: "Machine",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineItemConsume_EmployeeId",
                table: "MachineItemConsume",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineItemConsume_ItemGroupId",
                table: "MachineItemConsume",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineItemConsume_ItemId",
                table: "MachineItemConsume",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineItemConsume_MachineId",
                table: "MachineItemConsume",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineItemConsume_WarehouseId",
                table: "MachineItemConsume",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSpiral_ItemCategoryId",
                table: "MachineSpiral",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSpiral_ItemGroupId",
                table: "MachineSpiral",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSpiral_ItemId",
                table: "MachineSpiral",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSpiral_MachineId",
                table: "MachineSpiral",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSpiralLoad_ItemId",
                table: "MachineSpiralLoad",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSpiralLoad_MachineId",
                table: "MachineSpiralLoad",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSpiralLoad_OfficerId",
                table: "MachineSpiralLoad",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSpiralLoad_WarehouseId",
                table: "MachineSpiralLoad",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Officer_PlantId",
                table: "Officer",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_DealerId",
                table: "Plant",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantFileProcess_DepartmentId",
                table: "PlantFileProcess",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantFileProcess_EmployeeId",
                table: "PlantFileProcess",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantFileProcess_PlantPrintFileId",
                table: "PlantFileProcess",
                column: "PlantPrintFileId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantPrintFile_PlantId",
                table: "PlantPrintFile",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_SpiralFace_ItemCategoryId",
                table: "SpiralFace",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SpiralFace_ItemGroupId",
                table: "SpiralFace",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SysLangDict_SysLangId",
                table: "SysLangDict",
                column: "SysLangId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_DealerId",
                table: "Warehouse",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_PlantId",
                table: "Warehouse",
                column: "PlantId");

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

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoad_ItemId",
                table: "WarehouseLoad",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoad_MachineId",
                table: "WarehouseLoad",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoad_OfficerId",
                table: "WarehouseLoad",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoad_WarehouseId",
                table: "WarehouseLoad",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoad_WarehouseLoadHeaderId",
                table: "WarehouseLoad",
                column: "WarehouseLoadHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoadHeader_FirmId",
                table: "WarehouseLoadHeader",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoadHeader_LoadOfficerId",
                table: "WarehouseLoadHeader",
                column: "LoadOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoadHeader_PlantId",
                table: "WarehouseLoadHeader",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLoadHeader_WarehouseId",
                table: "WarehouseLoadHeader",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthUnit");

            migrationBuilder.DropTable(
                name: "CreditLoadHistory");

            migrationBuilder.DropTable(
                name: "DepartmentItemCategory");

            migrationBuilder.DropTable(
                name: "DepartmentMachine");

            migrationBuilder.DropTable(
                name: "EmployeeCardMatchHistory");

            migrationBuilder.DropTable(
                name: "EmployeeCredit");

            migrationBuilder.DropTable(
                name: "EmployeeCreditConsume");

            migrationBuilder.DropTable(
                name: "ItemReceiptDetail");

            migrationBuilder.DropTable(
                name: "MachineItemConsume");

            migrationBuilder.DropTable(
                name: "MachineSpiral");

            migrationBuilder.DropTable(
                name: "MachineSpiralLoad");

            migrationBuilder.DropTable(
                name: "PlantFileProcess");

            migrationBuilder.DropTable(
                name: "SpiralFace");

            migrationBuilder.DropTable(
                name: "SysLangDict");

            migrationBuilder.DropTable(
                name: "WarehouseHotSalesCategory");

            migrationBuilder.DropTable(
                name: "WarehouseLoad");

            migrationBuilder.DropTable(
                name: "ForexType");

            migrationBuilder.DropTable(
                name: "ItemReceipt");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "SysLang");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Machine");

            migrationBuilder.DropTable(
                name: "WarehouseLoadHeader");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "EmployeeCard");

            migrationBuilder.DropTable(
                name: "ItemGroup");

            migrationBuilder.DropTable(
                name: "UnitType");

            migrationBuilder.DropTable(
                name: "Firm");

            migrationBuilder.DropTable(
                name: "Officer");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "PlantPrintFile");

            migrationBuilder.DropTable(
                name: "ItemCategory");

            migrationBuilder.DropTable(
                name: "Plant");

            migrationBuilder.DropTable(
                name: "Dealer");
        }
    }
}
