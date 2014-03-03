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
    public class SupplierBaseMgr : SessionBase, ISupplierBaseMgr
    {
        protected ISupplierDao entityDao;
        
        public SupplierBaseMgr(ISupplierDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateSupplier(Supplier entity)
        {
            entityDao.CreateSupplier(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Supplier LoadSupplier(String code)
        {
            return entityDao.LoadSupplier(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Supplier> GetAllSupplier()
        {
                return entityDao.GetAllSupplier(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Supplier> GetAllSupplier(bool includeInactive)
        {
            return entityDao.GetAllSupplier(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateSupplier(Supplier entity)
        {
            entityDao.UpdateSupplier(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSupplier(String code)
        {
            entityDao.DeleteSupplier(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSupplier(Supplier entity)
        {
            entityDao.DeleteSupplier(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSupplier(IList<String> pkList)
        {
            entityDao.DeleteSupplier(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSupplier(IList<Supplier> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteSupplier(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
