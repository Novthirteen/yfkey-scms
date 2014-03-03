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
    public class UserPreferenceBaseMgr : SessionBase, IUserPreferenceBaseMgr
    {
        protected IUserPreferenceDao entityDao;
        
        public UserPreferenceBaseMgr(IUserPreferenceDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateUserPreference(UserPreference entity)
        {
            entityDao.CreateUserPreference(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual UserPreference LoadUserPreference(com.Sconit.Entity.MasterData.User user, String code)
        {
            return entityDao.LoadUserPreference(user, code);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual UserPreference LoadUserPreference(String userCode, String code)
        {
            return entityDao.LoadUserPreference(userCode, code);
        }

		[Transaction(TransactionMode.Requires)]
        public virtual IList<UserPreference> GetAllUserPreference()
        {
            return entityDao.GetAllUserPreference();
        }
		
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateUserPreference(UserPreference entity)
        {
            entityDao.UpdateUserPreference(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUserPreference(com.Sconit.Entity.MasterData.User user, String code)
        {
            entityDao.DeleteUserPreference(user, code);
        }
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUserPreference(String userCode, String code)
        {
            entityDao.DeleteUserPreference(userCode, code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUserPreference(UserPreference entity)
        {
            entityDao.DeleteUserPreference(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUserPreference(IList<UserPreference> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteUserPreference(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
