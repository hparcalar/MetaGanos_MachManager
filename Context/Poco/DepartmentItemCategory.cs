using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class DepartmentItemCategory{
        public int Id { get; set; }
        
        [ForeignKey("Department")]
        public Nullable<int> DepartmentId { get; set; }

        [ForeignKey("ItemCategory")]
        public Nullable<int> ItemCategoryId { get; set; }

        public Nullable<int> CreditRangeType { get; set; }
        public Nullable<int> CreditByRange { get; set; }

        // REFERENCES
        public virtual Department Department { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
    }
}