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
    public class ItemFlowPlanTrackBaseMgr : SessionBase, IItemFlowPlanTrackBaseMgr
    {
        protected IItemFlowPlanTrackDao entityDao;
        
        public ItemFlowPlanTrackBaseMgr(IItemFlowPlanTrackDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateItemFlowPlanTrack(ItemFlowPlanTrack entity)
        {
            entityDao.CreateItemFlowPlanTrack(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ItemFlowPlanTrack LoadItemFlowPlanTrack(Int32 id)
        {
            return entityDao.LoadItemFlowPlanTrack(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ItemFlowPlanTrack> GetAllItemFlowPlanTrack()
        {
            return entityDao.GetAllItemFlowPlanTrack();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateItemFlowPlanTrack(ItemFlowPlanTrack entity)
        {
            entityDao.UpdateItemFlowPlanTrack(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemFlowPlanTrack(Int32 id)
        {
            entityDao.DeleteItemFlowPlanTrack(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemFlowPlanTrack(ItemFlowPlanTrack entity)
        {
            entityDao.DeleteItemFlowPlanTrack(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemFlowPlanTrack(IList<Int32> pkList)
        {
            entityDao.DeleteItemFlowPlanTrack(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemFlowPlanTrack(IList<ItemFlowPlanTrack> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteItemFlowPlanTrack(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
