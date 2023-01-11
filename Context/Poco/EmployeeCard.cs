using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class EmployeeCard{
        public EmployeeCard(){
            this.Employee = new HashSet<Employee>();
        }
        public int Id { get; set; }
        public string CardCode { get; set; } = "";
        public string HexKey { get; set; } = "";

        [ForeignKey("Plant")]
        public Nullable<int> PlantId { get; set; }
        public bool IsActive { get; set; }

        // REFERENCES
        public virtual Plant Plant { get; set; }

        [InverseProperty("EmployeeCard")]
        public virtual ICollection<Employee> Employee { get; set; }
    }
}