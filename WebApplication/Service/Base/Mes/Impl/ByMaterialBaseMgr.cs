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
    public class ByMaterialBaseMgr : SessionBase, IByMaterialBaseMgr
    {
        protected IByMaterialDao entityDao;
        
        public ByMaterialBaseMgr(IByMaterialDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateByMaterial(ByMaterial entity)
        {
            entityDao.CreateByMaterial(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ByMaterial LoadByMaterial(Int32 id)
        {
            return entityDao.LoadByMaterial(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ByMaterial> GetAllByMaterial()
        {
            return entityDao.GetAllByMaterial();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateByMaterial(ByMaterial entity)
        {
            entityDao.UpdateByMaterial(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteByMaterial(Int32 id)
        {
            entityDao.DeleteByMaterial(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteByMaterial(ByMaterial entity)
        {
            entityDao.DeleteByMaterial(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteByMaterial(IList<Int32> pkList)
        {
            entityDao.DeleteByMaterial(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteByMaterial(IList<ByMaterial> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteByMaterial(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
