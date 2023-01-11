using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class Dealer{
        public int Id { get; set; }
        public string DealerCode { get; set; } = "";
        public string DealerName { get; set; } = "";
        public string Explanation { get; set; } = "";
        public string DealerPassword { get; set; }
        public string DefaultLanguage { get; set; }

        [ForeignKey("ParentDealer")]
        public Nullable<int> ParentDealerId { get; set; }
        public bool IsActive { get; set; }

        // REFERENCES
        public virtual Dealer ParentDealer { get; set; }
    }
}