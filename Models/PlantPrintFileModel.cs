namespace MachManager.Models{
    public class PlantPrintFileModel{
        public int Id { get; set; }
        public string PrintFileCode { get; set; } = "";
        public string PrintFileName { get; set; } = "";
        public string Explanation { get; set; } = "";
        public int PlantId { get; set; }
        public bool IsActive { get; set; }
        public string ImageData { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }

        #region VISUAL ELEMENTS
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        #endregion
    }
}