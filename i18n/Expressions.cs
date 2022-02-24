using System.Collections.Generic;

namespace MachManager.i18n{
    public enum Expressions{
        AnyError=1,
        SameCodeExists=2,
        RecordNotFound=3,
    }

    public class DefaultEqualResponses{
        public static Dictionary<Expressions, string> List = new Dictionary<Expressions, string>(){
            { Expressions.AnyError, "Bir hata meydana geldi." },
            { Expressions.SameCodeExists, "Girilen kod kullanımdadır. Lütfen başka bir kod giriniz." },
            { Expressions.RecordNotFound, "Kayıt bulunamadı." }
        };
    }
}