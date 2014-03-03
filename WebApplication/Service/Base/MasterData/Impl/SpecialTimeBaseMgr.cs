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
    public class SpecialTimeBaseMgr : SessionBase, ISpecialTimeBaseMgr
    {
        protected ISpecialTimeDao entityDao;
        
        public SpecialTimeBaseMgr(ISpecialTimeDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateSpecialTime(SpecialTime entity)
        {
            entityDao.CreateSpecialTime(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual SpecialTime LoadSpecialTime(Int32 iD)
        {
            return entityDao.LoadSpecialTime(iD);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<SpecialTime> GetAllSpecialTime()
        {
            return entityDao.GetAllSpecialTime();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateSpecialTime(SpecialTime entity)
        {
            entityDao.UpdateSpecialTime(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSpecialTime(Int32 iD)
        {
            entityDao.DeleteSpecialTime(iD);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSpecialTime(SpecialTime entity)
        {
            entityDao.DeleteSpecialTime(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSpecialTime(IList<Int32> pkList)
        {
            entityDao.DeleteSpecialTime(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSpecialTime(IList<SpecialTime> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteSpecialTime(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
