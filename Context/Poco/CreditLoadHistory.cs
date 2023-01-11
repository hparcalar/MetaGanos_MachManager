using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class CreditLoadHistory{
        public int Id { get; set; }
        
        [ForeignKey("Employee")]
        public Nullable<int> EmployeeId { get; set; }

        [ForeignKey("EmployeeCard")]
        public Nullable<int> EmployeeCardId { get; set; }

        [ForeignKey("ItemCategory")]
        public Nullable<int> ItemCategoryId { get; set; }

        [ForeignKey("Dealer")]
        public Nullable<int> DealerId { get; set; }

        public int LoadedCredits { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }

        // REFERENCES
        public virtual Employee Employee { get; set; }
        public virtual EmployeeCard EmployeeCard { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
        public virtual Dealer Dealer { get; set; }
    }
}