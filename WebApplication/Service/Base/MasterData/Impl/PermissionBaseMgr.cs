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
    public class PermissionBaseMgr : SessionBase, IPermissionBaseMgr
    {
        protected IPermissionDao entityDao;
        
        public PermissionBaseMgr(IPermissionDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreatePermission(Permission entity)
        {
            entityDao.CreatePermission(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Permission LoadPermission(Int32 id)
        {
            return entityDao.LoadPermission(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdatePermission(Permission entity)
        {
            entityDao.UpdatePermission(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePermission(Int32 id)
        {
            entityDao.DeletePermission(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePermission(Permission entity)
        {
            entityDao.DeletePermission(entity);
        }
		
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePermission(IList<Int32> pkList)
        {
            entityDao.DeletePermission(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePermission(IList<Permission> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeletePermission(entityList);
        }
		
        //[Transaction(TransactionMode.Unspecified)]
        //public virtual Permission GetPermission(String code)
        //{
        //    return entityDao.LoadPermission(code);
        //}
		
        //[Transaction(TransactionMode.Requires)]
        //public virtual void DeletePermission(IList<String> UniqueList)
        //{
        //    entityDao.DeletePermission(UniqueList);
        //}
		
        #endregion Method Created By CodeSmith
    }
}
