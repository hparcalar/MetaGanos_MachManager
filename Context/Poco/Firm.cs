using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class Firm{
        public int Id { get; set; }
        public string FirmCode { get; set; } = "";
        public string FirmName { get; set; } = "";
        public string ConnectionProtocol { get; set; } = "";
        public string FirmLogo { get; set; } = "";
        public string DebitFormSamplePath { get; set; } = "";
        public bool IsActive { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
    }
}