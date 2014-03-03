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
    public class RoutingBaseMgr : SessionBase, IRoutingBaseMgr
    {
        protected IRoutingDao entityDao;
        
        public RoutingBaseMgr(IRoutingDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateRouting(Routing entity)
        {
            entityDao.CreateRouting(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Routing LoadRouting(String code)
        {
            return entityDao.LoadRouting(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Routing> GetAllRouting()
        {
            return entityDao.GetAllRouting(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Routing> GetAllRouting(bool includeInactive)
        {
            return entityDao.GetAllRouting(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateRouting(Routing entity)
        {
            entityDao.UpdateRouting(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRouting(String code)
        {
            entityDao.DeleteRouting(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRouting(Routing entity)
        {
            entityDao.DeleteRouting(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRouting(IList<String> pkList)
        {
            entityDao.DeleteRouting(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRouting(IList<Routing> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteRouting(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
