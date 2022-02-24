using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class ForexType{
        public int Id { get; set; }
        
        public string ForexCode { get; set; } = "";
        public string Explanation { get; set; } = "";
        public bool IsActive { get; set; }
    }
}