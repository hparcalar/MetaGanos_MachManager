namespace MachManager.Models{
    public class LicenseKeyModel{
        public int Id { get; set; }
        public string LicenseCode { get; set; }
        public string Explanation { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastValidation { get; set; }
    }
}