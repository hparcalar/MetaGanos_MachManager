namespace MachManager.Models{
    public class EmployeeCardModel{
        public int Id { get; set; }
        public string CardCode { get; set; } = "";
        public string HexKey { get; set; } = "";
        public Nullable<int> PlantId { get; set; }
        public bool IsActive { get; set; }

        #region VISUAL ELEMENTS
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        #endregion
    }
}