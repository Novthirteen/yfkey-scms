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
    public class CurrencyBaseMgr : SessionBase, ICurrencyBaseMgr
    {
        protected ICurrencyDao entityDao;
        
        public CurrencyBaseMgr(ICurrencyDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateCurrency(Currency entity)
        {
            entityDao.CreateCurrency(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Currency LoadCurrency(String code)
        {
            return entityDao.LoadCurrency(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Currency> GetAllCurrency()
        {
            return entityDao.GetAllCurrency();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCurrency(Currency entity)
        {
            entityDao.UpdateCurrency(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCurrency(String code)
        {
            entityDao.DeleteCurrency(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCurrency(Currency entity)
        {
            entityDao.DeleteCurrency(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCurrency(IList<String> pkList)
        {
            entityDao.DeleteCurrency(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCurrency(IList<Currency> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteCurrency(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
