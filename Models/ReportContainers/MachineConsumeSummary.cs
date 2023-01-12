namespace MachManager.Models.ReportContainers{
    public class MachineConsumeSummary{
        public int? Id { get; set; }
        public int? MachineId { get; set; }
        public int? ItemId { get; set; }
        public int? ItemGroupId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? PlantId { get; set; }
        public int? EmployeeId { get; set; }
        public int? TotalConsumed { get; set; }
        public string MachineCode { get; set; }
        public string MachineName { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeCardCode { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public int? SpiralNo { get; set; }
        public int? ActiveCredit { get; set; }
        public DateTime? ConsumedDate { get; set; }
    }
}