namespace MachManager.Models{
    public class MachineItemConsumeModel{
        public int Id { get; set; }
        public int ConsumedCount { get; set; } = 0;
        public Nullable<int> MachineId { get; set; }

        public Nullable<int> SpiralNo { get; set; }
        public Nullable<int> ItemGroupId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<DateTime> ConsumedDate { get; set; }

        #region VISUAL ELEMENTS
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        #endregion
    }
}