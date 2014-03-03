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
    public class HuOddBaseMgr : SessionBase, IHuOddBaseMgr
    {
        protected IHuOddDao entityDao;
        
        public HuOddBaseMgr(IHuOddDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateHuOdd(HuOdd entity)
        {
            entityDao.CreateHuOdd(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual HuOdd LoadHuOdd(Int32 id)
        {
            return entityDao.LoadHuOdd(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<HuOdd> GetAllHuOdd()
        {
            return entityDao.GetAllHuOdd();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateHuOdd(HuOdd entity)
        {
            entityDao.UpdateHuOdd(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteHuOdd(Int32 id)
        {
            entityDao.DeleteHuOdd(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteHuOdd(HuOdd entity)
        {
            entityDao.DeleteHuOdd(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteHuOdd(IList<Int32> pkList)
        {
            entityDao.DeleteHuOdd(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteHuOdd(IList<HuOdd> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteHuOdd(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
