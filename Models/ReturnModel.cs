namespace MachManager.Models{
    public class ReturnModel{
        public int Id { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> PlantId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<DateTime> ReturnDate { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> WarehouseId { get; set; }

        #region VISUAL ELEMENTS
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeCardCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; } 
        public string ReturnTimeEx { get; set; }
        public string ReturnDateEx { get; set; }

        public Nullable<int> ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public Nullable<int> ItemGroupId { get; set; }
        public string ItemGroupName { get; set; }
        #endregion
    }
}