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
    public class RoleBaseMgr : SessionBase, IRoleBaseMgr
    {
        protected IRoleDao entityDao;
        
        public RoleBaseMgr(IRoleDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateRole(Role entity)
        {
            entityDao.CreateRole(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Role LoadRole(String code)
        {
            return entityDao.LoadRole(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Role> GetAllRole()
        {
            return entityDao.GetAllRole();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateRole(Role entity)
        {
            entityDao.UpdateRole(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRole(String code)
        {
            entityDao.DeleteRole(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRole(Role entity)
        {
            entityDao.DeleteRole(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRole(IList<String> pkList)
        {
            entityDao.DeleteRole(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRole(IList<Role> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteRole(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
