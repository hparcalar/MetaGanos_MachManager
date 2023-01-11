namespace MachManager.Models.Parameters{
    public class RequestBulkLoadModel{
        public int[] Employees { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? ItemGroupId { get; set; }
        public int? ItemId { get; set; }
    }
}