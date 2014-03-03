using Castle.Services.Transaction;
using com.Sconit.Persistence.Distribution;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Distribution.Impl
{
    [Transactional]
    public class CustomerRollingPlanDetailMgr : CustomerRollingPlanDetailBaseMgr, ICustomerRollingPlanDetailMgr
    {
        public CustomerRollingPlanDetailMgr(ICustomerRollingPlanDetailDao entityDao)
            : base(entityDao)
        {
        }

        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods
    }
}