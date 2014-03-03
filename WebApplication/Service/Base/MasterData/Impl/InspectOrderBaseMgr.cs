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
    public class InspectOrderBaseMgr : SessionBase, IInspectOrderBaseMgr
    {
        protected IInspectOrderDao entityDao;
        
        public InspectOrderBaseMgr(IInspectOrderDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateInspectOrder(InspectOrder entity)
        {
            entityDao.CreateInspectOrder(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual InspectOrder LoadInspectOrder(String inspectNo)
        {
            return entityDao.LoadInspectOrder(inspectNo);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<InspectOrder> GetAllInspectOrder()
        {
            return entityDao.GetAllInspectOrder();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateInspectOrder(InspectOrder entity)
        {
            entityDao.UpdateInspectOrder(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInspectOrder(String inspectNo)
        {
            entityDao.DeleteInspectOrder(inspectNo);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInspectOrder(InspectOrder entity)
        {
            entityDao.DeleteInspectOrder(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInspectOrder(IList<String> pkList)
        {
            entityDao.DeleteInspectOrder(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInspectOrder(IList<InspectOrder> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteInspectOrder(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
