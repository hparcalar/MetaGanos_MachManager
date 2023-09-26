using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context{
    public class EmployeeNotification{
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? TargetDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? NotificationStatus { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Plant Plant { get; set; }
    }
}