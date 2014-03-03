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
    public class PriceListBaseMgr : SessionBase, IPriceListBaseMgr
    {
        protected IPriceListDao entityDao;
        
        public PriceListBaseMgr(IPriceListDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreatePriceList(PriceList entity)
        {
            entityDao.CreatePriceList(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual PriceList LoadPriceList(String code)
        {
            return entityDao.LoadPriceList(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<PriceList> GetAllPriceList()
        {
            return entityDao.GetAllPriceList(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<PriceList> GetAllPriceList(bool includeInactive)
        {
            return entityDao.GetAllPriceList(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdatePriceList(PriceList entity)
        {
            entityDao.UpdatePriceList(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePriceList(String code)
        {
            entityDao.DeletePriceList(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePriceList(PriceList entity)
        {
            entityDao.DeletePriceList(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePriceList(IList<String> pkList)
        {
            entityDao.DeletePriceList(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePriceList(IList<PriceList> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeletePriceList(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
