namespace MachManager.Models
{
    public class DealerModel{
        public int Id { get; set; }
        public string DealerCode { get; set; } = "";
        public string DealerName { get; set; } = "";
        public string Explanation { get; set; } = "";
        public Nullable<int> ParentDealerId { get; set; }
        public bool IsActive { get; set; }
    }
}