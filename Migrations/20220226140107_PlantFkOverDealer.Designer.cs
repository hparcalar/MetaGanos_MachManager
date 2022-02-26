﻿// <auto-generated />
using System;
using MachManager.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MachManager.Migrations
{
    [DbContext(typeof(MetaGanosSchema))]
    [Migration("20220226140107_PlantFkOverDealer")]
    partial class PlantFkOverDealer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MachManager.Context.CreditLoadHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("EmployeeCardId")
                        .HasColumnType("integer");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<int>("LoadedCredits")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeCardId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("CreditLoadHistory");
                });

            modelBuilder.Entity("MachManager.Context.Dealer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DealerCode")
                        .HasColumnType("text");

                    b.Property<string>("DealerName")
                        .HasColumnType("text");

                    b.Property<string>("DealerPassword")
                        .HasColumnType("text");

                    b.Property<string>("Explanation")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("ParentDealerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentDealerId");

                    b.ToTable("Dealer");
                });

            modelBuilder.Entity("MachManager.Context.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DepartmentCode")
                        .HasColumnType("text");

                    b.Property<string>("DepartmentName")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("PlantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlantId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("MachManager.Context.DepartmentItemCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<int?>("ItemCategoryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("DepartmentItemCategory");
                });

            modelBuilder.Entity("MachManager.Context.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActiveCredit")
                        .HasColumnType("integer");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int?>("EmployeeCardId")
                        .HasColumnType("integer");

                    b.Property<string>("EmployeeCode")
                        .HasColumnType("text");

                    b.Property<string>("EmployeeName")
                        .HasColumnType("text");

                    b.Property<string>("EmployeePassword")
                        .HasColumnType("text");

                    b.Property<string>("Gsm")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("PlantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("EmployeeCardId");

                    b.HasIndex("PlantId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("MachManager.Context.EmployeeCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CardCode")
                        .HasColumnType("text");

                    b.Property<string>("HexKey")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("PlantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlantId");

                    b.ToTable("EmployeeCard");
                });

            modelBuilder.Entity("MachManager.Context.EmployeeCardMatchHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("EmployeeCardId")
                        .HasColumnType("integer");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<string>("Explanation")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeCardId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeCardMatchHistory");
                });

            modelBuilder.Entity("MachManager.Context.Firm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConnectionProtocol")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DebitFormSamplePath")
                        .HasColumnType("text");

                    b.Property<string>("FirmCode")
                        .HasColumnType("text");

                    b.Property<string>("FirmLogo")
                        .HasColumnType("text");

                    b.Property<string>("FirmName")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Firm");
                });

            modelBuilder.Entity("MachManager.Context.ForexType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Explanation")
                        .HasColumnType("text");

                    b.Property<string>("ForexCode")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("ForexType");
                });

            modelBuilder.Entity("MachManager.Context.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AlternatingCode1")
                        .HasColumnType("text");

                    b.Property<string>("AlternatingCode2")
                        .HasColumnType("text");

                    b.Property<string>("Barcode1")
                        .HasColumnType("text");

                    b.Property<string>("Barcode2")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal?>("CriticalMax")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("CriticalMin")
                        .HasColumnType("numeric");

                    b.Property<string>("Explanation")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("ItemCategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("ItemCode")
                        .HasColumnType("text");

                    b.Property<int?>("ItemGroupId")
                        .HasColumnType("integer");

                    b.Property<string>("ItemName")
                        .HasColumnType("text");

                    b.Property<decimal?>("Price1")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Price2")
                        .HasColumnType("numeric");

                    b.Property<int?>("UnitTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("ViewOrder")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ItemCategoryId");

                    b.HasIndex("ItemGroupId");

                    b.HasIndex("UnitTypeId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("MachManager.Context.ItemCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ControlTimeType")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("ItemCategoryCode")
                        .HasColumnType("text");

                    b.Property<string>("ItemCategoryName")
                        .HasColumnType("text");

                    b.Property<int>("ItemChangeTime")
                        .HasColumnType("integer");

                    b.Property<int>("ViewOrder")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ItemCategory");
                });

            modelBuilder.Entity("MachManager.Context.ItemGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("ItemCategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("ItemGroupCode")
                        .HasColumnType("text");

                    b.Property<string>("ItemGroupName")
                        .HasColumnType("text");

                    b.Property<int>("ViewOrder")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ItemCategoryId");

                    b.ToTable("ItemGroup");
                });

            modelBuilder.Entity("MachManager.Context.ItemReceipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DealerId")
                        .HasColumnType("integer");

                    b.Property<string>("DocumentNo")
                        .HasColumnType("text");

                    b.Property<int?>("FirmId")
                        .HasColumnType("integer");

                    b.Property<int?>("PlantId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ReceiptDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ReceiptNo")
                        .HasColumnType("text");

                    b.Property<int>("ReceiptType")
                        .HasColumnType("integer");

                    b.Property<int?>("WarehouseId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DealerId");

                    b.HasIndex("FirmId");

                    b.HasIndex("PlantId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("ItemReceipt");
                });

            modelBuilder.Entity("MachManager.Context.ItemReceiptDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ForexId")
                        .HasColumnType("integer");

                    b.Property<decimal?>("ForexRate")
                        .HasColumnType("numeric");

                    b.Property<int?>("ItemId")
                        .HasColumnType("integer");

                    b.Property<int?>("ItemReceiptId")
                        .HasColumnType("integer");

                    b.Property<int?>("LineNumber")
                        .HasColumnType("integer");

                    b.Property<int?>("MachineId")
                        .HasColumnType("integer");

                    b.Property<decimal?>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("VisualQuantity")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ForexId");

                    b.HasIndex("ItemId");

                    b.HasIndex("ItemReceiptId");

                    b.HasIndex("MachineId");

                    b.ToTable("ItemReceiptDetail");
                });

            modelBuilder.Entity("MachManager.Context.Machine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Barcode")
                        .HasColumnType("text");

                    b.Property<string>("Brand")
                        .HasColumnType("text");

                    b.Property<string>("BrandModel")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<int>("Cols")
                        .HasColumnType("integer");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("District")
                        .HasColumnType("text");

                    b.Property<string>("InventoryCode")
                        .HasColumnType("text");

                    b.Property<DateTime?>("InventoryEntryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LocationData")
                        .HasColumnType("text");

                    b.Property<string>("MachineCode")
                        .HasColumnType("text");

                    b.Property<string>("MachineName")
                        .HasColumnType("text");

                    b.Property<int?>("PlantId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ProductionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Rows")
                        .HasColumnType("integer");

                    b.Property<string>("SpecialCustomer")
                        .HasColumnType("text");

                    b.Property<string>("StartVideoPath")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PlantId");

                    b.ToTable("Machine");
                });

            modelBuilder.Entity("MachManager.Context.MachineSpiral", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("ActiveQuantity")
                        .HasColumnType("numeric");

                    b.Property<int?>("ItemCategoryId")
                        .HasColumnType("integer");

                    b.Property<int?>("ItemGroupId")
                        .HasColumnType("integer");

                    b.Property<int?>("ItemId")
                        .HasColumnType("integer");

                    b.Property<int?>("MachineId")
                        .HasColumnType("integer");

                    b.Property<int?>("PosOrders")
                        .HasColumnType("integer");

                    b.Property<int?>("PosX")
                        .HasColumnType("integer");

                    b.Property<int?>("PosY")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ItemCategoryId");

                    b.HasIndex("ItemGroupId");

                    b.HasIndex("MachineId");

                    b.ToTable("MachineSpiral");
                });

            modelBuilder.Entity("MachManager.Context.Plant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DealerId")
                        .HasColumnType("integer");

                    b.Property<string>("Explanation")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("PlantCode")
                        .HasColumnType("text");

                    b.Property<string>("PlantName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DealerId");

                    b.ToTable("Plant");
                });

            modelBuilder.Entity("MachManager.Context.SysLang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<string>("LanguageCode")
                        .HasColumnType("text");

                    b.Property<string>("LanguageName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SysLang");
                });

            modelBuilder.Entity("MachManager.Context.SysLangDict", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("EqualResponse")
                        .HasColumnType("text");

                    b.Property<int?>("ExpNo")
                        .HasColumnType("integer");

                    b.Property<string>("Expression")
                        .HasColumnType("text");

                    b.Property<int?>("SysLangId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SysLangId");

                    b.ToTable("SysLangDict");
                });

            modelBuilder.Entity("MachManager.Context.UnitType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("UnitTypeCode")
                        .HasColumnType("text");

                    b.Property<string>("UnitTypeName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UnitType");
                });

            modelBuilder.Entity("MachManager.Context.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DealerId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("WarehouseCode")
                        .HasColumnType("text");

                    b.Property<string>("WarehouseName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DealerId");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("MachManager.Context.CreditLoadHistory", b =>
                {
                    b.HasOne("MachManager.Context.EmployeeCard", "EmployeeCard")
                        .WithMany()
                        .HasForeignKey("EmployeeCardId");

                    b.HasOne("MachManager.Context.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employee");

                    b.Navigation("EmployeeCard");
                });

            modelBuilder.Entity("MachManager.Context.Dealer", b =>
                {
                    b.HasOne("MachManager.Context.Dealer", "ParentDealer")
                        .WithMany()
                        .HasForeignKey("ParentDealerId");

                    b.Navigation("ParentDealer");
                });

            modelBuilder.Entity("MachManager.Context.Department", b =>
                {
                    b.HasOne("MachManager.Context.Plant", "Plant")
                        .WithMany()
                        .HasForeignKey("PlantId");

                    b.Navigation("Plant");
                });

            modelBuilder.Entity("MachManager.Context.Employee", b =>
                {
                    b.HasOne("MachManager.Context.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("MachManager.Context.EmployeeCard", "EmployeeCard")
                        .WithMany()
                        .HasForeignKey("EmployeeCardId");

                    b.HasOne("MachManager.Context.Plant", "Plant")
                        .WithMany()
                        .HasForeignKey("PlantId");

                    b.Navigation("Department");

                    b.Navigation("EmployeeCard");

                    b.Navigation("Plant");
                });

            modelBuilder.Entity("MachManager.Context.EmployeeCard", b =>
                {
                    b.HasOne("MachManager.Context.Plant", "Plant")
                        .WithMany()
                        .HasForeignKey("PlantId");

                    b.Navigation("Plant");
                });

            modelBuilder.Entity("MachManager.Context.EmployeeCardMatchHistory", b =>
                {
                    b.HasOne("MachManager.Context.EmployeeCard", "EmployeeCard")
                        .WithMany()
                        .HasForeignKey("EmployeeCardId");

                    b.HasOne("MachManager.Context.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employee");

                    b.Navigation("EmployeeCard");
                });

            modelBuilder.Entity("MachManager.Context.Item", b =>
                {
                    b.HasOne("MachManager.Context.ItemCategory", "ItemCategory")
                        .WithMany()
                        .HasForeignKey("ItemCategoryId");

                    b.HasOne("MachManager.Context.ItemGroup", "ItemGroup")
                        .WithMany()
                        .HasForeignKey("ItemGroupId");

                    b.HasOne("MachManager.Context.UnitType", "UnitType")
                        .WithMany()
                        .HasForeignKey("UnitTypeId");

                    b.Navigation("ItemCategory");

                    b.Navigation("ItemGroup");

                    b.Navigation("UnitType");
                });

            modelBuilder.Entity("MachManager.Context.ItemGroup", b =>
                {
                    b.HasOne("MachManager.Context.ItemCategory", "ItemCategory")
                        .WithMany()
                        .HasForeignKey("ItemCategoryId");

                    b.Navigation("ItemCategory");
                });

            modelBuilder.Entity("MachManager.Context.ItemReceipt", b =>
                {
                    b.HasOne("MachManager.Context.Dealer", "Dealer")
                        .WithMany()
                        .HasForeignKey("DealerId");

                    b.HasOne("MachManager.Context.Firm", "Firm")
                        .WithMany()
                        .HasForeignKey("FirmId");

                    b.HasOne("MachManager.Context.Plant", "Plant")
                        .WithMany()
                        .HasForeignKey("PlantId");

                    b.HasOne("MachManager.Context.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId");

                    b.Navigation("Dealer");

                    b.Navigation("Firm");

                    b.Navigation("Plant");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("MachManager.Context.ItemReceiptDetail", b =>
                {
                    b.HasOne("MachManager.Context.ForexType", "ForexType")
                        .WithMany()
                        .HasForeignKey("ForexId");

                    b.HasOne("MachManager.Context.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.HasOne("MachManager.Context.ItemReceipt", "ItemReceipt")
                        .WithMany()
                        .HasForeignKey("ItemReceiptId");

                    b.HasOne("MachManager.Context.Machine", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId");

                    b.Navigation("ForexType");

                    b.Navigation("Item");

                    b.Navigation("ItemReceipt");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("MachManager.Context.Machine", b =>
                {
                    b.HasOne("MachManager.Context.Plant", "Plant")
                        .WithMany()
                        .HasForeignKey("PlantId");

                    b.Navigation("Plant");
                });

            modelBuilder.Entity("MachManager.Context.MachineSpiral", b =>
                {
                    b.HasOne("MachManager.Context.ItemCategory", "ItemCategory")
                        .WithMany()
                        .HasForeignKey("ItemCategoryId");

                    b.HasOne("MachManager.Context.ItemGroup", "ItemGroup")
                        .WithMany()
                        .HasForeignKey("ItemGroupId");

                    b.HasOne("MachManager.Context.Machine", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId");

                    b.Navigation("ItemCategory");

                    b.Navigation("ItemGroup");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("MachManager.Context.Plant", b =>
                {
                    b.HasOne("MachManager.Context.Dealer", "Dealer")
                        .WithMany()
                        .HasForeignKey("DealerId");

                    b.Navigation("Dealer");
                });

            modelBuilder.Entity("MachManager.Context.SysLangDict", b =>
                {
                    b.HasOne("MachManager.Context.SysLang", "SysLang")
                        .WithMany()
                        .HasForeignKey("SysLangId");

                    b.Navigation("SysLang");
                });

            modelBuilder.Entity("MachManager.Context.Warehouse", b =>
                {
                    b.HasOne("MachManager.Context.Dealer", "Dealer")
                        .WithMany()
                        .HasForeignKey("DealerId");

                    b.Navigation("Dealer");
                });
#pragma warning restore 612, 618
        }
    }
}
