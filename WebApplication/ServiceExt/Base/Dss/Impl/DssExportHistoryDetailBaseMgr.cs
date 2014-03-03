using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Dss;
using com.Sconit.Persistence.Dss;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Dss.Impl
{
    [Transactional]
    public class DssExportHistoryDetailBaseMgr : SessionBase, IDssExportHistoryDetailBaseMgr
    {
        protected IDssExportHistoryDetailDao entityDao;
        
        public DssExportHistoryDetailBaseMgr(IDssExportHistoryDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateDssExportHistoryDetail(DssExportHistoryDetail entity)
        {
            entityDao.CreateDssExportHistoryDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual DssExportHistoryDetail LoadDssExportHistoryDetail(Int32 id)
        {
            return entityDao.LoadDssExportHistoryDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<DssExportHistoryDetail> GetAllDssExportHistoryDetail()
        {
            return entityDao.GetAllDssExportHistoryDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateDssExportHistoryDetail(DssExportHistoryDetail entity)
        {
            entityDao.UpdateDssExportHistoryDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssExportHistoryDetail(Int32 id)
        {
            entityDao.DeleteDssExportHistoryDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssExportHistoryDetail(DssExportHistoryDetail entity)
        {
            entityDao.DeleteDssExportHistoryDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssExportHistoryDetail(IList<Int32> pkList)
        {
            entityDao.DeleteDssExportHistoryDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssExportHistoryDetail(IList<DssExportHistoryDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteDssExportHistoryDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
