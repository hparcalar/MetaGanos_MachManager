using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachManager.Migrations
{
    public partial class ExpNoForDictionary_i18n : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditLoadHistory_Employee_EmployeeId",
                table: "CreditLoadHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditLoadHistory_EmployeeCard_EmployeeCardId",
                table: "CreditLoadHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Plant_PlantId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_EmployeeCard_EmployeeCardId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Plant_PlantId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeCard_Plant_PlantId",
                table: "EmployeeCard");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeCardMatchHistory_Employee_EmployeeId",
                table: "EmployeeCardMatchHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeCardMatchHistory_EmployeeCard_EmployeeCardId",
                table: "EmployeeCardMatchHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemCategory_ItemCategoryId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_UnitType_UnitTypeId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemGroup_ItemCategory_ItemCategoryId",
                table: "ItemGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceipt_Dealer_DealerId",
                table: "ItemReceipt");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceipt_Firm_FirmId",
                table: "ItemReceipt");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceipt_Plant_PlantId",
                table: "ItemReceipt");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceipt_Warehouse_WarehouseId",
                table: "ItemReceipt");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceiptDetail_Item_ItemId",
                table: "ItemReceiptDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceiptDetail_ItemReceipt_ItemReceiptId",
                table: "ItemReceiptDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceiptDetail_Machine_MachineId",
                table: "ItemReceiptDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Machine_Plant_PlantId",
                table: "Machine");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineSpiral_ItemCategory_ItemCategoryId",
                table: "MachineSpiral");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineSpiral_ItemGroup_ItemGroupId",
                table: "MachineSpiral");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineSpiral_Machine_MachineId",
                table: "MachineSpiral");

            migrationBuilder.DropForeignKey(
                name: "FK_SysLangDict_SysLang_SysLangId",
                table: "SysLangDict");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Dealer_DealerId",
                table: "Warehouse");

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseName",
                table: "Warehouse",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseCode",
                table: "Warehouse",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "DealerId",
                table: "Warehouse",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "UnitTypeName",
                table: "UnitType",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UnitTypeCode",
                table: "UnitType",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "SysLangId",
                table: "SysLangDict",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Expression",
                table: "SysLangDict",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "EqualResponse",
                table: "SysLangDict",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "ExpNo",
                table: "SysLangDict",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LanguageName",
                table: "SysLang",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LanguageCode",
                table: "SysLang",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PlantName",
                table: "Plant",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PlantCode",
                table: "Plant",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Explanation",
                table: "Plant",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "MachineSpiral",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ItemGroupId",
                table: "MachineSpiral",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ItemCategoryId",
                table: "MachineSpiral",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "StartVideoPath",
                table: "Machine",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "SpecialCustomer",
                table: "Machine",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "Machine",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "MachineName",
                table: "Machine",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "MachineCode",
                table: "Machine",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LocationData",
                table: "Machine",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "InventoryCode",
                table: "Machine",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "District",
                table: "Machine",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Machine",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Machine",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BrandModel",
                table: "Machine",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Machine",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Machine",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "ItemReceiptDetail",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ItemReceiptId",
                table: "ItemReceiptDetail",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "ItemReceiptDetail",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "ItemReceipt",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNo",
                table: "ItemReceipt",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "ItemReceipt",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "FirmId",
                table: "ItemReceipt",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNo",
                table: "ItemReceipt",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "DealerId",
                table: "ItemReceipt",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "ItemGroupName",
                table: "ItemGroup",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ItemGroupCode",
                table: "ItemGroup",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "ItemCategoryId",
                table: "ItemGroup",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "ItemCategoryName",
                table: "ItemCategory",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ItemCategoryCode",
                table: "ItemCategory",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "UnitTypeId",
                table: "Item",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "ItemName",
                table: "Item",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "ItemGroupId",
                table: "Item",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "ItemCode",
                table: "Item",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "ItemCategoryId",
                table: "Item",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Explanation",
                table: "Item",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode2",
                table: "Item",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode1",
                table: "Item",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AlternatingCode2",
                table: "Item",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AlternatingCode1",
                table: "Item",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ForexCode",
                table: "ForexType",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Explanation",
                table: "ForexType",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirmName",
                table: "Firm",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirmLogo",
                table: "Firm",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirmCode",
                table: "Firm",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DebitFormSamplePath",
                table: "Firm",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ConnectionProtocol",
                table: "Firm",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Explanation",
                table: "EmployeeCardMatchHistory",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeCardMatchHistory",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeCardId",
                table: "EmployeeCardMatchHistory",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "EmployeeCard",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "HexKey",
                table: "EmployeeCard",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CardCode",
                table: "EmployeeCard",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "Employee",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Gsm",
                table: "Employee",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeName",
                table: "Employee",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeCode",
                table: "Employee",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeCardId",
                table: "Employee",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employee",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Employee",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "Department",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentName",
                table: "Department",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentCode",
                table: "Department",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Explanation",
                table: "Dealer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DealerName",
                table: "Dealer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DealerCode",
                table: "Dealer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "CreditLoadHistory",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeCardId",
                table: "CreditLoadHistory",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditLoadHistory_Employee_EmployeeId",
                table: "CreditLoadHistory",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditLoadHistory_EmployeeCard_EmployeeCardId",
                table: "CreditLoadHistory",
                column: "EmployeeCardId",
                principalTable: "EmployeeCard",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Plant_PlantId",
                table: "Department",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                table: "Employee",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_EmployeeCard_EmployeeCardId",
                table: "Employee",
                column: "EmployeeCardId",
                principalTable: "EmployeeCard",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Plant_PlantId",
                table: "Employee",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeCard_Plant_PlantId",
                table: "EmployeeCard",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeCardMatchHistory_Employee_EmployeeId",
                table: "EmployeeCardMatchHistory",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeCardMatchHistory_EmployeeCard_EmployeeCardId",
                table: "EmployeeCardMatchHistory",
                column: "EmployeeCardId",
                principalTable: "EmployeeCard",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemCategory_ItemCategoryId",
                table: "Item",
                column: "ItemCategoryId",
                principalTable: "ItemCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                table: "Item",
                column: "ItemGroupId",
                principalTable: "ItemGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_UnitType_UnitTypeId",
                table: "Item",
                column: "UnitTypeId",
                principalTable: "UnitType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGroup_ItemCategory_ItemCategoryId",
                table: "ItemGroup",
                column: "ItemCategoryId",
                principalTable: "ItemCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceipt_Dealer_DealerId",
                table: "ItemReceipt",
                column: "DealerId",
                principalTable: "Dealer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceipt_Firm_FirmId",
                table: "ItemReceipt",
                column: "FirmId",
                principalTable: "Firm",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceipt_Plant_PlantId",
                table: "ItemReceipt",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceipt_Warehouse_WarehouseId",
                table: "ItemReceipt",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceiptDetail_Item_ItemId",
                table: "ItemReceiptDetail",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceiptDetail_ItemReceipt_ItemReceiptId",
                table: "ItemReceiptDetail",
                column: "ItemReceiptId",
                principalTable: "ItemReceipt",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceiptDetail_Machine_MachineId",
                table: "ItemReceiptDetail",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Machine_Plant_PlantId",
                table: "Machine",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSpiral_ItemCategory_ItemCategoryId",
                table: "MachineSpiral",
                column: "ItemCategoryId",
                principalTable: "ItemCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSpiral_ItemGroup_ItemGroupId",
                table: "MachineSpiral",
                column: "ItemGroupId",
                principalTable: "ItemGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSpiral_Machine_MachineId",
                table: "MachineSpiral",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SysLangDict_SysLang_SysLangId",
                table: "SysLangDict",
                column: "SysLangId",
                principalTable: "SysLang",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Dealer_DealerId",
                table: "Warehouse",
                column: "DealerId",
                principalTable: "Dealer",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditLoadHistory_Employee_EmployeeId",
                table: "CreditLoadHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditLoadHistory_EmployeeCard_EmployeeCardId",
                table: "CreditLoadHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Plant_PlantId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_EmployeeCard_EmployeeCardId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Plant_PlantId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeCard_Plant_PlantId",
                table: "EmployeeCard");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeCardMatchHistory_Employee_EmployeeId",
                table: "EmployeeCardMatchHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeCardMatchHistory_EmployeeCard_EmployeeCardId",
                table: "EmployeeCardMatchHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemCategory_ItemCategoryId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_UnitType_UnitTypeId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemGroup_ItemCategory_ItemCategoryId",
                table: "ItemGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceipt_Dealer_DealerId",
                table: "ItemReceipt");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceipt_Firm_FirmId",
                table: "ItemReceipt");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceipt_Plant_PlantId",
                table: "ItemReceipt");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceipt_Warehouse_WarehouseId",
                table: "ItemReceipt");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceiptDetail_Item_ItemId",
                table: "ItemReceiptDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceiptDetail_ItemReceipt_ItemReceiptId",
                table: "ItemReceiptDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemReceiptDetail_Machine_MachineId",
                table: "ItemReceiptDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Machine_Plant_PlantId",
                table: "Machine");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineSpiral_ItemCategory_ItemCategoryId",
                table: "MachineSpiral");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineSpiral_ItemGroup_ItemGroupId",
                table: "MachineSpiral");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineSpiral_Machine_MachineId",
                table: "MachineSpiral");

            migrationBuilder.DropForeignKey(
                name: "FK_SysLangDict_SysLang_SysLangId",
                table: "SysLangDict");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Dealer_DealerId",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ExpNo",
                table: "SysLangDict");

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseName",
                table: "Warehouse",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseCode",
                table: "Warehouse",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DealerId",
                table: "Warehouse",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnitTypeName",
                table: "UnitType",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnitTypeCode",
                table: "UnitType",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SysLangId",
                table: "SysLangDict",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Expression",
                table: "SysLangDict",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EqualResponse",
                table: "SysLangDict",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LanguageName",
                table: "SysLang",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LanguageCode",
                table: "SysLang",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlantName",
                table: "Plant",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlantCode",
                table: "Plant",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Explanation",
                table: "Plant",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "MachineSpiral",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemGroupId",
                table: "MachineSpiral",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemCategoryId",
                table: "MachineSpiral",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StartVideoPath",
                table: "Machine",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SpecialCustomer",
                table: "Machine",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "Machine",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MachineName",
                table: "Machine",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MachineCode",
                table: "Machine",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LocationData",
                table: "Machine",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InventoryCode",
                table: "Machine",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "District",
                table: "Machine",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Machine",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Machine",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BrandModel",
                table: "Machine",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Machine",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Machine",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "ItemReceiptDetail",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemReceiptId",
                table: "ItemReceiptDetail",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "ItemReceiptDetail",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "ItemReceipt",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNo",
                table: "ItemReceipt",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "ItemReceipt",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FirmId",
                table: "ItemReceipt",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNo",
                table: "ItemReceipt",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DealerId",
                table: "ItemReceipt",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemGroupName",
                table: "ItemGroup",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemGroupCode",
                table: "ItemGroup",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemCategoryId",
                table: "ItemGroup",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemCategoryName",
                table: "ItemCategory",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemCategoryCode",
                table: "ItemCategory",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnitTypeId",
                table: "Item",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemName",
                table: "Item",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemGroupId",
                table: "Item",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemCode",
                table: "Item",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemCategoryId",
                table: "Item",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Explanation",
                table: "Item",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Barcode2",
                table: "Item",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Barcode1",
                table: "Item",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AlternatingCode2",
                table: "Item",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AlternatingCode1",
                table: "Item",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ForexCode",
                table: "ForexType",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Explanation",
                table: "ForexType",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirmName",
                table: "Firm",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirmLogo",
                table: "Firm",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirmCode",
                table: "Firm",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DebitFormSamplePath",
                table: "Firm",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConnectionProtocol",
                table: "Firm",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Explanation",
                table: "EmployeeCardMatchHistory",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeCardMatchHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeCardId",
                table: "EmployeeCardMatchHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "EmployeeCard",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HexKey",
                table: "EmployeeCard",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardCode",
                table: "EmployeeCard",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "Employee",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gsm",
                table: "Employee",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeName",
                table: "Employee",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeCode",
                table: "Employee",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeCardId",
                table: "Employee",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employee",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Employee",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "Department",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentName",
                table: "Department",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentCode",
                table: "Department",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Explanation",
                table: "Dealer",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DealerName",
                table: "Dealer",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DealerCode",
                table: "Dealer",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "CreditLoadHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeCardId",
                table: "CreditLoadHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditLoadHistory_Employee_EmployeeId",
                table: "CreditLoadHistory",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditLoadHistory_EmployeeCard_EmployeeCardId",
                table: "CreditLoadHistory",
                column: "EmployeeCardId",
                principalTable: "EmployeeCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Plant_PlantId",
                table: "Department",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                table: "Employee",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_EmployeeCard_EmployeeCardId",
                table: "Employee",
                column: "EmployeeCardId",
                principalTable: "EmployeeCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Plant_PlantId",
                table: "Employee",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeCard_Plant_PlantId",
                table: "EmployeeCard",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeCardMatchHistory_Employee_EmployeeId",
                table: "EmployeeCardMatchHistory",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeCardMatchHistory_EmployeeCard_EmployeeCardId",
                table: "EmployeeCardMatchHistory",
                column: "EmployeeCardId",
                principalTable: "EmployeeCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemCategory_ItemCategoryId",
                table: "Item",
                column: "ItemCategoryId",
                principalTable: "ItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                table: "Item",
                column: "ItemGroupId",
                principalTable: "ItemGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_UnitType_UnitTypeId",
                table: "Item",
                column: "UnitTypeId",
                principalTable: "UnitType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGroup_ItemCategory_ItemCategoryId",
                table: "ItemGroup",
                column: "ItemCategoryId",
                principalTable: "ItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceipt_Dealer_DealerId",
                table: "ItemReceipt",
                column: "DealerId",
                principalTable: "Dealer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceipt_Firm_FirmId",
                table: "ItemReceipt",
                column: "FirmId",
                principalTable: "Firm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceipt_Plant_PlantId",
                table: "ItemReceipt",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceipt_Warehouse_WarehouseId",
                table: "ItemReceipt",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceiptDetail_Item_ItemId",
                table: "ItemReceiptDetail",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceiptDetail_ItemReceipt_ItemReceiptId",
                table: "ItemReceiptDetail",
                column: "ItemReceiptId",
                principalTable: "ItemReceipt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemReceiptDetail_Machine_MachineId",
                table: "ItemReceiptDetail",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Machine_Plant_PlantId",
                table: "Machine",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSpiral_ItemCategory_ItemCategoryId",
                table: "MachineSpiral",
                column: "ItemCategoryId",
                principalTable: "ItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSpiral_ItemGroup_ItemGroupId",
                table: "MachineSpiral",
                column: "ItemGroupId",
                principalTable: "ItemGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSpiral_Machine_MachineId",
                table: "MachineSpiral",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SysLangDict_SysLang_SysLangId",
                table: "SysLangDict",
                column: "SysLangId",
                principalTable: "SysLang",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Dealer_DealerId",
                table: "Warehouse",
                column: "DealerId",
                principalTable: "Dealer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
