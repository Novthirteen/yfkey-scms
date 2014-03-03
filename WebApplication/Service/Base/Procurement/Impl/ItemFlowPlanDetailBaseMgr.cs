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
    public class ItemFlowPlanDetailBaseMgr : SessionBase, IItemFlowPlanDetailBaseMgr
    {
        protected IItemFlowPlanDetailDao entityDao;
        
        public ItemFlowPlanDetailBaseMgr(IItemFlowPlanDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateItemFlowPlanDetail(ItemFlowPlanDetail entity)
        {
            entityDao.CreateItemFlowPlanDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ItemFlowPlanDetail LoadItemFlowPlanDetail(Int32 id)
        {
            return entityDao.LoadItemFlowPlanDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ItemFlowPlanDetail> GetAllItemFlowPlanDetail()
        {
            return entityDao.GetAllItemFlowPlanDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateItemFlowPlanDetail(ItemFlowPlanDetail entity)
        {
            entityDao.UpdateItemFlowPlanDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemFlowPlanDetail(Int32 id)
        {
            entityDao.DeleteItemFlowPlanDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemFlowPlanDetail(ItemFlowPlanDetail entity)
        {
            entityDao.DeleteItemFlowPlanDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemFlowPlanDetail(IList<Int32> pkList)
        {
            entityDao.DeleteItemFlowPlanDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemFlowPlanDetail(IList<ItemFlowPlanDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteItemFlowPlanDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
