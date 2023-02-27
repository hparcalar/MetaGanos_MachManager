using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context{
    public class LicenseKey{
        public int Id { get; set; }
        public string LicenseCode { get; set; }
        public string Explanation { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastValidation { get; set; }
    }
}