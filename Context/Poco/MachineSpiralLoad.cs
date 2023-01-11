using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class MachineSpiralLoad {
        public int Id { get; set; }
        public Nullable<DateTime> LoadDate { get; set; }
        public decimal? Quantity { get; set; }
        public Nullable<int> SpiralNo { get; set; }

        [ForeignKey("Machine")]
        public Nullable<int> MachineId { get; set; }

        [ForeignKey("Item")]
        public Nullable<int> ItemId { get; set; }

        [ForeignKey("Officer")]
        public Nullable<int> OfficerId { get; set; }

        [ForeignKey("Warehouse")]
        public Nullable<int> WarehouseId { get; set; }

        // REFERENCES
        public virtual Item Item { get; set; }
        public virtual Machine Machine { get; set; }
        public virtual Officer Officer { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}