using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class Warehouse{
        public int Id { get; set; }
        public string WarehouseCode { get; set; } = "";
        public string WarehouseName { get; set; } = "";

        [ForeignKey("Dealer")]
        public Nullable<int> DealerId { get; set; }

        [ForeignKey("Plant")]
        public Nullable<int> PlantId { get; set; }
        public bool IsActive { get; set; }

        // REFERENCES
        public virtual Dealer Dealer { get; set; }
        public virtual Plant Plant { get; set; }
    }
}