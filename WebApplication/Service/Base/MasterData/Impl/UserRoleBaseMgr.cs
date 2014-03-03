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
    public class UserRoleBaseMgr : SessionBase, IUserRoleBaseMgr
    {
        protected IUserRoleDao entityDao;
        
        public UserRoleBaseMgr(IUserRoleDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateUserRole(UserRole entity)
        {
            entityDao.CreateUserRole(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual UserRole LoadUserRole(Int32 id)
        {
            return entityDao.LoadUserRole(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<UserRole> GetAllUserRole()
        {
            return entityDao.GetAllUserRole();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateUserRole(UserRole entity)
        {
            entityDao.UpdateUserRole(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUserRole(Int32 id)
        {
            entityDao.DeleteUserRole(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUserRole(UserRole entity)
        {
            entityDao.DeleteUserRole(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUserRole(IList<Int32> pkList)
        {
            entityDao.DeleteUserRole(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUserRole(IList<UserRole> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteUserRole(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
