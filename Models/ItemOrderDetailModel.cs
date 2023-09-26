namespace MachManager.Models{
    public class ItemOrderDetailModel{
        public int Id { get; set; }
        public int? ItemOrderId { get; set; }
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

        #region VISUAL ELEMENTS
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string StatusText { get; set; }
        #endregion

    }
}