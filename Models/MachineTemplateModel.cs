namespace MachManager.Models{
    public class MachineTemplateModel{
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public int? DealerId { get; set; }
        public string BrandModel { get; set; }
        public int? Rows { get; set; }
        public int? Cols { get; set; }
        public string SpiralConf { get; set; }
    }
}