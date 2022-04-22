using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class Officer{
        public int Id { get; set; }

        [ForeignKey("Plant")]
        public int PlantId { get; set; }
        public string OfficerCode { get; set; }
        public string OfficerName { get; set; }
        public string OfficerPassword { get; set; }
        public bool IsActive { get; set; }
        public string DefaultLanguage { get; set; }

        public virtual Plant Plant { get; set; }
    }
}