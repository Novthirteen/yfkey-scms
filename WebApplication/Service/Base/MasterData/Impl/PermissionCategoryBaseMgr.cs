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
    public class PermissionCategoryBaseMgr : SessionBase, IPermissionCategoryBaseMgr
    {
        protected IPermissionCategoryDao entityDao;
        
        public PermissionCategoryBaseMgr(IPermissionCategoryDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreatePermissionCategory(PermissionCategory entity)
        {
            entityDao.CreatePermissionCategory(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual PermissionCategory LoadPermissionCategory(String code)
        {
            return entityDao.LoadPermissionCategory(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdatePermissionCategory(PermissionCategory entity)
        {
            entityDao.UpdatePermissionCategory(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePermissionCategory(String code)
        {
            entityDao.DeletePermissionCategory(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePermissionCategory(PermissionCategory entity)
        {
            entityDao.DeletePermissionCategory(entity);
        }
		
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePermissionCategory(IList<String> pkList)
        {
            entityDao.DeletePermissionCategory(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePermissionCategory(IList<PermissionCategory> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeletePermissionCategory(entityList);
        }

        #endregion Method Created By CodeSmith
    }
}
