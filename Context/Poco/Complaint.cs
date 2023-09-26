using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context{
    public class Complaint{
        public int Id { get; set; }
        public string ComplaintCode { get; set; }
        public DateTime? ComplaintDate { get; set; }

        [ForeignKey("Employee")]
        public int? OwnerEmployeeId { get; set; }

        [ForeignKey("Officer")]
        public int? OwnerUserId { get; set; }

        [ForeignKey("Item")]
        public int? ItemId { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public int? ComplaintStatus { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Officer Officer { get; set; }
        public virtual Item Item { get; set; }
        public virtual Plant Plant { get; set; }
    }
}