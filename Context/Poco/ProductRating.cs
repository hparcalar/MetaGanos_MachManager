using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context{
    public class ProductRating{
        public int Id { get; set; }

        [ForeignKey("Item")]
        public int? ItemId { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }

        [ForeignKey("Officer")]
        public int? UserId { get; set; }
        public int? Rate { get; set; }

        public DateTime? RatingDate { get; set; }
        public string Explanation { get; set; }

        public virtual Item Item { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Officer Officer { get; set; }
    }
}