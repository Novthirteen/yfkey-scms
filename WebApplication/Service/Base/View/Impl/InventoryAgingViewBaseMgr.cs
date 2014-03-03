using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.View;
using com.Sconit.Persistence.View;

//TODO: Add other using statements here.

namespace com.Sconit.Service.View.Impl
{
    [Transactional]
    public class InventoryAgingViewBaseMgr : SessionBase, IInventoryAgingViewBaseMgr
    {
        protected IInventoryAgingViewDao entityDao;
        
        public InventoryAgingViewBaseMgr(IInventoryAgingViewDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateInventoryAgingView(InventoryAgingView entity)
        {
            entityDao.CreateInventoryAgingView(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual InventoryAgingView LoadInventoryAgingView(Int32 id)
        {
            return entityDao.LoadInventoryAgingView(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<InventoryAgingView> GetAllInventoryAgingView()
        {
            return entityDao.GetAllInventoryAgingView();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateInventoryAgingView(InventoryAgingView entity)
        {
            entityDao.UpdateInventoryAgingView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInventoryAgingView(Int32 id)
        {
            entityDao.DeleteInventoryAgingView(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInventoryAgingView(InventoryAgingView entity)
        {
            entityDao.DeleteInventoryAgingView(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInventoryAgingView(IList<Int32> pkList)
        {
            entityDao.DeleteInventoryAgingView(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInventoryAgingView(IList<InventoryAgingView> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteInventoryAgingView(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
