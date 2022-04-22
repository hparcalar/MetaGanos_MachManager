namespace MachManager.Models{
    public class ItemCategoryModel{
        public int Id { get; set; }
        public string ItemCategoryCode { get; set; } = "";
        public string ItemCategoryName { get; set; } = "";
        public int ViewOrder { get; set; }
        public int ControlTimeType { get; set; }
        public int ItemChangeTime { get; set; }
        public bool IsActive { get; set; }
        public string CategoryImage { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<int> CreditRangeType { get; set; }
        public Nullable<int> CreditByRange { get; set; }
        public Nullable<int> CreditRangeLength { get; set; }
        public Nullable<int> PlantId { get; set; }

        #region VISUAL ELEMENTS
        public string CreditRangeTypeText { get; set; }
        public string ControlTimeTypeText { get; set; }
        public string PlantCode { get; set; }
        public string PlantName { get; set; }

        #endregion
    }
}