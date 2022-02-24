using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class ItemReceipt{
        public int Id { get; set; }
        public int ReceiptType { get; set; }
        public string ReceiptNo { get; set; } = "";
        
        [ForeignKey("Firm")]
        public Nullable<int> FirmId { get; set; }

        [ForeignKey("Warehouse")]
        public Nullable<int> WarehouseId { get; set; }

        public string DocumentNo { get; set; }
        public Nullable<DateTime> ReceiptDate { get; set; }

        [ForeignKey("Dealer")]
        public Nullable<int> DealerId { get; set; }

        [ForeignKey("Plant")]
        public Nullable<int> PlantId { get; set; }
        
        public Nullable<DateTime> CreatedDate { get; set; }

        // REFERENCES
        public virtual Firm Firm { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual Dealer Dealer { get; set; }
        public virtual Plant Plant { get; set; }
    }
}