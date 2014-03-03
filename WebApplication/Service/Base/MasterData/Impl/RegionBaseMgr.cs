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
    public class RegionBaseMgr : SessionBase, IRegionBaseMgr
    {
        protected IRegionDao entityDao;
        
        public RegionBaseMgr(IRegionDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateRegion(Region entity)
        {
            entityDao.CreateRegion(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Region LoadRegion(String code)
        {
            return entityDao.LoadRegion(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Region> GetAllRegion()
        {
                return entityDao.GetAllRegion(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Region> GetAllRegion(bool includeInactive)
        {
            return entityDao.GetAllRegion(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateRegion(Region entity)
        {
            entityDao.UpdateRegion(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRegion(String code)
        {
            entityDao.DeleteRegion(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRegion(Region entity)
        {
            entityDao.DeleteRegion(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRegion(IList<String> pkList)
        {
            entityDao.DeleteRegion(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRegion(IList<Region> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteRegion(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
