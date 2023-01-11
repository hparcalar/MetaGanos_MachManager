using MachManager.Context;

namespace MachManager.Business.Base{
    public class IBusinessObject : IDisposable{
        protected MetaGanosSchema _context;
        public IBusinessObject(MetaGanosSchema context){
            this._context = context;
        }

        public void Dispose(){
            
        }
    }
}