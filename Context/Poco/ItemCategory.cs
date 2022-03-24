using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class ItemCategory{
        public int Id { get; set; }
        public string ItemCategoryCode { get; set; } = "";
        public string ItemCategoryName { get; set; } = "";
        public int ViewOrder { get; set; }
        public int ControlTimeType { get; set; }
        public int ItemChangeTime { get; set; }
        public Nullable<int> CreditRangeType { get; set; }
        public Nullable<int> CreditByRange { get; set; }
        public bool IsActive { get; set; }
        public string CategoryImage { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
    }
}