using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class Machine{
        public int Id { get; set; }

        [ForeignKey("Plant")]
        public Nullable<int> PlantId { get; set; }
        public string MachineCode { get; set; } = "";
        public string MachineName { get; set; } = "";
        public string StartVideoPath { get; set; } = "";
        public string SpecialCustomer { get; set; } = "";
        public string InventoryCode { get; set; } = "";
        public string Barcode { get; set; } = "";
        public Nullable<DateTime> ProductionDate { get; set; }
        public Nullable<DateTime> InventoryEntryDate { get; set; }
        public string LocationData { get; set; } = "";
        public string Brand { get; set; } = "";
        public string BrandModel { get; set; } = "";
        public string Country { get; set; } = "";
        public string City { get; set; } = "";
        public string District { get; set; } = "";
        public int Rows { get; set; }
        public int Cols { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> SpiralStartIndex { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string DefaultLanguage { get; set; }

        // REFERENCES
        public virtual Plant Plant { get; set; }
    }
}