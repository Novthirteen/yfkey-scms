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
    public class RepackBaseMgr : SessionBase, IRepackBaseMgr
    {
        protected IRepackDao entityDao;
        
        public RepackBaseMgr(IRepackDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateRepack(Repack entity)
        {
            entityDao.CreateRepack(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Repack LoadRepack(String repackNo)
        {
            return entityDao.LoadRepack(repackNo);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Repack> GetAllRepack()
        {
            return entityDao.GetAllRepack();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateRepack(Repack entity)
        {
            entityDao.UpdateRepack(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRepack(String repackNo)
        {
            entityDao.DeleteRepack(repackNo);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRepack(Repack entity)
        {
            entityDao.DeleteRepack(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRepack(IList<String> pkList)
        {
            entityDao.DeleteRepack(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRepack(IList<Repack> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteRepack(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
