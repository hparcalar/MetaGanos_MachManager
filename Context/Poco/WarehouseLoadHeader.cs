using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context{
    public class WarehouseLoadHeader{
        public int Id { get; set; }
        public DateTime? LoadDate { get; set; }
        public string ReceiptNo { get; set; }
        public string DocumentNo { get; set; }
        public int? LoadType { get; set; }

        [ForeignKey("Firm")]
        public int? FirmId { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        [ForeignKey("Warehouse")]
        public int? WarehouseId { get; set; }

        [ForeignKey("Officer")]
        public int? LoadOfficerId { get; set; }

        [ForeignKey("ItemOrder")]
        public int? ItemOrderId { get; set; }
        public string Explanation { get; set; }

        [ForeignKey("OutWarehouse")]
        public int? OutWarehouseId { get; set; }

        [ForeignKey("InLoadHeader")]
        public int? InLoadHeaderId { get; set; }

        [ForeignKey("OutLoadHeader")]
        public int? OutLoadHeaderId { get; set; }
        
        public bool? IsGenerated { get; set; }

        public virtual Firm Firm { get; set; }
        public virtual Plant Plant { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual Officer Officer { get; set; }
        public virtual ItemOrder ItemOrder { get; set; }
        public virtual Warehouse OutWarehouse { get; set; }
        public virtual WarehouseLoadHeader InLoadHeader { get; set; }
        public virtual WarehouseLoadHeader OutLoadHeader { get; set; }
    }
}