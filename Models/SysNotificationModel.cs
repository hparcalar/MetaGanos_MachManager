namespace MachManager.Models{
    public class SysNotificationModel{
        public int Id { get; set; }
        public DateTime? NotificationDate { get; set; }
        public int? NotificationType { get; set; }
        public int? WarningType { get; set; }
        public int? NotificationStatus { get; set; }
        public string GotoLink { get; set; }
        public int? UserId { get; set; }
        public int? PlantId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        #region VISUAL ELEMENTS
        public string UserName { get; set; }
        public string StatusText { get; set; }
        public string WarningTypeText { get; set; }
        #endregion
    }
}