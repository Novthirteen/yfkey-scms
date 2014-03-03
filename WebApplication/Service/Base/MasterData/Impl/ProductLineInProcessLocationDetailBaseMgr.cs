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
    public class ProductLineInProcessLocationDetailBaseMgr : SessionBase, IProductLineInProcessLocationDetailBaseMgr
    {
        protected IProductLineInProcessLocationDetailDao entityDao;
        
        public ProductLineInProcessLocationDetailBaseMgr(IProductLineInProcessLocationDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateProductLineInProcessLocationDetail(ProductLineInProcessLocationDetail entity)
        {
            entityDao.CreateProductLineInProcessLocationDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ProductLineInProcessLocationDetail LoadProductLineInProcessLocationDetail(Int32 id)
        {
            return entityDao.LoadProductLineInProcessLocationDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ProductLineInProcessLocationDetail> GetAllProductLineInProcessLocationDetail()
        {
            return entityDao.GetAllProductLineInProcessLocationDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateProductLineInProcessLocationDetail(ProductLineInProcessLocationDetail entity)
        {
            entityDao.UpdateProductLineInProcessLocationDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteProductLineInProcessLocationDetail(Int32 id)
        {
            entityDao.DeleteProductLineInProcessLocationDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteProductLineInProcessLocationDetail(ProductLineInProcessLocationDetail entity)
        {
            entityDao.DeleteProductLineInProcessLocationDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteProductLineInProcessLocationDetail(IList<Int32> pkList)
        {
            entityDao.DeleteProductLineInProcessLocationDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteProductLineInProcessLocationDetail(IList<ProductLineInProcessLocationDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteProductLineInProcessLocationDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
