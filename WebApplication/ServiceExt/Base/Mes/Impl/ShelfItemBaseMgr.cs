using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Mes;
using com.Sconit.Persistence.Mes;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes.Impl
{
    [Transactional]
    public class ShelfItemBaseMgr : SessionBase, IShelfItemBaseMgr
    {
        protected IShelfItemDao entityDao;
        
        public ShelfItemBaseMgr(IShelfItemDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateShelfItem(ShelfItem entity)
        {
            entityDao.CreateShelfItem(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ShelfItem LoadShelfItem(Int32 id)
        {
            return entityDao.LoadShelfItem(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ShelfItem> GetAllShelfItem()
        {
            return entityDao.GetAllShelfItem();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateShelfItem(ShelfItem entity)
        {
            entityDao.UpdateShelfItem(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShelfItem(Int32 id)
        {
            entityDao.DeleteShelfItem(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShelfItem(ShelfItem entity)
        {
            entityDao.DeleteShelfItem(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShelfItem(IList<Int32> pkList)
        {
            entityDao.DeleteShelfItem(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShelfItem(IList<ShelfItem> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteShelfItem(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
