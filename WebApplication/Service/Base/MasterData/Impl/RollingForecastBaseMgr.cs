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
    public class RollingForecastBaseMgr : SessionBase, IRollingForecastBaseMgr
    {
        protected IRollingForecastDao entityDao;
        
        public RollingForecastBaseMgr(IRollingForecastDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateRollingForecast(RollingForecast entity)
        {
            entityDao.CreateRollingForecast(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual RollingForecast LoadRollingForecast(Int32 id)
        {
            return entityDao.LoadRollingForecast(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<RollingForecast> GetAllRollingForecast()
        {
            return entityDao.GetAllRollingForecast();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateRollingForecast(RollingForecast entity)
        {
            entityDao.UpdateRollingForecast(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRollingForecast(Int32 id)
        {
            entityDao.DeleteRollingForecast(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRollingForecast(RollingForecast entity)
        {
            entityDao.DeleteRollingForecast(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRollingForecast(IList<Int32> pkList)
        {
            entityDao.DeleteRollingForecast(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRollingForecast(IList<RollingForecast> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteRollingForecast(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
