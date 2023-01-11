namespace MachManager.Models{
    public class CreditLoadHistoryModel{
        public int Id { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> EmployeeCardId { get; set; }
        public Nullable<int> ItemCategoryId { get; set; }
        public Nullable<int> DealerId { get; set; }
        public int LoadedCredits { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }

        #region VISUAL ELEMENTS
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        #endregion

    }
}