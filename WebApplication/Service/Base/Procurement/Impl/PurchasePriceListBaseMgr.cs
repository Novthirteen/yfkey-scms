using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Procurement;
using com.Sconit.Persistence.Procurement;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement.Impl
{
    [Transactional]
    public class PurchasePriceListBaseMgr : SessionBase, IPurchasePriceListBaseMgr
    {
        protected IPurchasePriceListDao entityDao;
        
        public PurchasePriceListBaseMgr(IPurchasePriceListDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreatePurchasePriceList(PurchasePriceList entity)
        {
            entityDao.CreatePurchasePriceList(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual PurchasePriceList LoadPurchasePriceList(String code)
        {
            return entityDao.LoadPurchasePriceList(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<PurchasePriceList> GetAllPurchasePriceList()
        {
            return entityDao.GetAllPurchasePriceList(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<PurchasePriceList> GetAllPurchasePriceList(bool includeInactive)
        {
            return entityDao.GetAllPurchasePriceList(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdatePurchasePriceList(PurchasePriceList entity)
        {
            entityDao.UpdatePurchasePriceList(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePurchasePriceList(String code)
        {
            entityDao.DeletePurchasePriceList(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePurchasePriceList(PurchasePriceList entity)
        {
            entityDao.DeletePurchasePriceList(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePurchasePriceList(IList<String> pkList)
        {
            entityDao.DeletePurchasePriceList(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePurchasePriceList(IList<PurchasePriceList> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeletePurchasePriceList(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
