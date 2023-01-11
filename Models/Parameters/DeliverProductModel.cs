namespace MachManager.Models.Parameters{
    public class DeliverProductModel{
        public int EmployeeId { get; set; }
        public int ItemId { get; set; }
        public int SpiralNo { get; set; }
        public DateTime DeliverDate { get; set; }
        public int? Quantity { get; set; }
        public string WarehouseCode { get; set; }
    }
}