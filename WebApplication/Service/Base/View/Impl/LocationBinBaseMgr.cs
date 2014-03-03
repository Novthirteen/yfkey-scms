using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.View;
using com.Sconit.Persistence.View;

//TODO: Add other using statements here.

namespace com.Sconit.Service.View.Impl
{
    [Transactional]
    public class LocationBinBaseMgr : SessionBase, ILocationBinBaseMgr
    {
        protected ILocationBinDao entityDao;
        
        public LocationBinBaseMgr(ILocationBinDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateLocationBin(LocationBin entity)
        {
            entityDao.CreateLocationBin(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual LocationBin LoadLocationBin(Int32 id)
        {
            return entityDao.LoadLocationBin(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<LocationBin> GetAllLocationBin()
        {
            return entityDao.GetAllLocationBin();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateLocationBin(LocationBin entity)
        {
            entityDao.UpdateLocationBin(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationBin(Int32 id)
        {
            entityDao.DeleteLocationBin(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationBin(LocationBin entity)
        {
            entityDao.DeleteLocationBin(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationBin(IList<Int32> pkList)
        {
            entityDao.DeleteLocationBin(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationBin(IList<LocationBin> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteLocationBin(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
