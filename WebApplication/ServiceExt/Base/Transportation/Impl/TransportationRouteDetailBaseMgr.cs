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
    public class TransportationRouteDetailBaseMgr : SessionBase, ITransportationRouteDetailBaseMgr
    {
        protected ITransportationRouteDetailDao entityDao;
        
        public TransportationRouteDetailBaseMgr(ITransportationRouteDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateTransportationRouteDetail(TransportationRouteDetail entity)
        {
            entityDao.CreateTransportationRouteDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual TransportationRouteDetail LoadTransportationRouteDetail(Int32 id)
        {
            return entityDao.LoadTransportationRouteDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<TransportationRouteDetail> GetAllTransportationRouteDetail()
        {
            return entityDao.GetAllTransportationRouteDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateTransportationRouteDetail(TransportationRouteDetail entity)
        {
            entityDao.UpdateTransportationRouteDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationRouteDetail(Int32 id)
        {
            entityDao.DeleteTransportationRouteDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationRouteDetail(TransportationRouteDetail entity)
        {
            entityDao.DeleteTransportationRouteDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationRouteDetail(IList<Int32> pkList)
        {
            entityDao.DeleteTransportationRouteDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationRouteDetail(IList<TransportationRouteDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteTransportationRouteDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
