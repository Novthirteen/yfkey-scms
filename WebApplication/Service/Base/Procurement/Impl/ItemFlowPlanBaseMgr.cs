using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Procurement;
using com.Sconit.Persistence.Procurement;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement.Impl
{
    [Transactional]
    public class ItemFlowPlanBaseMgr : SessionBase, IItemFlowPlanBaseMgr
    {
        protected IItemFlowPlanDao entityDao;
        
        public ItemFlowPlanBaseMgr(IItemFlowPlanDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateItemFlowPlan(ItemFlowPlan entity)
        {
            entityDao.CreateItemFlowPlan(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ItemFlowPlan LoadItemFlowPlan(Int32 id)
        {
            return entityDao.LoadItemFlowPlan(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ItemFlowPlan> GetAllItemFlowPlan()
        {
            return entityDao.GetAllItemFlowPlan();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateItemFlowPlan(ItemFlowPlan entity)
        {
            entityDao.UpdateItemFlowPlan(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemFlowPlan(Int32 id)
        {
            entityDao.DeleteItemFlowPlan(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemFlowPlan(ItemFlowPlan entity)
        {
            entityDao.DeleteItemFlowPlan(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemFlowPlan(IList<Int32> pkList)
        {
            entityDao.DeleteItemFlowPlan(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemFlowPlan(IList<ItemFlowPlan> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteItemFlowPlan(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
