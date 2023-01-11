using MachManager.Models;

namespace MachManager.Models.Operational{
    public class CardLoginResult{
        public bool Result { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        
        public EmployeeModel Employee { get; set; }
    }
}