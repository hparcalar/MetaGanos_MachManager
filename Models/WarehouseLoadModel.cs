namespace MachManager.Models{
    public class WarehouseLoadModel{
        public int Id { get; set; }
        public Nullable<DateTime> LoadDate { get; set; }
        public int? LoadType { get; set; }
        public decimal? Quantity { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> WarehouseId { get; set; }
        public Nullable<int> OfficerId { get; set; }
        public Nullable<int> MachineId { get; set; }
        public int? WarehouseLoadHeaderId { get; set; }

        #region VISUAL ELEMENTS
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string OfficerName { get; set; }
        public string LoadTypeText { get; set; }
        public string MachineCode { get; set; }
        public string MachineName { get; set; }
        #endregion
    }
}