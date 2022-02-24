using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class EmployeeCardMatchHistory{
        public int Id { get; set; }
        
        [ForeignKey("Employee")]
        public Nullable<int> EmployeeId { get; set; }

        [ForeignKey("EmployeeCard")]
        public Nullable<int> EmployeeCardId { get; set; }
        public string Explanation { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }

        // REFERENCES
        public virtual Employee Employee { get; set; }
        public virtual EmployeeCard EmployeeCard { get; set; }
    }
}