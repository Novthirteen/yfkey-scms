using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.View;
using com.Sconit.Persistence.View;

//TODO: Add other using statements here.

namespace com.Sconit.Service.View.Impl
{
    [Transactional]
    public class FlowViewBaseMgr : SessionBase, IFlowViewBaseMgr
    {
        protected IFlowViewDao entityDao;
        
        public FlowViewBaseMgr(IFlowViewDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateFlowView(FlowView entity)
        {
            entityDao.CreateFlowView(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual FlowView LoadFlowView(Int32 id)
        {
            return entityDao.LoadFlowView(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<FlowView> GetAllFlowView()
        {
            return entityDao.GetAllFlowView();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateFlowView(FlowView entity)
        {
            entityDao.UpdateFlowView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowView(Int32 id)
        {
            entityDao.DeleteFlowView(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowView(FlowView entity)
        {
            entityDao.DeleteFlowView(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowView(IList<Int32> pkList)
        {
            entityDao.DeleteFlowView(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowView(IList<FlowView> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteFlowView(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
