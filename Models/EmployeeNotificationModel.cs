namespace MachManager.Models{
    public class EmployeeNotificationModel{
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? PlantId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? TargetDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? NotificationStatus { get; set; }

        #region VISUAL ELEMENTS
        public string UserName { get; set; }
        public string StatusText { get; set; }
        #endregion
    }
}