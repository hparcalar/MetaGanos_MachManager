namespace MachManager.Models{
    public class AuthUnitModel{
        public int Id { get; set; }
        public int OfficerId { get; set; }

        public string Section { get; set; }

        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanDelete { get; set; }

        #region VISUAL ELEMENTS
        public string SectionText { get; set; }
        #endregion
    }
}