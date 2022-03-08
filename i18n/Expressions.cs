using System.Collections.Generic;

namespace MachManager.i18n{
    public enum Expressions{
        AnyError=1,
        SameCodeExists=2,
        RecordNotFound=3,
        UserNotFound=4,
        WrongPassword=5,
        CardIsBoundToEmployee=6,
        SpiralCapacityOverflowed=7,
        DemandedItemIsDifferentThanExistingOne=8,
        SpiralIsOutOfStock=9,
        EmployeeIsOutOfCredit=10,
    }

    public class DefaultEqualResponses{
        public static Dictionary<Expressions, string> List = new Dictionary<Expressions, string>(){
            { Expressions.AnyError, "Bir hata meydana geldi." },
            { Expressions.SameCodeExists, "Girilen kod kullanımdadır. Lütfen başka bir kod giriniz." },
            { Expressions.RecordNotFound, "Kayıt bulunamadı." },
            { Expressions.UserNotFound, "Kullanıcı tanımlı değil." },
            { Expressions.WrongPassword, "Hatalı parola." },
            { Expressions.CardIsBoundToEmployee, "Bu kart bir personel ile eşleştirilmiş durumdadır." },
            { Expressions.SpiralCapacityOverflowed, "Spiral kapasitesi aşımı." },
            { Expressions.DemandedItemIsDifferentThanExistingOne, "Talep edilen stok ile spiralde bulunan stok farklı olduğu için işlem başarısız oldu." },
            { Expressions.SpiralIsOutOfStock, "Spiralde stok kalmadı." },
            { Expressions.EmployeeIsOutOfCredit, "Bakiyeniz yetersiz." }
        };
    }
}