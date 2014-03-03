using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class MiscOrderDetailMgr : MiscOrderDetailBaseMgr, IMiscOrderDetailMgr
    {
        private ICriteriaMgr criteriaMgr;
        public MiscOrderDetailMgr(IMiscOrderDetailDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<MiscOrderDetail> GetMiscOrderDetail(string orderNo)
        {
            DetachedCriteria criteria = DetachedCriteria.For<MiscOrderDetail>();
            criteria.Add(Expression.Eq("MiscOrder.OrderNo", orderNo));
            return this.criteriaMgr.FindAll<MiscOrderDetail>(criteria);
        }

        #endregion Customized Methods
    }
}