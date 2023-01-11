namespace MachManager.Models{
    public class SysLangModel{
        public int Id { get; set; }
        public string LanguageCode { get; set; } = "";
        public string LanguageName { get; set; } = "";
        public bool IsDefault { get; set; } = false;
        public bool IsActive { get; set; }
    }
}