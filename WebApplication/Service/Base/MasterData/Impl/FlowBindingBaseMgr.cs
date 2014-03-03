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
    public class FlowBindingBaseMgr : SessionBase, IFlowBindingBaseMgr
    {
        protected IFlowBindingDao entityDao;
        
        public FlowBindingBaseMgr(IFlowBindingDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateFlowBinding(FlowBinding entity)
        {
            entityDao.CreateFlowBinding(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual FlowBinding LoadFlowBinding(Int32 id)
        {
            return entityDao.LoadFlowBinding(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<FlowBinding> GetAllFlowBinding()
        {
            return entityDao.GetAllFlowBinding();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateFlowBinding(FlowBinding entity)
        {
            entityDao.UpdateFlowBinding(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowBinding(Int32 id)
        {
            entityDao.DeleteFlowBinding(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowBinding(FlowBinding entity)
        {
            entityDao.DeleteFlowBinding(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowBinding(IList<Int32> pkList)
        {
            entityDao.DeleteFlowBinding(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowBinding(IList<FlowBinding> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteFlowBinding(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
