namespace MachManager.Models.ReportContainers{
    public class ItemConsumeAbs{
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public string Group { get; set; }
        public decimal? InQuantity { get; set; }
        public decimal? OutQuantity { get; set; }
        public decimal? TotalQuantity { get; set; }
    }
}