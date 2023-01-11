namespace MachManager.Models.ReportContainers{
    public class MachineConsumeAbsExcel{
        public string ConsumedDate { get; set; }
        public string ConsumedTime { get; set; }
        public string EmployeeName { get; set; }
        public string MachineName { get; set; }
        public string ItemCategoryName { get; set; }
        public string ItemName { get; set; }
        public int? SpiralNo { get; set; }
        public int? TotalConsumed { get; set; }
    }
}