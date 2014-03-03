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
    public class FlowBaseMgr : SessionBase, IFlowBaseMgr
    {
        protected IFlowDao entityDao;
        
        public FlowBaseMgr(IFlowDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateFlow(Flow entity)
        {
            entityDao.CreateFlow(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Flow LoadFlow(String code)
        {
            return entityDao.LoadFlow(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Flow> GetAllFlow()
        {
            return entityDao.GetAllFlow(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Flow> GetAllFlow(bool includeInactive)
        {
            return entityDao.GetAllFlow(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateFlow(Flow entity)
        {
            entityDao.UpdateFlow(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlow(String code)
        {
            entityDao.DeleteFlow(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlow(Flow entity)
        {
            entityDao.DeleteFlow(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlow(IList<String> pkList)
        {
            entityDao.DeleteFlow(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlow(IList<Flow> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteFlow(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
