namespace MachManager.Models{
    public class SysLangDictModel{
        public int Id { get; set; }
        public Nullable<int> SysLangId { get; set; }

        public Nullable<int> ExpNo { get; set; }

        public string Expression { get; set; } = "";
        public string EqualResponse { get; set; } = "";
    }
}