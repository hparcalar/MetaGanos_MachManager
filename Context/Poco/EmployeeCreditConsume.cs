using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class EmployeeCreditConsume{
        public int Id { get; set; }
        public int ConsumedCredit { get; set; } = 0;

        [ForeignKey("ItemCategory")]
        public Nullable<int> ItemCategoryId { get; set; }

        [ForeignKey("ItemGroup")]
        public Nullable<int> ItemGroupId { get; set; }

        [ForeignKey("Item")]
        public Nullable<int> ItemId { get; set; }

        [ForeignKey("Department")]
        public Nullable<int> EmployeeId { get; set; }

        public Nullable<DateTime> ConsumedDate { get; set; }

        // REFERENCES
        public virtual Employee Employee { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }
        public virtual Item Item { get; set; }
    }
}