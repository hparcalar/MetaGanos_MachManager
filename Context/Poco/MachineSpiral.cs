using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class MachineSpiral{
        public int Id { get; set; }
        
        public Nullable<int> PosX { get; set; }
        public Nullable<int> PosY { get; set; }
        public Nullable<int> PosOrders { get; set; }

        public decimal? ActiveQuantity { get; set; }

        [ForeignKey("Machine")]
        public Nullable<int> MachineId { get; set; }
        
        [ForeignKey("Item")]
        public Nullable<int> ItemId { get; set; }

        [ForeignKey("ItemCategory")]
        public Nullable<int> ItemCategoryId { get; set; }

        [ForeignKey("ItemGroup")]
        public Nullable<int> ItemGroupId { get; set; }

        // REFERENCES
        public virtual Machine Machine { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }
    }
}