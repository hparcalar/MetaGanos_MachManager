namespace MachManager.Models
{
    public class DealerModel{
        public int Id { get; set; }
        public string DealerCode { get; set; } = "";
        public string DealerName { get; set; } = "";
        public string Explanation { get; set; } = "";
        public string DealerPassword { get; set; } = "";
        public Nullable<int> ParentDealerId { get; set; }
        public bool IsActive { get; set; }
        public string DefaultLanguage { get; set; }
        public string LicenseKey { get; set; }
        public bool IsRoot { get; set; }
        public DateTime? LastSelfValidation { get; set; }

        #region VISUAL ELEMENTS
        public string ParentDealerName { get; set; }
        #endregion
    }
}