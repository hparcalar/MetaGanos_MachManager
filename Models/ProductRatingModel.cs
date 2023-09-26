namespace MachManager.Models{
    public class ProductRatingModel{
        public int Id { get; set; }
        public int? ItemId { get; set; }
        public int? EmployeeId { get; set; }
        public int? UserId { get; set; }
        public int? Rate { get; set; }

        public DateTime? RatingDate { get; set; }
        public string Explanation { get; set; }

        #region VISUAL ELEMENTS
        public string UserName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        #endregion
    }
}