using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context{
    public class ExternalCardRead{
        public int Id { get; set; }

        [ForeignKey("Machine")]
        public int MachineId { get; set; }

        public string CardNo { get; set; }
        public virtual Machine Machine { get; set; }
    }
}