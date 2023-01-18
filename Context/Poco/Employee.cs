using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class Employee{
         public Employee(){
            this.EmployeeCredit = new HashSet<EmployeeCredit>();
        }
        public int Id { get; set; }
        public string EmployeeCode { get; set; } = "";
        public string EmployeeName { get; set; } = "";
        public string EmployeePassword { get; set; }
        public string Gsm { get; set; } = "";
        public string Email { get; set; } = "";
        public int? EmployeeStatus { get; set; }
        public int ActiveCredit { get; set; } = 0;

        [ForeignKey("Plant")]
        public Nullable<int> PlantId { get; set; }

        [ForeignKey("Department")]
        public Nullable<int> DepartmentId { get; set; }

        [ForeignKey("EmployeeCard")]
        public Nullable<int> EmployeeCardId { get; set; }
        public bool IsActive { get; set; }

        // REFERENCES
        public virtual Department Department { get; set; }
        public virtual EmployeeCard EmployeeCard { get; set; }
        public virtual Plant Plant { get; set; }

        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeCredit> EmployeeCredit { get; set; }
    }
}