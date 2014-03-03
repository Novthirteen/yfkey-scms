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
    public class InspectResultBaseMgr : SessionBase, IInspectResultBaseMgr
    {
        protected IInspectResultDao entityDao;
        
        public InspectResultBaseMgr(IInspectResultDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateInspectResult(InspectResult entity)
        {
            entityDao.CreateInspectResult(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual InspectResult LoadInspectResult(Int32 id)
        {
            return entityDao.LoadInspectResult(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<InspectResult> GetAllInspectResult()
        {
            return entityDao.GetAllInspectResult();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateInspectResult(InspectResult entity)
        {
            entityDao.UpdateInspectResult(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInspectResult(Int32 id)
        {
            entityDao.DeleteInspectResult(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInspectResult(InspectResult entity)
        {
            entityDao.DeleteInspectResult(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInspectResult(IList<Int32> pkList)
        {
            entityDao.DeleteInspectResult(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInspectResult(IList<InspectResult> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteInspectResult(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
