namespace MachManager.Models{
    public class PlantFileProcessModel {
        public int Id { get; set; }
        public string Explanation { get; set; } = "";
        public Nullable<int> PlantPrintFileId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public int ProcessStatus { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> ApprovedDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }

        #region VISUAL ELEMENTS
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string PrintFileCode { get; set; }
        public string PrintFileName { get; set; }
        public string ProcessStatusText { get; set; }
        #endregion
    }
}