using MachManager.i18n;

namespace MachManager.Models.Constants{
    public class ControlTimeOption{
        public static readonly int DAILY = 1;
        public static readonly int WEEKLY = 2;
        public static readonly int MONTHLY = 3;
        public static readonly int YEARLY = 4;
        public static readonly int INDEFINITE = 5;

        public static Expressions GetExpression(int controlTimeType){
            
            switch (controlTimeType)
            {
                case 1:
                    return Expressions.Daily;
                case 2:
                    return Expressions.Weekly;
                case 3:
                    return Expressions.Monthly;
                case 4:
                    return Expressions.Yearly;
                case 5:
                    return Expressions.Indefinite;
                default:
                    break;
            }

            return Expressions.Indefinite;
        }
    }
}