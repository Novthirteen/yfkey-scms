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
    public class HuBaseMgr : SessionBase, IHuBaseMgr
    {
        protected IHuDao entityDao;
        
        public HuBaseMgr(IHuDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateHu(Hu entity)
        {
            entityDao.CreateHu(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Hu LoadHu(String huId)
        {
            return entityDao.LoadHu(huId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Hu> GetAllHu()
        {
            return entityDao.GetAllHu();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateHu(Hu entity)
        {
            entityDao.UpdateHu(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteHu(String huId)
        {
            entityDao.DeleteHu(huId);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteHu(Hu entity)
        {
            entityDao.DeleteHu(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteHu(IList<String> pkList)
        {
            entityDao.DeleteHu(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteHu(IList<Hu> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteHu(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
