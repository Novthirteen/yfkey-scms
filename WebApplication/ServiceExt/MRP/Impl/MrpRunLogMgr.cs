using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MRP;
using com.Sconit.Entity.MRP;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP.Impl
{
    [Transactional]
    public class MrpRunLogMgr : MrpRunLogBaseMgr, IMrpRunLogMgr
    {
        private ICriteriaMgr criteriaMgr;

        public MrpRunLogMgr(IMrpRunLogDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        public MrpRunLog GetLastestMrpRunLog()
        {
            return GetLastestMrpRunLog(DateTime.Now);
        }

        public MrpRunLog GetLastestMrpRunLog(DateTime effectiveDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For<MrpRunLog>();

            criteria.Add(Expression.Lt("RunDate", effectiveDate));
            criteria.AddOrder(Order.Desc("RunDate"));
            criteria.AddOrder(Order.Desc("Id"));

            IList<MrpRunLog> list = this.criteriaMgr.FindAll<MrpRunLog>(criteria, 0, 1);

            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }
        #endregion Customized Methods
    }
}
