namespace MachManager.Models{
    public class PlantModel{
        public int Id { get; set; }
        public string PlantCode { get; set; } = "";
        public string PlantName { get; set; } = "";
        public string Explanation { get; set; } = "";
        public bool IsActive { get; set; }
        public Nullable<int> DealerId { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }

        #region VISUAL ELEMENTS
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        #endregion
    }
}