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
    public class DssImportHistoryMgr : DssImportHistoryBaseMgr, IDssImportHistoryMgr
    {
        private ICriteriaMgr criteriaMgr;

        public DssImportHistoryMgr(IDssImportHistoryDao entityDao,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<DssImportHistory> GetActiveDssImportHistory(int dssInboundCtrlId)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(DssImportHistory));
            criteria.Add(Expression.Eq("IsActive", true));
            criteria.Add(Expression.Eq("DssInboundCtrl.Id", dssInboundCtrlId));
            criteria.Add(Expression.Lt("ErrorCount", 1));//2012-6-12 djin 3变1 

            return criteriaMgr.FindAll<DssImportHistory>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public void CreateDssImportHistory(IList<DssImportHistory> dssImportHistoryList)
        {
            if (dssImportHistoryList != null && dssImportHistoryList.Count > 0)
            {
                foreach (var dssImportHistory in dssImportHistoryList)
                {
                    //#region 过滤掉已经导入的文件
                    //DetachedCriteria criteria = DetachedCriteria.For<DssImportHistory>();
                    //criteria.SetProjection(Projections.Count("Id"));
                    //criteria.CreateAlias("DssInboundControl", "dic");
                    //criteria.Add(Expression.Eq("dic.Id", item.DssInboundCtrl.Id));
                    //criteria.Add(Expression.Eq("KeyCode", item.KeyCode));
                    //IList<int> count = this.criteriaMgr.FindAll<int>(criteria);
                    //if (count[0] == 0) 
                    //{
                    this.CreateDssImportHistory(dssImportHistory);
                    //}
                    //#endregion
                }
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public void UpdateDssImportHistory(IList<DssImportHistory> dssImportHistoryList)
        {
            if (dssImportHistoryList != null && dssImportHistoryList.Count > 0)
            {
                foreach (var dssImportHistory in dssImportHistoryList)
                {
                    this.UpdateDssImportHistory(dssImportHistory);
                }
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public void UpdateDssImportHistory(IList<DssImportHistory> dssImportHistoryList, bool isActive)
        {
            if (dssImportHistoryList != null && dssImportHistoryList.Count > 0)
            {
                foreach (var dssImportHistory in dssImportHistoryList)
                {
                    dssImportHistory.IsActive = isActive;
                    this.UpdateDssImportHistory(dssImportHistory);
                }
            }
        }

        #endregion Customized Methods
    }
}