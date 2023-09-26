using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context{
    public class SysPublication{
        public int Id { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        public DateTime? PublicationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? WarningType { get; set; }
        public byte[] Attachment { get; set; }
        public string AttachmentContentType { get; set; }
        public string AttachmentFileName { get; set; }
        public int? PublicationStatus { get; set; }
        public bool? ReplaceWithHomeVideo { get; set; }

        public virtual Plant Plant { get; set; }
    }
}