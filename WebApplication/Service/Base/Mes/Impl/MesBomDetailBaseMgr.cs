using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Mes;
using com.Sconit.Persistence.Mes;


//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes.Impl
{
    [Transactional]
    public class MesBomDetailBaseMgr : SessionBase, IMesBomDetailBaseMgr
    {
        protected IMesBomDetailDao entityDao;

        public MesBomDetailBaseMgr(IMesBomDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBomDetail(MesBomDetail entity)
        {
            entityDao.CreateBomDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual MesBomDetail LoadBomDetail(Int32 id)
        {
            return entityDao.LoadBomDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MesBomDetail> GetAllBomDetail()
        {
            return entityDao.GetAllBomDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBomDetail(MesBomDetail entity)
        {
            entityDao.UpdateBomDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBomDetail(Int32 id)
        {
            entityDao.DeleteBomDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBomDetail(MesBomDetail entity)
        {
            entityDao.DeleteBomDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBomDetail(IList<Int32> pkList)
        {
            entityDao.DeleteBomDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBomDetail(IList<MesBomDetail> entityList)
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
