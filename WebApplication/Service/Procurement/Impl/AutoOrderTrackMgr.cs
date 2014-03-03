using Castle.Services.Transaction;
using com.Sconit.Persistence.Procurement;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement.Impl
{
    [Transactional]
    public class AutoOrderTrackMgr : AutoOrderTrackBaseMgr, IAutoOrderTrackMgr
    {
        public AutoOrderTrackMgr(IAutoOrderTrackDao entityDao)
            : base(entityDao)
        {
        }

        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods
    }
}