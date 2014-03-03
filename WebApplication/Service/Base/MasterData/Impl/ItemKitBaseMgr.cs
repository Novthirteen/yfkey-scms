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
    public class ItemKitBaseMgr : SessionBase, IItemKitBaseMgr
    {
        protected IItemKitDao entityDao;
        
        public ItemKitBaseMgr(IItemKitDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateItemKit(ItemKit entity)
        {
            entityDao.CreateItemKit(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ItemKit LoadItemKit(com.Sconit.Entity.MasterData.Item parentItem, com.Sconit.Entity.MasterData.Item childItem)
        {
            return entityDao.LoadItemKit(parentItem, childItem);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual ItemKit LoadItemKit(String parentItemCode, String childItemCode)
        {
            return entityDao.LoadItemKit(parentItemCode, childItemCode);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ItemKit> GetAllItemKit()
        {
            return entityDao.GetAllItemKit(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ItemKit> GetAllItemKit(bool includeInactive)
        {
            return entityDao.GetAllItemKit(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateItemKit(ItemKit entity)
        {
            entityDao.UpdateItemKit(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemKit(com.Sconit.Entity.MasterData.Item parentItem, com.Sconit.Entity.MasterData.Item childItem)
        {
            entityDao.DeleteItemKit(parentItem, childItem);
        }
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemKit(String parentItemCode, String childItemCode)
        {
            entityDao.DeleteItemKit(parentItemCode, childItemCode);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemKit(ItemKit entity)
        {
            entityDao.DeleteItemKit(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemKit(IList<ItemKit> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteItemKit(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
