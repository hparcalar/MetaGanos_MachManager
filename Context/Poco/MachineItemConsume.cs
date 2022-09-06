using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class MachineItemConsume{
        public int Id { get; set; }
        public int ConsumedCount { get; set; } = 0;

        [ForeignKey("Machine")]
        public Nullable<int> MachineId { get; set; }

        public Nullable<int> SpiralNo { get; set; }

        [ForeignKey("ItemGroup")]
        public Nullable<int> ItemGroupId { get; set; }

        [ForeignKey("Item")]
        public Nullable<int> ItemId { get; set; }

        [ForeignKey("Employee")]
        public Nullable<int> EmployeeId { get; set; }

        [ForeignKey("Warehouse")]
        public Nullable<int> WarehouseId { get; set; }

        public Nullable<DateTime> ConsumedDate { get; set; }

        // REFERENCES
        public virtual Machine Machine { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }
        public virtual Item Item { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}