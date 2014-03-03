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
    public class StorageAreaBaseMgr : SessionBase, IStorageAreaBaseMgr
    {
        protected IStorageAreaDao entityDao;
        
        public StorageAreaBaseMgr(IStorageAreaDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateStorageArea(StorageArea entity)
        {
            entityDao.CreateStorageArea(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual StorageArea LoadStorageArea(String code)
        {
            return entityDao.LoadStorageArea(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<StorageArea> GetAllStorageArea()
        {
            return entityDao.GetAllStorageArea();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateStorageArea(StorageArea entity)
        {
            entityDao.UpdateStorageArea(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteStorageArea(String code)
        {
            entityDao.DeleteStorageArea(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteStorageArea(StorageArea entity)
        {
            entityDao.DeleteStorageArea(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteStorageArea(IList<String> pkList)
        {
            entityDao.DeleteStorageArea(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteStorageArea(IList<StorageArea> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteStorageArea(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
