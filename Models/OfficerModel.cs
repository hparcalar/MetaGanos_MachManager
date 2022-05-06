namespace MachManager.Models{
    public class OfficerModel{
        public int Id { get; set; }
        public int PlantId { get; set; }
        public string OfficerCode { get; set; }
        public string OfficerName { get; set; }
        public string OfficerPassword { get; set; }
        public bool IsActive { get; set; }
        public string DefaultLanguage { get; set; }

        #region VISUAL ELEMENTS
        public AuthUnitModel[] AuthUnits { get; set; }
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        #endregion
    }
}