namespace MachManager.Models{
    public class DepartmentCreditModel{
        public int Id { get; set; }
        public int ActiveCredit { get; set; } = 0;
        public Nullable<int> ItemCategoryId { get; set; }
        public Nullable<int> ItemGroupId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> DepartmentId { get; set; }

        public int RangeCredit { get; set; } = 0;
        public int RangeIndex { get; set; } = 0;
        public int RangeType { get; set; } = 4;
        public int RangeLength { get; set; } = 1;
        public int CreditByRange { get; set; } = 0;
        public Nullable<int> ProductIntervalType { get; set; }
        public Nullable<int> ProductIntervalTime { get; set; }
        public string SpecificRangeDates { get; set; }

        public Nullable<DateTime> CreditLoadDate { get; set; }
        public Nullable<DateTime> CreditStartDate { get; set; }
        public Nullable<DateTime> CreditEndDate { get; set; }

        public string ItemName { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemCategoryName { get; set; }

        public string ItemCode { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemCategoryCode { get; set; }
    }
}