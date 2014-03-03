using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Transportation;
using com.Sconit.Persistence.Transportation;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class TransportationRouteBaseMgr : SessionBase, ITransportationRouteBaseMgr
    {
        protected ITransportationRouteDao entityDao;
        
        public TransportationRouteBaseMgr(ITransportationRouteDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateTransportationRoute(TransportationRoute entity)
        {
            entityDao.CreateTransportationRoute(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual TransportationRoute LoadTransportationRoute(String code)
        {
            return entityDao.LoadTransportationRoute(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<TransportationRoute> GetAllTransportationRoute()
        {
            return entityDao.GetAllTransportationRoute(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<TransportationRoute> GetAllTransportationRoute(bool includeInactive)
        {
            return entityDao.GetAllTransportationRoute(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateTransportationRoute(TransportationRoute entity)
        {
            entityDao.UpdateTransportationRoute(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationRoute(String code)
        {
            entityDao.DeleteTransportationRoute(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationRoute(TransportationRoute entity)
        {
            entityDao.DeleteTransportationRoute(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationRoute(IList<String> pkList)
        {
            entityDao.DeleteTransportationRoute(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationRoute(IList<TransportationRoute> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteTransportationRoute(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
