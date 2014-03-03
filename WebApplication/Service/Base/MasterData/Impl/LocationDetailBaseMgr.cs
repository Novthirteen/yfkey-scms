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
    public class LocationDetailBaseMgr : SessionBase, ILocationDetailBaseMgr
    {
        protected ILocationDetailDao entityDao;
        
        public LocationDetailBaseMgr(ILocationDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateLocationDetail(LocationDetail entity)
        {
            entityDao.CreateLocationDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual LocationDetail LoadLocationDetail(Int32 id)
        {
            return entityDao.LoadLocationDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<LocationDetail> GetAllLocationDetail()
        {
            return entityDao.GetAllLocationDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateLocationDetail(LocationDetail entity)
        {
            entityDao.UpdateLocationDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationDetail(Int32 id)
        {
            entityDao.DeleteLocationDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationDetail(LocationDetail entity)
        {
            entityDao.DeleteLocationDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationDetail(IList<Int32> pkList)
        {
            entityDao.DeleteLocationDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationDetail(IList<LocationDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteLocationDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
