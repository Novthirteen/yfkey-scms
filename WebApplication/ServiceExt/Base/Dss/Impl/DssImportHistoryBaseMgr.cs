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
    public class DssImportHistoryBaseMgr : SessionBase, IDssImportHistoryBaseMgr
    {
        protected IDssImportHistoryDao entityDao;
        
        public DssImportHistoryBaseMgr(IDssImportHistoryDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateDssImportHistory(DssImportHistory entity)
        {
            entityDao.CreateDssImportHistory(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual DssImportHistory LoadDssImportHistory(Int32 id)
        {
            return entityDao.LoadDssImportHistory(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<DssImportHistory> GetAllDssImportHistory()
        {
            return entityDao.GetAllDssImportHistory(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<DssImportHistory> GetAllDssImportHistory(bool includeInactive)
        {
            return entityDao.GetAllDssImportHistory(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateDssImportHistory(DssImportHistory entity)
        {
            entityDao.UpdateDssImportHistory(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssImportHistory(Int32 id)
        {
            entityDao.DeleteDssImportHistory(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssImportHistory(DssImportHistory entity)
        {
            entityDao.DeleteDssImportHistory(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssImportHistory(IList<Int32> pkList)
        {
            entityDao.DeleteDssImportHistory(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssImportHistory(IList<DssImportHistory> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteDssImportHistory(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
