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
    public class CycleCountResultBaseMgr : SessionBase, ICycleCountResultBaseMgr
    {
        protected ICycleCountResultDao entityDao;
        
        public CycleCountResultBaseMgr(ICycleCountResultDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateCycleCountResult(CycleCountResult entity)
        {
            entityDao.CreateCycleCountResult(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual CycleCountResult LoadCycleCountResult(Int32 id)
        {
            return entityDao.LoadCycleCountResult(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<CycleCountResult> GetAllCycleCountResult()
        {
            return entityDao.GetAllCycleCountResult();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCycleCountResult(CycleCountResult entity)
        {
            entityDao.UpdateCycleCountResult(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCycleCountResult(Int32 id)
        {
            entityDao.DeleteCycleCountResult(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCycleCountResult(CycleCountResult entity)
        {
            entityDao.DeleteCycleCountResult(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCycleCountResult(IList<Int32> pkList)
        {
            entityDao.DeleteCycleCountResult(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCycleCountResult(IList<CycleCountResult> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteCycleCountResult(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
