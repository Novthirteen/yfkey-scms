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
    public class CycleCountDetailBaseMgr : SessionBase, ICycleCountDetailBaseMgr
    {
        protected ICycleCountDetailDao entityDao;
        
        public CycleCountDetailBaseMgr(ICycleCountDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateCycleCountDetail(CycleCountDetail entity)
        {
            entityDao.CreateCycleCountDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual CycleCountDetail LoadCycleCountDetail(Int32 id)
        {
            return entityDao.LoadCycleCountDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<CycleCountDetail> GetAllCycleCountDetail()
        {
            return entityDao.GetAllCycleCountDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCycleCountDetail(CycleCountDetail entity)
        {
            entityDao.UpdateCycleCountDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCycleCountDetail(Int32 id)
        {
            entityDao.DeleteCycleCountDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCycleCountDetail(CycleCountDetail entity)
        {
            entityDao.DeleteCycleCountDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCycleCountDetail(IList<Int32> pkList)
        {
            entityDao.DeleteCycleCountDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCycleCountDetail(IList<CycleCountDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteCycleCountDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
