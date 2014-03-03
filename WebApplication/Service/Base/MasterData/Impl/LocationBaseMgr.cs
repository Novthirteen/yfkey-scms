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
    public class LocationBaseMgr : SessionBase, ILocationBaseMgr
    {
        protected ILocationDao entityDao;
        
        public LocationBaseMgr(ILocationDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateLocation(Location entity)
        {
            entityDao.CreateLocation(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Location LoadLocation(String code)
        {
            return entityDao.LoadLocation(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Location> GetAllLocation()
        {
            return entityDao.GetAllLocation(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Location> GetAllLocation(bool includeInactive)
        {
            return entityDao.GetAllLocation(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateLocation(Location entity)
        {
            entityDao.UpdateLocation(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocation(String code)
        {
            entityDao.DeleteLocation(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocation(Location entity)
        {
            entityDao.DeleteLocation(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocation(IList<String> pkList)
        {
            entityDao.DeleteLocation(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocation(IList<Location> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteLocation(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
