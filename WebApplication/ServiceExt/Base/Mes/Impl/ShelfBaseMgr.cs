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
    public class ShelfBaseMgr : SessionBase, IShelfBaseMgr
    {
        protected IShelfDao entityDao;
        
        public ShelfBaseMgr(IShelfDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateShelf(Shelf entity)
        {
            entityDao.CreateShelf(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Shelf LoadShelf(String code)
        {
            return entityDao.LoadShelf(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Shelf> GetAllShelf()
        {
            return entityDao.GetAllShelf(false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Shelf> GetAllShelf(bool includeInactive)
        {
            return entityDao.GetAllShelf(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateShelf(Shelf entity)
        {
            entityDao.UpdateShelf(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShelf(String code)
        {
            entityDao.DeleteShelf(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShelf(Shelf entity)
        {
            entityDao.DeleteShelf(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShelf(IList<String> pkList)
        {
            entityDao.DeleteShelf(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShelf(IList<Shelf> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteShelf(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
