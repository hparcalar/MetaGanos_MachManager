namespace MachManager.Models.Operational{
    public class PanelLoginResult{
        public string Token { get; set; }
        public string AuthType { get; set; }
        public int UserId { get; set; }
        public int FactoryId { get; set; }
        public string FactoryName { get; set; }
        public string Username { get; set; }
        public string DefaultLanguage { get; set; }
    }
}