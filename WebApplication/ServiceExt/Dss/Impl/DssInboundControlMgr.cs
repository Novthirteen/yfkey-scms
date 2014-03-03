using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Dss;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.Dss;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Dss.Impl
{
    [Transactional]
    public class DssInboundControlMgr : DssInboundControlBaseMgr, IDssInboundControlMgr
    {
        private ICriteriaMgr criteriaMgr;
        public DssInboundControlMgr(IDssInboundControlDao entityDao,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public IList<DssInboundControl> GetDssInboundControl()
        {
            DetachedCriteria criteria = DetachedCriteria.For<DssInboundControl>();

            criteria.AddOrder(Order.Asc("Sequence"));

            return this.criteriaMgr.FindAll<DssInboundControl>(criteria);
        }
        #endregion Customized Methods
    }
}