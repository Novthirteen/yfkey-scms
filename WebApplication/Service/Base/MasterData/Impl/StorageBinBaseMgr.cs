using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class StorageBinBaseMgr : SessionBase, IStorageBinBaseMgr
    {
        protected IStorageBinDao entityDao;
        
        public StorageBinBaseMgr(IStorageBinDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateStorageBin(StorageBin entity)
        {
            entityDao.CreateStorageBin(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual StorageBin LoadStorageBin(String code)
        {
            return entityDao.LoadStorageBin(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<StorageBin> GetAllStorageBin()
        {
            return entityDao.GetAllStorageBin();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateStorageBin(StorageBin entity)
        {
            entityDao.UpdateStorageBin(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteStorageBin(String code)
        {
            entityDao.DeleteStorageBin(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteStorageBin(StorageBin entity)
        {
            entityDao.DeleteStorageBin(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteStorageBin(IList<String> pkList)
        {
            entityDao.DeleteStorageBin(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteStorageBin(IList<StorageBin> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteStorageBin(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
