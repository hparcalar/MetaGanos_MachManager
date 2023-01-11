using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context{
    public class WarehouseHotSalesCategory{
        public int Id { get; set; }

        [ForeignKey("ItemCategory")]
        public Nullable<int> ItemCategoryId { get; set; }

        [ForeignKey("ItemGroup")]
        public Nullable<int> ItemGroupId { get; set; }

        [ForeignKey("Item")]
        public Nullable<int> ItemId { get; set; }

        [ForeignKey("Warehouse")]
        public Nullable<int> WarehouseId { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }
        public virtual Item Item { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}