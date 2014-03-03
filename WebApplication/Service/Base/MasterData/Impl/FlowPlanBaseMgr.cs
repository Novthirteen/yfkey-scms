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
    public class FlowPlanBaseMgr : SessionBase, IFlowPlanBaseMgr
    {
        protected IFlowPlanDao entityDao;
        
        public FlowPlanBaseMgr(IFlowPlanDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateFlowPlan(FlowPlan entity)
        {
            entityDao.CreateFlowPlan(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual FlowPlan LoadFlowPlan(Int32 id)
        {
            return entityDao.LoadFlowPlan(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<FlowPlan> GetAllFlowPlan()
        {
            return entityDao.GetAllFlowPlan();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateFlowPlan(FlowPlan entity)
        {
            entityDao.UpdateFlowPlan(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowPlan(Int32 id)
        {
            entityDao.DeleteFlowPlan(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowPlan(FlowPlan entity)
        {
            entityDao.DeleteFlowPlan(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowPlan(IList<Int32> pkList)
        {
            entityDao.DeleteFlowPlan(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowPlan(IList<FlowPlan> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteFlowPlan(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
