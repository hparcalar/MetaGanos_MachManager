using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class Department{
        public int Id { get; set; }
        public string DepartmentCode { get; set; } = "";
        public string DepartmentName { get; set; } = "";

        [ForeignKey("Plant")]
        public Nullable<int> PlantId { get; set; }

        [ForeignKey("PlantPrintFile")]
        public Nullable<int> PlantPrintFileId { get; set; }
        public bool IsActive { get; set; }

        // REFERENCES
        public virtual Plant Plant { get; set; }
        public virtual PlantPrintFile PlantPrintFile { get; set; }
    }
}