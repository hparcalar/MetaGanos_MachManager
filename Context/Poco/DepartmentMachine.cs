using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context{
    public class DepartmentMachine{
        public int Id { get; set; }

        [ForeignKey("Department")]
        public Nullable<int> DepartmentId { get; set; }

        [ForeignKey("Machine")]
        public Nullable<int> MachineId { get; set; }

        public virtual Department Department { get; set; }

        public virtual Machine Machine { get; set; }
    }
}