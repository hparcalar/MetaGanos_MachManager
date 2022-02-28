namespace MachManager.Models{
    public class EmployeeCreditConsumeModel{
        public int Id { get; set; }
        public int ConsumedCredit { get; set; } = 0;
        public Nullable<int> ItemCategoryId { get; set; }
        public Nullable<int> ItemGroupId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<DateTime> ConsumedDate { get; set; }
    }
}