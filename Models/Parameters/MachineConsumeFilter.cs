namespace MachManager.Models.Parameters{
    public class MachineConsumeFilter{
        public int[] MachineId { get; set; }
        public string WarehouseCode { get; set; }
        public string PlantCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int[] EmployeeId { get; set; }
        public int[] DepartmentId { get; set; }
        public int[] PlantId { get; set; }
        public int[] CategoryId { get; set; }
        public int[] GroupId { get; set; }
        public int[] ItemId { get; set; }
    }
}