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
    public class RepackDetailBaseMgr : SessionBase, IRepackDetailBaseMgr
    {
        protected IRepackDetailDao entityDao;
        
        public RepackDetailBaseMgr(IRepackDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateRepackDetail(RepackDetail entity)
        {
            entityDao.CreateRepackDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual RepackDetail LoadRepackDetail(Int32 id)
        {
            return entityDao.LoadRepackDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<RepackDetail> GetAllRepackDetail()
        {
            return entityDao.GetAllRepackDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateRepackDetail(RepackDetail entity)
        {
            entityDao.UpdateRepackDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRepackDetail(Int32 id)
        {
            entityDao.DeleteRepackDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRepackDetail(RepackDetail entity)
        {
            entityDao.DeleteRepackDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRepackDetail(IList<Int32> pkList)
        {
            entityDao.DeleteRepackDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRepackDetail(IList<RepackDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteRepackDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
