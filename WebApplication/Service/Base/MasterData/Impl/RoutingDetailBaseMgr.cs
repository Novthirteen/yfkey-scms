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
    public class RoutingDetailBaseMgr : SessionBase, IRoutingDetailBaseMgr
    {
        protected IRoutingDetailDao entityDao;
        
        public RoutingDetailBaseMgr(IRoutingDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateRoutingDetail(RoutingDetail entity)
        {
            entityDao.CreateRoutingDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual RoutingDetail LoadRoutingDetail(Int32 id)
        {
            return entityDao.LoadRoutingDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<RoutingDetail> GetAllRoutingDetail()
        {
            return entityDao.GetAllRoutingDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateRoutingDetail(RoutingDetail entity)
        {
            entityDao.UpdateRoutingDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRoutingDetail(Int32 id)
        {
            entityDao.DeleteRoutingDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRoutingDetail(RoutingDetail entity)
        {
            entityDao.DeleteRoutingDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRoutingDetail(IList<Int32> pkList)
        {
            entityDao.DeleteRoutingDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRoutingDetail(IList<RoutingDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteRoutingDetail(entityList);
        }   
        
        [Transaction(TransactionMode.Unspecified)]
        public virtual RoutingDetail LoadRoutingDetail(com.Sconit.Entity.MasterData.Routing routing, Int32 operation, String reference)
        {
            return entityDao.LoadRoutingDetail(routing, operation, reference);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRoutingDetail(String routingCode, Int32 operation, String reference)
        {
            entityDao.DeleteRoutingDetail(routingCode, operation, reference);
        }   
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual RoutingDetail LoadRoutingDetail(String routingCode, Int32 operation, String reference)
        {
            return entityDao.LoadRoutingDetail(routingCode, operation, reference);
        }
        #endregion Method Created By CodeSmith
    }
}
