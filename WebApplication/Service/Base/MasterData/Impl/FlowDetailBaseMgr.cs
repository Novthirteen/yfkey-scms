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
    public class FlowDetailBaseMgr : SessionBase, IFlowDetailBaseMgr
    {
        protected IFlowDetailDao entityDao;
        
        public FlowDetailBaseMgr(IFlowDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateFlowDetail(FlowDetail entity)
        {
            entityDao.CreateFlowDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual FlowDetail LoadFlowDetail(Int32 id)
        {
            return entityDao.LoadFlowDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<FlowDetail> GetAllFlowDetail()
        {
            return entityDao.GetAllFlowDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateFlowDetail(FlowDetail entity)
        {
            entityDao.UpdateFlowDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowDetail(Int32 id)
        {
            entityDao.DeleteFlowDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowDetail(FlowDetail entity)
        {
            entityDao.DeleteFlowDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowDetail(IList<Int32> pkList)
        {
            entityDao.DeleteFlowDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowDetail(IList<FlowDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteFlowDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
