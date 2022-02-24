using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class ItemReceiptDetail{
        public int Id { get; set; }

        public Nullable<int> LineNumber { get; set; }
        
        [ForeignKey("Item")]
        public Nullable<int> ItemId { get; set; }

        [ForeignKey("ItemReceipt")]
        public Nullable<int> ItemReceiptId { get; set; }

        public decimal? Quantity { get; set; }
        public decimal? VisualQuantity { get; set; }

        public decimal? UnitPrice { get; set; }

        [ForeignKey("ForexType")]
        public Nullable<int> ForexId { get; set; }

        public decimal? ForexRate { get; set; }

        [ForeignKey("Machine")]
        public Nullable<int> MachineId { get; set; }

        // REFERENCES
        public virtual ItemReceipt ItemReceipt { get; set; }
        public virtual Item Item { get; set; }
        public virtual Machine Machine { get; set; }
        public virtual ForexType ForexType { get; set; }
    }
}