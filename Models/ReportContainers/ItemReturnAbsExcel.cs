namespace MachManager.Models.ReportContainers{
    public class ItemReturnAbsExcel{
        public string ReturnDate { get; set; }
        public string ReturnTime { get; set; }
        public string EmployeeName { get; set; }
        public string WarehouseName { get; set; }
        public string DepartmentName { get; set; }
        public string ItemCategoryName { get; set; }
        public string ItemName { get; set; }
        public int? Quantity { get; set; }
    }
}