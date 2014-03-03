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
    public class ItemReferenceBaseMgr : SessionBase, IItemReferenceBaseMgr
    {
        protected IItemReferenceDao entityDao;
        
        public ItemReferenceBaseMgr(IItemReferenceDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateItemReference(ItemReference entity)
        {
            entityDao.CreateItemReference(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ItemReference LoadItemReference(Int32 id)
        {
            return entityDao.LoadItemReference(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ItemReference> GetAllItemReference()
        {
            return entityDao.GetAllItemReference(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ItemReference> GetAllItemReference(bool includeInactive)
        {
            return entityDao.GetAllItemReference(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateItemReference(ItemReference entity)
        {
            entityDao.UpdateItemReference(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemReference(Int32 id)
        {
            entityDao.DeleteItemReference(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemReference(ItemReference entity)
        {
            entityDao.DeleteItemReference(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemReference(IList<Int32> pkList)
        {
            entityDao.DeleteItemReference(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemReference(IList<ItemReference> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteItemReference(entityList);
        }   
        
        [Transaction(TransactionMode.Unspecified)]
        public virtual ItemReference LoadItemReference(com.Sconit.Entity.MasterData.Item item, com.Sconit.Entity.MasterData.Party party, String referenceCode)
        {
            return entityDao.LoadItemReference(item, party, referenceCode);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteItemReference(String itemCode, String partyCode, String referenceCode)
        {
            entityDao.DeleteItemReference(itemCode, partyCode, referenceCode);
        }   
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual ItemReference LoadItemReference(String itemCode, String partyCode, String referenceCode)
        {
            return entityDao.LoadItemReference(itemCode, partyCode, referenceCode);
        }
        #endregion Method Created By CodeSmith
    }
}
