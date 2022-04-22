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

        public MetaGanosSchema() : base(){}
        public MetaGanosSchema(Microsoft.EntityFrameworkCore.DbContextOptions options) : base(options){}

        public new void Dispose() {
            base.Dispose();
        }
    }
}