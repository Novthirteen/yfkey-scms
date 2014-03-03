using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Service.Criteria;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class BillAddressMgr : BillAddressBaseMgr, IBillAddressMgr
    {
        private ICriteriaMgr criteriaMgr;
        public BillAddressMgr(IBillAddressDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public BillAddress GetDefaultBillAddress(string party)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BillAddress>();
            criteria.Add(Expression.Eq("Party.Code", party));
            criteria.Add(Expression.Eq("IsPrimary",true));
            criteria.Add(Expression.Eq("IsActive", true));
            IList<BillAddress> billAddressList = this.criteriaMgr.FindAll<BillAddress>(criteria);
            if (billAddressList.Count > 0)
            {
                return billAddressList[0];
            }
            else
            {
                return null;
            }
        }
        #endregion Customized Methods
    }
}