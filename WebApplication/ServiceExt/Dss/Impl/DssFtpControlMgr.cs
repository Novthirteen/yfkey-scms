using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Dss;
using NHibernate.Expression;
using com.Sconit.Entity.Dss;
using com.Sconit.Service.Criteria;

namespace com.Sconit.Service.Dss.Impl
{
    [Transactional]
    public class DssFtpControlMgr : DssFtpControlBaseMgr, IDssFtpControlMgr
    {
        private ICriteriaMgr criteriaMgr;
        public DssFtpControlMgr(IDssFtpControlDao entityDao,
             ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public IList<DssFtpControl> GetDssFtpControl(string IOType)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DssFtpControl>();

            criteria.Add(Expression.Eq("IOType", IOType));

            return this.criteriaMgr.FindAll<DssFtpControl>(criteria);
        }
        #endregion Customized Methods
    }
}