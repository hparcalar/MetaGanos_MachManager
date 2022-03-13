using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class PlantPrintFile{
        public int Id { get; set; }
        public string PrintFileCode { get; set; } = "";
        public string PrintFileName { get; set; } = "";
        public string Explanation { get; set; } = "";
        public string ImageData { get; set; }

        [ForeignKey("Plant")]
        public int PlantId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }

        // REFERENCES
        public virtual Plant Plant { get; set; }
    }
}