using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MRP;
using com.Sconit.Persistence.MRP;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP.Impl
{
    [Transactional]
    public class ExpectTransitInventoryBaseMgr : SessionBase, IExpectTransitInventoryBaseMgr
    {
        protected IExpectTransitInventoryDao entityDao;

        public ExpectTransitInventoryBaseMgr(IExpectTransitInventoryDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateExpectTransitInventory(ExpectTransitInventory entity)
        {
            entityDao.CreateExpectTransitInventory(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ExpectTransitInventory LoadExpectTransitInventory(Int32 id)
        {
            return entityDao.LoadExpectTransitInventory(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ExpectTransitInventory> GetAllExpectTransitInventory()
        {
            return entityDao.GetAllExpectTransitInventory();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateExpectTransitInventory(ExpectTransitInventory entity)
        {
            entityDao.UpdateExpectTransitInventory(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteExpectTransitInventory(Int32 id)
        {
            entityDao.DeleteExpectTransitInventory(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteExpectTransitInventory(ExpectTransitInventory entity)
        {
            entityDao.DeleteExpectTransitInventory(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteExpectTransitInventory(IList<Int32> pkList)
        {
            entityDao.DeleteExpectTransitInventory(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteExpectTransitInventory(IList<ExpectTransitInventory> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteExpectTransitInventory(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
