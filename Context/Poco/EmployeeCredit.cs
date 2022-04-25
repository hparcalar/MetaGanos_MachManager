using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class EmployeeCredit{
        public int Id { get; set; }
        public int ActiveCredit { get; set; } = 0;

        [ForeignKey("ItemCategory")]
        public Nullable<int> ItemCategoryId { get; set; }

        [ForeignKey("ItemGroup")]
        public Nullable<int> ItemGroupId { get; set; }

        [ForeignKey("Item")]
        public Nullable<int> ItemId { get; set; }

        [ForeignKey("Department")]
        public Nullable<int> EmployeeId { get; set; }

        public int RangeCredit { get; set; } = 0;
        public int RangeIndex { get; set; } = 0;
        public int RangeType { get; set; } = 4;
        public int RangeLength { get; set; } = 1;
        public int CreditByRange { get; set; } = 0;
        public Nullable<int> ProductIntervalType { get; set; }
        public Nullable<int> ProductIntervalTime { get; set; }
        public string SpecificRangeDates { get; set; }

        public Nullable<DateTime> CreditLoadDate { get; set; }
        public Nullable<DateTime> CreditStartDate { get; set; }
        public Nullable<DateTime> CreditEndDate { get; set; }

        // REFERENCES
        public virtual Employee Employee { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }
        public virtual Item Item { get; set; }
    }
}