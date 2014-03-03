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
    public class LocationBinItemDetailBaseMgr : SessionBase, ILocationBinItemDetailBaseMgr
    {
        protected ILocationBinItemDetailDao entityDao;
        
        public LocationBinItemDetailBaseMgr(ILocationBinItemDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateLocationBinItemDetail(LocationBinItemDetail entity)
        {
            entityDao.CreateLocationBinItemDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual LocationBinItemDetail LoadLocationBinItemDetail(Int32 id)
        {
            return entityDao.LoadLocationBinItemDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<LocationBinItemDetail> GetAllLocationBinItemDetail()
        {
            return entityDao.GetAllLocationBinItemDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateLocationBinItemDetail(LocationBinItemDetail entity)
        {
            entityDao.UpdateLocationBinItemDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationBinItemDetail(Int32 id)
        {
            entityDao.DeleteLocationBinItemDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationBinItemDetail(LocationBinItemDetail entity)
        {
            entityDao.DeleteLocationBinItemDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationBinItemDetail(IList<Int32> pkList)
        {
            entityDao.DeleteLocationBinItemDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationBinItemDetail(IList<LocationBinItemDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteLocationBinItemDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
