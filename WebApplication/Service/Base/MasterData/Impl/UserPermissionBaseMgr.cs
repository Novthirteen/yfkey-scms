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
    public class UserPermissionBaseMgr : SessionBase, IUserPermissionBaseMgr
    {
        protected IUserPermissionDao entityDao;
        
        public UserPermissionBaseMgr(IUserPermissionDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateUserPermission(UserPermission entity)
        {
            entityDao.CreateUserPermission(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual UserPermission LoadUserPermission(Int32 id)
        {
            return entityDao.LoadUserPermission(id);
        }

		[Transaction(TransactionMode.Requires)]
        public virtual IList<UserPermission> GetAllUserPermission()
        {
            return entityDao.GetAllUserPermission();
        }
		
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateUserPermission(UserPermission entity)
        {
            entityDao.UpdateUserPermission(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUserPermission(Int32 id)
        {
            entityDao.DeleteUserPermission(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUserPermission(UserPermission entity)
        {
            entityDao.DeleteUserPermission(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUserPermission(IList<Int32> pkList)
        {
            entityDao.DeleteUserPermission(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUserPermission(IList<UserPermission> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteUserPermission(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
