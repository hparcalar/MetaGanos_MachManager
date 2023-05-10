using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class MetaGanosSchema : DbContext, IDisposable{
        public DbSet<Dealer> Dealer { get; set; }
        public DbSet<Plant> Plant { get; set; }
        public DbSet<CreditLoadHistory> CreditLoadHistory { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<DepartmentItemCategory> DepartmentItemCategory { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeCard> EmployeeCard { get; set; }
        public DbSet<EmployeeCardMatchHistory> EmployeeCardMatchHistory { get; set; }
        public DbSet<Firm> Firm { get; set; }
        public DbSet<ForexType> ForexType { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemCategory> ItemCategory { get; set; }
        public DbSet<ItemGroup> ItemGroup { get; set; }
        public DbSet<ItemReceipt> ItemReceipt { get; set; }
        public DbSet<ItemReceiptDetail> ItemReceiptDetail { get; set; }
        public DbSet<Machine> Machine { get; set; }
        public DbSet<MachineSpiral> MachineSpiral { get; set; }
        public DbSet<SysLang> SysLang { get; set; }
        public DbSet<SysLangDict> SysLangDict { get; set; }
        public DbSet<UnitType> UnitType { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<EmployeeCredit> EmployeeCredit { get; set; }
        public DbSet<EmployeeCreditConsume> EmployeeCreditConsume { get; set; }
        public DbSet<MachineItemConsume> MachineItemConsume { get; set; }
        public DbSet<SpiralFace> SpiralFace { get; set; }
        public DbSet<PlantPrintFile> PlantPrintFile { get; set; }
        public DbSet<PlantFileProcess> PlantFileProcess { get; set; }
        public DbSet<Officer> Officer { get; set; }
        public DbSet<DepartmentMachine> DepartmentMachine { get; set; }
        public DbSet<AuthUnit> AuthUnit { get; set; }
        public DbSet<MachineSpiralLoad> MachineSpiralLoad { get; set; }
        public DbSet<WarehouseLoad> WarehouseLoad { get; set; }
        public DbSet<WarehouseHotSalesCategory> WarehouseHotSalesCategory { get; set; }
        public DbSet<WarehouseLoadHeader> WarehouseLoadHeader { get; set; }
        public DbSet<DepartmentCredit> DepartmentCredit { get; set; }
        public DbSet<MachineTemplate> MachineTemplate { get; set; }
        public DbSet<LicenseKey> LicenseKey { get; set; }
        public DbSet<Complaint> Complaint { get; set; }
        public DbSet<ProductRating> ProductRating { get; set; }
        public DbSet<SysNotification> SysNotification { get; set; }
        public DbSet<EmployeeNotification> EmployeeNotification { get; set; }
        public DbSet<SysPublication> SysPublication { get; set; }
        public DbSet<ItemOrder> ItemOrder { get; set; }
        public DbSet<ItemOrderDetail> ItemOrderDetail { get; set; }
        public DbSet<ExternalCardRead> ExternalCardRead { get; set; }

        public MetaGanosSchema() : base(){}
        public MetaGanosSchema(Microsoft.EntityFrameworkCore.DbContextOptions options) : base(options){}

        public new void Dispose() {
            base.Dispose();
        }
    }
}