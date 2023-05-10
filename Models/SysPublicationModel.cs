namespace MachManager.Models{
    public class SysPublicationModel{
        public int Id { get; set; }
        public int? PlantId { get; set; }

        public DateTime? PublicationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? WarningType { get; set; }
        public byte[] Attachment { get; set; }
        public string AttachmentContentType { get; set; }
        public string AttachmentFileName { get; set; }
        public int? PublicationStatus { get; set; }
        public bool? ReplaceWithHomeVideo { get; set; }

        #region VISUAL ELEMENTS
        public string WarningTypeText { get; set; }
        public string StatusText { get; set; }
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        #endregion
    }
}