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
        Factories = 11,
        FactoryDefinitions=12,
        Machines = 13,
        MachineDefinitions=14,
        Departments=15,
        DepartmentDefinitions=16,
        Employees=17,
        EmployeeDefinitions=18,
        Cards=19,
        CardDefinitions=20,
        Items=21,
        ItemDefinitions=22,
        ItemCategories=23,
        ItemGroups=24,
        List=25,
        Count=26,
        New=27,
        Edit=28,
        Item=29,
        Units=30,
        LanguageSettings=31,
        RecordWasUsedAndCannotBeDeleted=32,
        Created=33,
        Approved=34,
        Finished=35,
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
            { Expressions.EmployeeIsOutOfCredit, "Bakiyeniz yetersiz." },
            { Expressions.Factories, "Fabrikalar" },
            { Expressions.FactoryDefinitions, "Fabrika Tanımları" },
            { Expressions.Machines, "Makineler" },
            { Expressions.MachineDefinitions, "Makine Tanımları" },
            { Expressions.Departments, "Departmanlar" },
            { Expressions.DepartmentDefinitions, "Departman Tanımları" },
            { Expressions.Employees, "Personeller" },
            { Expressions.EmployeeDefinitions, "Personel Tanımları" },
            { Expressions.Cards, "Kartlar" },
            { Expressions.CardDefinitions, "Kart Tanımları" },
            { Expressions.Items, "Stoklar" },
            { Expressions.ItemDefinitions, "Stok Tanımları" },
            { Expressions.ItemCategories, "Stok Kategorileri" },
            { Expressions.ItemGroups, "Stok Grupları" },
            { Expressions.List, "Liste" },
            { Expressions.Count, "Adet" },
            { Expressions.New, "Yeni" },
            { Expressions.Edit, "Düzenle" },
            { Expressions.Item, "Stok" },
            { Expressions.Units, "Birimler" },
            { Expressions.LanguageSettings, "Dil Ayarları" },
            { Expressions.RecordWasUsedAndCannotBeDeleted, "Bu kayıt kullanılmış olduğu için silinemez. Pasif olarak işaretlemeyi deneyebilirsiniz." },
            { Expressions.Created, "Oluşturuldu" },
            { Expressions.Approved, "Onaylandı" },
            { Expressions.Finished, "Tamamlandı" },
        };
    }
}