using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class SysLangDict{
        public int Id { get; set; }
        
        [ForeignKey("SysLang")]
        public Nullable<int> SysLangId { get; set; }

        public Nullable<int> ExpNo { get; set; }

        public string Expression { get; set; } = "";
        public string EqualResponse { get; set; } = "";

        // REFERENCES
        public virtual SysLang SysLang { get; set; }
    }
}