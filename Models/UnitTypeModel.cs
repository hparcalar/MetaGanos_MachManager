namespace MachManager.Models{
    public class UnitTypeModel{
        public int Id { get; set; }
        public string UnitTypeCode { get; set; } = "";
        public string UnitTypeName { get; set; } = "";
        public bool IsActive { get; set; }
    }
}