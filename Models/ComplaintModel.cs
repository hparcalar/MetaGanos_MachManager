namespace MachManager.Models{
    public class ComplaintModel{
        public int Id { get; set; }
        public string ComplaintCode { get; set; }
        public DateTime? ComplaintDate { get; set; }

        public int? OwnerEmployeeId { get; set; }

        public int? OwnerUserId { get; set; }

        public int? ItemId { get; set; }

        public int? PlantId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public int? ComplaintStatus { get; set; }

        #region VISUAL ELEMENTS
        public string UserName { get; set; }
        public string StatusText { get; set; }
        #endregion
    }
}