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
    public class ProductLineUserBaseMgr : SessionBase, IProductLineUserBaseMgr
    {
        protected IProductLineUserDao entityDao;
        
        public ProductLineUserBaseMgr(IProductLineUserDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateProductLineUser(ProductLineUser entity)
        {
            entityDao.CreateProductLineUser(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ProductLineUser LoadProductLineUser(Int32 id)
        {
            return entityDao.LoadProductLineUser(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ProductLineUser> GetAllProductLineUser()
        {
            return entityDao.GetAllProductLineUser();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateProductLineUser(ProductLineUser entity)
        {
            entityDao.UpdateProductLineUser(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteProductLineUser(Int32 id)
        {
            entityDao.DeleteProductLineUser(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteProductLineUser(ProductLineUser entity)
        {
            entityDao.DeleteProductLineUser(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteProductLineUser(IList<Int32> pkList)
        {
            entityDao.DeleteProductLineUser(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteProductLineUser(IList<ProductLineUser> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteProductLineUser(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
