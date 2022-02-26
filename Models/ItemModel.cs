namespace MachManager.Models{
    public class ItemModel{
        public int Id { get; set; }
        public string ItemCode { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string AlternatingCode1 { get; set; }
        public string AlternatingCode2 { get; set; }
        public string Barcode1 { get; set; }
        public string Barcode2 { get; set; }
        public decimal? CriticalMax { get; set; }
        public decimal? CriticalMin { get; set; }
        public decimal? Price1 { get; set; }
        public decimal? Price2 { get; set; }
        public string Explanation { get; set; } = "";
        public int ViewOrder { get; set; }
        public Nullable<int> ItemGroupId { get; set; }
        public Nullable<int> ItemCategoryId { get; set; }
        public Nullable<int> UnitTypeId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        
        #region VISUAL ELEMENTS
        public string ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        public string UnitTypeCode { get; set; }
        public string UnitTypeName { get; set; }
        #endregion
    }
}