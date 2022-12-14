namespace MachManager.Models{
    public class MachineItemConsumeModel{
        public int Id { get; set; }
        public int ConsumedCount { get; set; } = 0;
        public Nullable<int> MachineId { get; set; }
        public Nullable<int> WarehouseId { get; set; }

        public Nullable<int> SpiralNo { get; set; }
        public Nullable<int> ItemGroupId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<DateTime> ConsumedDate { get; set; }
        public int? ItemCategoryId { get; set; }

        #region VISUAL ELEMENTS
        public int MakeDelete { get; set; } = 0;
        public string ConsumeDateStr { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }  
        #endregion
    }
}