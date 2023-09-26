using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class Return{
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public Nullable<int> EmployeeId { get; set; }
        [ForeignKey("Plant")]
        public Nullable<int> PlantId { get; set; }
        public Nullable<DateTime> ReturnDate { get; set; }
        [ForeignKey("Item")]
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> Quantity { get; set; }
        [ForeignKey("Warehouse")]
        public Nullable<int> WarehouseId { get; set; }
        
        // REFERENCES
        public virtual Employee Employee { get; set; }
        public virtual Plant Plant { get; set; }
        public virtual Item Item { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}