namespace MachManager.Models{
    public class NotificationModel{
        public int Id { get; set; }
        public int PlantId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationMessage { get; set; }
        public bool IsSeen { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        #region VISUAL ELEMENTS
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        #endregion
    }
}