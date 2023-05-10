namespace MachManager.Models{
    public class WarehouseLoadHeaderModel{
        public int Id { get; set; }
        public DateTime? LoadDate { get; set; }
        public string ReceiptNo { get; set; }
        public string DocumentNo { get; set; }
        public int? LoadType { get; set; }
        public int? FirmId { get; set; }
        public int? PlantId { get; set; }
        public int? WarehouseId { get; set; }
        public int? LoadOfficerId { get; set; }
        public string Explanation { get; set; }
        public int? ItemOrderId { get; set; }

        public int? OutWarehouseId { get; set; }
        public int? InLoadHeaderId { get; set; }

        public int? OutLoadHeaderId { get; set; }
        public bool? IsGenerated { get; set; }

        #region VISUAL ELEMENTS
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string OfficerCode { get; set; }
        public string OfficerName { get; set; }
        public string LoadTypeText { get; set; }
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        public string ItemOrderNo { get; set; }

        public WarehouseLoadModel[] Details { get; set; }
        #endregion
    }
}