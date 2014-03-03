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
    public class BomDetailBaseMgr : SessionBase, IBomDetailBaseMgr
    {
        protected IBomDetailDao entityDao;
        
        public BomDetailBaseMgr(IBomDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBomDetail(BomDetail entity)
        {
            entityDao.CreateBomDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual BomDetail LoadBomDetail(Int32 id)
        {
            return entityDao.LoadBomDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<BomDetail> GetAllBomDetail()
        {
            return entityDao.GetAllBomDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBomDetail(BomDetail entity)
        {
            entityDao.UpdateBomDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBomDetail(Int32 id)
        {
            entityDao.DeleteBomDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBomDetail(BomDetail entity)
        {
            entityDao.DeleteBomDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBomDetail(IList<Int32> pkList)
        {
            entityDao.DeleteBomDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBomDetail(IList<BomDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteBomDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
