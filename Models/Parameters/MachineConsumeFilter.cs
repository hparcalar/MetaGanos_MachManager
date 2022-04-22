namespace MachManager.Models.Parameters{
    public class MachineConsumeFilter{
        public int[] Machines { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int[] Plants { get; set; }
    }
}