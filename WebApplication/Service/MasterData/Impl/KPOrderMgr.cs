using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence;
using System.Data.SqlClient;
using System.Data;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class KPOrderMgr : KPOrderBaseMgr, IKPOrderMgr
    {
        private ISqlHelperDao sqlHelperDao;

        public KPOrderMgr(IKPOrderDao entityDao,ISqlHelperDao sqlHelperDao)
            : base(entityDao)
        {
            this.sqlHelperDao = sqlHelperDao;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public KPOrder LoadKPOrder(decimal orderId, bool includeDetail)
        {
            KPOrder kpOrder = this.LoadKPOrder(orderId);

            if (includeDetail && kpOrder.KPItems != null && kpOrder.KPItems.Count > 0)
            {
               
            }

            return kpOrder;
        }

        public void ImportKPOrder(DateTime startTime)
        {
             SqlParameter []  sqlParameter=new SqlParameter[1];
             sqlParameter[0] = new SqlParameter("@CodePrefix", SqlDbType.DateTime, 50);
             sqlParameter[0].Value = startTime;
             sqlHelperDao.ExecuteStoredProcedure("USP_Busi_ImportKPOrder", sqlParameter);
        }
        #endregion Customized Methods
    }
}