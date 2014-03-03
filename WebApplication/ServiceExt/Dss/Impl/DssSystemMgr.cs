using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Dss;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity.Dss;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Dss.Impl
{
    [Transactional]
    public class DssSystemMgr : DssSystemBaseMgr, IDssSystemMgr
    {
        private ICriteriaMgr criteriaMgr;
        public DssSystemMgr(IDssSystemDao entityDao,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods
        
        #endregion Customized Methods
    }
}