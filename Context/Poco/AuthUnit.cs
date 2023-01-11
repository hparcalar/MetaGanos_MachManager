using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context{
    public class AuthUnit{
        public int Id { get; set; }

        [ForeignKey("Officer")]
        public int OfficerId { get; set; }

        public string Section { get; set; }

        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanDelete { get; set; }

        public virtual Officer Officer { get; set; }
    }
}