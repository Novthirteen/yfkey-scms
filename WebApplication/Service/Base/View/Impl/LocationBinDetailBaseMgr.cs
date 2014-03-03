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
    public class LocationBinDetailBaseMgr : SessionBase, ILocationBinDetailBaseMgr
    {
        protected ILocationBinDetailDao entityDao;
        
        public LocationBinDetailBaseMgr(ILocationBinDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateLocationBinDetail(LocationBinDetail entity)
        {
            entityDao.CreateLocationBinDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual LocationBinDetail LoadLocationBinDetail(Int32 id)
        {
            return entityDao.LoadLocationBinDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<LocationBinDetail> GetAllLocationBinDetail()
        {
            return entityDao.GetAllLocationBinDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateLocationBinDetail(LocationBinDetail entity)
        {
            entityDao.UpdateLocationBinDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationBinDetail(Int32 id)
        {
            entityDao.DeleteLocationBinDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationBinDetail(LocationBinDetail entity)
        {
            entityDao.DeleteLocationBinDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationBinDetail(IList<Int32> pkList)
        {
            entityDao.DeleteLocationBinDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationBinDetail(IList<LocationBinDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteLocationBinDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
