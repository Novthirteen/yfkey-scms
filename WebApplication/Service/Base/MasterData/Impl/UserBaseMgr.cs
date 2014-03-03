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
    public class UserBaseMgr : SessionBase, IUserBaseMgr
    {
        protected IUserDao entityDao;
        
        public UserBaseMgr(IUserDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateUser(User entity)
        {
            entityDao.CreateUser(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual IList<User> GetAllUser()
        {
            return entityDao.GetAllUser(false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<User> GetAllUser(bool includeInactive)
        {
            return entityDao.GetAllUser(includeInactive);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual User LoadUser(string code)
        {
            return entityDao.LoadUser(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateUser(User entity)
        {
            entityDao.UpdateUser(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUser(string code)
        {
            entityDao.DeleteUser(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUser(User entity)
        {
            entityDao.DeleteUser(entity);
        }
		
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUser(IList<String> pkList)
        {
            entityDao.DeleteUser(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUser(IList<User> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteUser(entityList);
        }

        #endregion Method Created By CodeSmith
    }
}
