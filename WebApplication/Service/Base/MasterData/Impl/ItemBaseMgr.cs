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
    public class ItemBaseMgr : SessionBase, IItemBaseMgr
    {
        protected IItemDao entityDao;
        
        public ItemBaseMgr(IItemDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateItem(Item entity)
        {
            entityDao.CreateItem(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Item LoadItem(String code)
        {
            return entityDao.LoadItem(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Item> GetAllItem()
        {
            return entityDao.GetAllItem(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Item> GetAllItem(bool includeInactive)
        {
            return entityDao.GetAllItem(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateItem(Item entity)
        {
            entityDao.UpdateItem(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItem(String code)
        {
            entityDao.DeleteItem(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItem(Item entity)
        {
            entityDao.DeleteItem(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItem(IList<String> pkList)
        {
            entityDao.DeleteItem(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItem(IList<Item> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteItem(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
