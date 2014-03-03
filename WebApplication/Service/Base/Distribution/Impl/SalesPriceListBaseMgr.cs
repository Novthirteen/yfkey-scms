using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Distribution;
using com.Sconit.Persistence.Distribution;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Distribution.Impl
{
    [Transactional]
    public class SalesPriceListBaseMgr : SessionBase, ISalesPriceListBaseMgr
    {
        protected ISalesPriceListDao entityDao;
        
        public SalesPriceListBaseMgr(ISalesPriceListDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateSalesPriceList(SalesPriceList entity)
        {
            entityDao.CreateSalesPriceList(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual SalesPriceList LoadSalesPriceList(String code)
        {
            return entityDao.LoadSalesPriceList(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<SalesPriceList> GetAllSalesPriceList()
        {
            return entityDao.GetAllSalesPriceList(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<SalesPriceList> GetAllSalesPriceList(bool includeInactive)
        {
            return entityDao.GetAllSalesPriceList(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateSalesPriceList(SalesPriceList entity)
        {
            entityDao.UpdateSalesPriceList(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSalesPriceList(String code)
        {
            entityDao.DeleteSalesPriceList(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSalesPriceList(SalesPriceList entity)
        {
            entityDao.DeleteSalesPriceList(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSalesPriceList(IList<String> pkList)
        {
            entityDao.DeleteSalesPriceList(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSalesPriceList(IList<SalesPriceList> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteSalesPriceList(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
