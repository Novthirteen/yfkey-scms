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
    public class LocationLotDetailBaseMgr : SessionBase, ILocationLotDetailBaseMgr
    {
        protected ILocationLotDetailDao entityDao;
        
        public LocationLotDetailBaseMgr(ILocationLotDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateLocationLotDetail(LocationLotDetail entity)
        {
            entityDao.CreateLocationLotDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual LocationLotDetail LoadLocationLotDetail(Int32 id)
        {
            return entityDao.LoadLocationLotDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<LocationLotDetail> GetAllLocationLotDetail()
        {
            return entityDao.GetAllLocationLotDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateLocationLotDetail(LocationLotDetail entity)
        {
            entityDao.UpdateLocationLotDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationLotDetail(Int32 id)
        {
            entityDao.DeleteLocationLotDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationLotDetail(LocationLotDetail entity)
        {
            entityDao.DeleteLocationLotDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationLotDetail(IList<Int32> pkList)
        {
            entityDao.DeleteLocationLotDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationLotDetail(IList<LocationLotDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteLocationLotDetail(entityList);
        }   
        
        #endregion Method Created By CodeSmith
    }
}
