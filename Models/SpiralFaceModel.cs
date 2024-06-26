namespace MachManager.Models{
    public class SpiralFaceModel{
        public int Id { get; set; }
        public Nullable<int> ItemCategoryId { get; set; }
        public Nullable<int> ItemGroupId { get; set; }
        public Nullable<int> Capacity { get; set; }

        #region VISUAL ELEMENTS
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        #endregion
    }
}