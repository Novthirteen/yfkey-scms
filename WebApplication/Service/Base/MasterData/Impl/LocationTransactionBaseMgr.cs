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
    public class LocationTransactionBaseMgr : SessionBase, ILocationTransactionBaseMgr
    {
        protected ILocationTransactionDao entityDao;
        
        public LocationTransactionBaseMgr(ILocationTransactionDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateLocationTransaction(LocationTransaction entity)
        {
            entityDao.CreateLocationTransaction(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual LocationTransaction LoadLocationTransaction(Int32 id)
        {
            return entityDao.LoadLocationTransaction(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<LocationTransaction> GetAllLocationTransaction()
        {
            return entityDao.GetAllLocationTransaction();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateLocationTransaction(LocationTransaction entity)
        {
            entityDao.UpdateLocationTransaction(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationTransaction(Int32 id)
        {
            entityDao.DeleteLocationTransaction(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationTransaction(LocationTransaction entity)
        {
            entityDao.DeleteLocationTransaction(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationTransaction(IList<Int32> pkList)
        {
            entityDao.DeleteLocationTransaction(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationTransaction(IList<LocationTransaction> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteLocationTransaction(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
