namespace MachManager.Models{
    public class DepartmentMachineModel{
        public int Id { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> MachineId { get; set; }

        #region VISUAL ELEMENTS
        public string MachineCode { get; set; }
        public string MachineName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        #endregion
    }
}