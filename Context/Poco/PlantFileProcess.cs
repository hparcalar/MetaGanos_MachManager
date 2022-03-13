using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class PlantFileProcess{
        public int Id { get; set; }
        public string Explanation { get; set; } = "";
        
        [ForeignKey("PlantPrintFile")]
        public Nullable<int> PlantPrintFileId { get; set; }

        [ForeignKey("Employee")]
        public Nullable<int> EmployeeId { get; set; }

        [ForeignKey("Department")]
        public Nullable<int> DepartmentId { get; set; }
        public int ProcessStatus { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> ApprovedDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }

        // REFERENCES
        public virtual PlantPrintFile PlantPrintFile { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Department Department { get; set; }
    }
}