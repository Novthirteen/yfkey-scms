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
    public class PriceListDetailBaseMgr : SessionBase, IPriceListDetailBaseMgr
    {
        protected IPriceListDetailDao entityDao;
        
        public PriceListDetailBaseMgr(IPriceListDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreatePriceListDetail(PriceListDetail entity)
        {
            entityDao.CreatePriceListDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual PriceListDetail LoadPriceListDetail(Int32 id)
        {
            return entityDao.LoadPriceListDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<PriceListDetail> GetAllPriceListDetail()
        {
            return entityDao.GetAllPriceListDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdatePriceListDetail(PriceListDetail entity)
        {
            entityDao.UpdatePriceListDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePriceListDetail(Int32 id)
        {
            entityDao.DeletePriceListDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePriceListDetail(PriceListDetail entity)
        {
            entityDao.DeletePriceListDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePriceListDetail(IList<Int32> pkList)
        {
            entityDao.DeletePriceListDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePriceListDetail(IList<PriceListDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeletePriceListDetail(entityList);
        }   
        
        [Transaction(TransactionMode.Unspecified)]
        public virtual PriceListDetail LoadPriceListDetail(com.Sconit.Entity.MasterData.PriceList priceList, DateTime startDate, com.Sconit.Entity.MasterData.Item item, Currency currency)
        {
            return entityDao.LoadPriceListDetail(priceList, startDate, item, currency);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePriceListDetail(String priceListCode, DateTime startDate, String itemCode, String currencyCode)
        {
            entityDao.DeletePriceListDetail(priceListCode, startDate, itemCode, currencyCode);
        }   
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual PriceListDetail LoadPriceListDetail(String priceListCode, DateTime startDate, String itemCode, String currencyCode)
        {
            return entityDao.LoadPriceListDetail(priceListCode, startDate, itemCode, currencyCode);
        }
        #endregion Method Created By CodeSmith
    }
}
