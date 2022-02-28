namespace MachManager.Models{
    public class EmployeeCreditModel{
        public int Id { get; set; }
        public int ActiveCredit { get; set; } = 0;
        public Nullable<int> ItemCategoryId { get; set; }
        public Nullable<int> ItemGroupId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> EmployeeId { get; set; }

        #region VISUAL ELEMENTS
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        #endregion
    }
}