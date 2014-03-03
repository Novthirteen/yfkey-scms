using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Dss;
using com.Sconit.Entity.Dss;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Dss.Impl
{
    [Transactional]
    public class DssOutboundControlMgr : DssOutboundControlBaseMgr, IDssOutboundControlMgr
    {
        private ICriteriaMgr criteriaMgr;
        public DssOutboundControlMgr(IDssOutboundControlDao entityDao,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public IList<DssOutboundControl> GetDssOutboundControl()
        {
            DetachedCriteria criteria = DetachedCriteria.For<DssOutboundControl>()
                .Add(Expression.Eq("IsActive", true));

            criteria.AddOrder(Order.Asc("Sequence"));

            return this.criteriaMgr.FindAll<DssOutboundControl>(criteria);
        }
        #endregion Customized Methods
    }
}