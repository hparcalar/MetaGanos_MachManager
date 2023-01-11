namespace MachManager.Models{
    public class WarehouseHotSalesCategoryModel{
        public int Id { get; set; }
        public Nullable<int> ItemCategoryId { get; set; }
        public Nullable<int> ItemGroupId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> WarehouseId { get; set; }

        #region VISUAL ELEMENTS
        public string ItemText { get; set; }
        #endregion
    }
}