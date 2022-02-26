namespace MachManager.Models{
    public class ItemGroupModel{
        public int Id { get; set; }
        public string ItemGroupCode { get; set; } = "";
        public string ItemGroupName { get; set; } = "";
        public int ViewOrder { get; set; }
        public Nullable<int> ItemCategoryId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }

        #region VISUAL ELEMENTS
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        #endregion
    }
}