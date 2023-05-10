using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context{
    public class ItemOrder{
        public ItemOrder(){
            this.ItemOrderDetails = new HashSet<ItemOrderDetail>();
        }
        public int Id { get; set; }

        [ForeignKey("Plant")]
        public int? PlantId { get; set; }

        public int? ReceiptType { get; set; }

        public string ReceiptNo { get; set; }
        public DateTime? OrderDate { get; set; }

        [ForeignKey("Firm")]
        public int? FirmId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string Explanation { get; set; }

        public decimal? SubTotal { get; set; }
        public decimal? TaxTotal { get; set; }
        public decimal? OverallTotal { get; set; }

        public int? ItemOrderStatus { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual Firm Firm { get; set; }

        [InverseProperty("ItemOrder")]
        public virtual ICollection<ItemOrderDetail> ItemOrderDetails { get; set; }
    }
}