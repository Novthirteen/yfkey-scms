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
    public class LocationLotDetailViewBaseMgr : SessionBase, ILocationLotDetailViewBaseMgr
    {
        protected ILocationLotDetailViewDao entityDao;
        
        public LocationLotDetailViewBaseMgr(ILocationLotDetailViewDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateLocationLotDetailView(LocationLotDetailView entity)
        {
            entityDao.CreateLocationLotDetailView(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual LocationLotDetailView LoadLocationLotDetailView(Int32 id)
        {
            return entityDao.LoadLocationLotDetailView(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<LocationLotDetailView> GetAllLocationLotDetailView()
        {
            return entityDao.GetAllLocationLotDetailView();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateLocationLotDetailView(LocationLotDetailView entity)
        {
            entityDao.UpdateLocationLotDetailView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationLotDetailView(Int32 id)
        {
            entityDao.DeleteLocationLotDetailView(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationLotDetailView(LocationLotDetailView entity)
        {
            entityDao.DeleteLocationLotDetailView(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationLotDetailView(IList<Int32> pkList)
        {
            entityDao.DeleteLocationLotDetailView(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationLotDetailView(IList<LocationLotDetailView> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteLocationLotDetailView(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
