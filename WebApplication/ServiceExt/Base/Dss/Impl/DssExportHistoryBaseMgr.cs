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
    public class DssExportHistoryBaseMgr : SessionBase, IDssExportHistoryBaseMgr
    {
        protected IDssExportHistoryDao entityDao;
        
        public DssExportHistoryBaseMgr(IDssExportHistoryDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateDssExportHistory(DssExportHistory entity)
        {
            entityDao.CreateDssExportHistory(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual DssExportHistory LoadDssExportHistory(Int32 id)
        {
            return entityDao.LoadDssExportHistory(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<DssExportHistory> GetAllDssExportHistory()
        {
            return entityDao.GetAllDssExportHistory(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<DssExportHistory> GetAllDssExportHistory(bool includeInactive)
        {
            return entityDao.GetAllDssExportHistory(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateDssExportHistory(DssExportHistory entity)
        {
            entityDao.UpdateDssExportHistory(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssExportHistory(Int32 id)
        {
            entityDao.DeleteDssExportHistory(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssExportHistory(DssExportHistory entity)
        {
            entityDao.DeleteDssExportHistory(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssExportHistory(IList<Int32> pkList)
        {
            entityDao.DeleteDssExportHistory(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssExportHistory(IList<DssExportHistory> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteDssExportHistory(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
