namespace MachManager.Models{
    public class EmployeeModel{
        public int Id { get; set; }
        public string EmployeeCode { get; set; } = "";
        public string EmployeeName { get; set; } = "";
        public string EmployeePassword { get; set; }
        public string Gsm { get; set; } = "";
        public string Email { get; set; } = "";
        public int ActiveCredit { get; set; } = 0;
        public Nullable<int> PlantId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> EmployeeCardId { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsActive { get; set; }
        public int? EmployeeStatus { get; set; }

        #region VISUAL ELEMENTS
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeCardCode { get; set; }
        public string EmployeeCardHex { get; set; }
        public EmployeeCreditModel[] Credits { get; set; }
        #endregion
    }
}