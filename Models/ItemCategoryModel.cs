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
    }
}