using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class Item{
        public int Id { get; set; }
        public string ItemCode { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string AlternatingCode1 { get; set; }
        public string AlternatingCode2 { get; set; }
        public string Barcode1 { get; set; }
        public string Barcode2 { get; set; }
        public decimal? CriticalMax { get; set; }
        public decimal? CriticalMin { get; set; }
        public decimal? Price1 { get; set; }
        public decimal? Price2 { get; set; }
        public string Explanation { get; set; } = "";
        public string ItemImage { get; set; }
        public int ViewOrder { get; set; }

        [ForeignKey("ItemGroup")]
        public Nullable<int> ItemGroupId { get; set; }

        [ForeignKey("ItemCategory")]
        public Nullable<int> ItemCategoryId { get; set; }

        [ForeignKey("UnitType")]
        public Nullable<int> UnitTypeId { get; set; }

        public bool IsActive { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }

        // REFERENCES
        public virtual ItemGroup ItemGroup { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
        public virtual UnitType UnitType { get; set; }
    }
}