using MachManager.i18n;

namespace MachManager.Models.Constants{
    public class CreditRangeOption{
        public const int DAILY = 1;
        public const int WEEKLY = 2;
        public const int MONTHLY = 3;
        public const int INDEFINITE = 4;

        public static Expressions GetExpression(int creditRangeType){
            
            switch (creditRangeType)
            {
                case 1:
                    return Expressions.Daily;
                case 2:
                    return Expressions.Weekly;
                case 3:
                    return Expressions.Monthly;
                case 4:
                    return Expressions.Indefinite;
                default:
                    break;
            }

            return Expressions.Indefinite;
        }
    }
}