namespace MachManager.Models{
    public class DepartmentItemCategoryModel{
        public int Id { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> ItemCategoryId { get; set; }
        public Nullable<int> CreditRangeType { get; set; }
        public Nullable<int> CreditByRange { get; set; }

        #region VISUAL ELEMENTS
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        public string CreditRangeTypeText { get; set; }
        #endregion
    }
}