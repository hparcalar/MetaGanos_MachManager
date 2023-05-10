using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context{
    public class SysNotification{
        public int Id { get; set; }
        public DateTime? NotificationDate { get; set; }
        public int? NotificationType { get; set; }
        public int? WarningType { get; set; }
        public int? NotificationStatus { get; set; }
        public string GotoLink { get; set; }

        [ForeignKey("Officer")]
        public int? UserId { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public virtual Officer Officer { get; set; }
        public virtual Plant Plant { get; set; }
    }
}