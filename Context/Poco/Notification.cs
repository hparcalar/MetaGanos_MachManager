using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class Notification{
        public int Id { get; set; }

        [ForeignKey("Plant")]
        public int PlantId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationMessage { get; set; }
        public bool IsSeen { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Plant Plant { get; set; }
    }
}