namespace MachManager.Models{
    public class FirmModel{
        public int Id { get; set; }
        public string FirmCode { get; set; } = "";
        public string FirmName { get; set; } = "";
        public string ConnectionProtocol { get; set; } = "";
        public string FirmLogo { get; set; } = "";
        public string DebitFormSamplePath { get; set; } = "";
        public int? PlantId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
    }
}