namespace MachManager.Models.ReportContainers{
    public class PlantOverallStats{
        public string MostConsumedItemName { get; set; }
        public int MostConsumedItemCount { get; set; }
        public int ActiveMachineCount { get; set; }
        public int TotalMachineCount { get; set; }
        public int InFaultSpiralCount { get; set; }
        public CategoryStatsYearly[] CategoryStats { get; set; }
    }

    public class CategoryStatsYearly{
        public string CategoryName { get; set; }
        public int[] MonthlyStats { get; set; }
    }
}