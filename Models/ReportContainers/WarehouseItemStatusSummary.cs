namespace MachManager.Models.ReportContainers{
    public class WarehouseItemStatusSummary{
        public int? ItemId { get; set; }
        public int? ItemGroupId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? WarehouseId { get; set; }
        public int? PlantId { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string PlantCode { get; set; }
        public string PlantName { get; set; }

        public decimal? InQuantity { get; set; }
        public decimal? OutQuantity { get; set; }
        public decimal? TotalQuantity { get; set; }

    }
}