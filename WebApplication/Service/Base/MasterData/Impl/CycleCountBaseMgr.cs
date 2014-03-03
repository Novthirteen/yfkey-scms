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
    public class CycleCountBaseMgr : SessionBase, ICycleCountBaseMgr
    {
        protected ICycleCountDao entityDao;
        
        public CycleCountBaseMgr(ICycleCountDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateCycleCount(CycleCount entity)
        {
            entityDao.CreateCycleCount(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual CycleCount LoadCycleCount(String code)
        {
            return entityDao.LoadCycleCount(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<CycleCount> GetAllCycleCount()
        {
            return entityDao.GetAllCycleCount();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCycleCount(CycleCount entity)
        {
            entityDao.UpdateCycleCount(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCycleCount(String code)
        {
            entityDao.DeleteCycleCount(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCycleCount(CycleCount entity)
        {
            entityDao.DeleteCycleCount(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCycleCount(IList<String> pkList)
        {
            entityDao.DeleteCycleCount(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCycleCount(IList<CycleCount> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteCycleCount(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
