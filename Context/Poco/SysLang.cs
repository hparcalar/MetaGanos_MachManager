using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachManager.Context {
    public class SysLang{
        public SysLang(){
            this.SysLangDict = new HashSet<SysLangDict>();
        }
        public int Id { get; set; }
        public string LanguageCode { get; set; } = "";
        public string LanguageName { get; set; } = "";
        public bool IsDefault { get; set; } = false;
        public bool IsActive { get; set; }

        [InverseProperty("SysLang")]
        public virtual ICollection<SysLangDict> SysLangDict { get; set; }
    }
}