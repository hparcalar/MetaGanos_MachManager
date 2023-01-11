namespace MachManager.Models.Parameters{
    public class WarehouseItemStatusFilter{
        public string WarehouseCode { get; set; }
        public string PlantCode { get; set; }
        public int? WarehouseId { get; set; }
        public int[] PlantId { get; set; }
        public int[] CategoryId { get; set; }
        public int[] GroupId { get; set; }
        public int[] ItemId { get; set; }
    }
}