namespace MachManager.Models{
    public class WarehouseModel{
        public int Id { get; set; }
        public string WarehouseCode { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public Nullable<int> DealerId { get; set; }
        public Nullable<int> PlantId { get; set; }
        public bool IsActive { get; set; }

        #region VISUAL ELEMENTS
        public string PlantName { get; set; }
        public WarehouseHotSalesCategoryModel[] HotSalesCategories { get; set; }
        #endregion
    }
}