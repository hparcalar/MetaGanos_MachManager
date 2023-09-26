namespace MachManager.Models{
    public class ItemOrderModel{
        public int Id { get; set; }
        public int? PlantId { get; set; }

        public int? ReceiptType { get; set; }

        public string ReceiptNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? FirmId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string Explanation { get; set; }

        public decimal? SubTotal { get; set; }
        public decimal? TaxTotal { get; set; }
        public decimal? OverallTotal { get; set; }

        public int? ItemOrderStatus { get; set; }

        #region VISUAL ELEMENTS
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        public string StatusText { get; set; }
        public ItemOrderDetailModel[] Details { get; set; }
        #endregion
    }
}