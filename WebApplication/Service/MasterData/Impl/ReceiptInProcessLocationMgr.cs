using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ReceiptInProcessLocationMgr : ReceiptInProcessLocationBaseMgr, IReceiptInProcessLocationMgr
    {
        private ICriteriaMgr criteriaMgr;
        public ReceiptInProcessLocationMgr(IReceiptInProcessLocationDao entityDao,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<ReceiptInProcessLocation> GetReceiptInProcessLocation(string ipNo, string receiptNo)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ReceiptInProcessLocation));
            if (ipNo != null && ipNo.Trim() != string.Empty)
                criteria.Add(Expression.Eq("InProcessLocation.IpNo", ipNo));
            if (receiptNo != null && receiptNo.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Receipt.ReceiptNo", receiptNo));

            return criteriaMgr.FindAll<ReceiptInProcessLocation>(criteria);
        }

        #endregion Customized Methods
    }
}