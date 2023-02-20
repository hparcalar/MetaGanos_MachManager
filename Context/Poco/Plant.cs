using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class Plant{
        public int Id { get; set; }
        public string PlantCode { get; set; } = "";
        public string PlantName { get; set; } = "";
        public string Explanation { get; set; } = "";
        public string PlantLogo { get; set; } = "";
        public bool IsActive { get; set; }
        public bool? Last4CharForCardRead { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        [ForeignKey("Dealer")]
        public Nullable<int> DealerId { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public bool? AutoSpiralLoading { get; set; }
        public bool? IsCreditsVisible { get; set; }

        // REFERENCES
        public virtual Dealer Dealer { get; set; }
    }
}