namespace MachManager.Models.PagedModels{
    public class PagedEmployeeModel{
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public EmployeeModel[] Data { get; set; }
    }
}