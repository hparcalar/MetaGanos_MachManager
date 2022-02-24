using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class ItemGroup{
        public int Id { get; set; }
        public string ItemGroupCode { get; set; } = "";
        public string ItemGroupName { get; set; } = "";
        public int ViewOrder { get; set; }

        [ForeignKey("ItemCategory")]
        public Nullable<int> ItemCategoryId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }

        // REFERENCES
        public virtual ItemCategory ItemCategory { get; set; }
    }
}