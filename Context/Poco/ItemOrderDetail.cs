using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context{
    public class ItemOrderDetail{
        public int Id { get; set; }

        [ForeignKey("ItemOrder")]
        public int? ItemOrderId { get; set; }

        [ForeignKey("Item")]
        public int? ItemId { get; set; }

        public int? LineNumber { get; set; }

        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TaxTotal { get; set; }
        public decimal? OverallTotal { get; set; }
        public string Explanation { get; set; }
        public int? ItemOrderStatus { get; set; }

        public virtual ItemOrder ItemOrder { get; set; }
        public virtual Item Item { get; set; }
    }
}