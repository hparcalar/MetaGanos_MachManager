namespace MachManager.Models{
    public class DepartmentModel{
        public int Id { get; set; }
        public string DepartmentCode { get; set; } = "";
        public string DepartmentName { get; set; } = "";
        public Nullable<int> PlantId { get; set; }
        public Nullable<int> PlantPrintFileId { get; set; }
        public bool IsActive { get; set; }

        #region VISUAL ELEMENTS
        public DepartmentItemCategoryModel[] ItemCategories { get; set; }
        public DepartmentMachineModel[] Machines { get; set; }
        public DepartmentCreditModel[] Credits { get; set; }
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        #endregion

    }
}