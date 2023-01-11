using MachManager.i18n;

namespace MachManager.Models.Constants{
    public static class PrintFileStatus{
        public static readonly int CREATED = 1;
        public static readonly int APPROVED = 2;
        public static readonly int FINISHED = 3;

        public static Expressions GetExpression(int printFileStatus){
            
            switch (printFileStatus)
            {
                case 1:
                    return Expressions.Created;
                case 2:
                    return Expressions.Approved;
                case 3:
                    return Expressions.Finished;
                default:
                    break;
            }

            return Expressions.Created;
        }
    }
}