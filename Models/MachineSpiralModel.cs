namespace MachManager.Models{
    public class MachineSpiralModel{
        public int Id { get; set; }
        
        public Nullable<int> PosX { get; set; }
        public Nullable<int> PosY { get; set; }
        public Nullable<int> PosOrders { get; set; }

        public decimal? ActiveQuantity { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> ItemCategoryId { get; set; }
        public Nullable<int> ItemGroupId { get; set; }
        public Nullable<int> Capacity { get; set; }
        public Nullable<bool> IsEnabled { get; set; }

        #region VISUAL ELEMENTS
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        #endregion
    }
}