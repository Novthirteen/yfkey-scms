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
    public class RolePermissionBaseMgr : SessionBase, IRolePermissionBaseMgr
    {
        protected IRolePermissionDao entityDao;
        
        public RolePermissionBaseMgr(IRolePermissionDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateRolePermission(RolePermission entity)
        {
            entityDao.CreateRolePermission(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual RolePermission LoadRolePermission(Int32 id)
        {
            return entityDao.LoadRolePermission(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<RolePermission> GetAllRolePermission()
        {
            return entityDao.GetAllRolePermission();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateRolePermission(RolePermission entity)
        {
            entityDao.UpdateRolePermission(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRolePermission(Int32 id)
        {
            entityDao.DeleteRolePermission(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRolePermission(RolePermission entity)
        {
            entityDao.DeleteRolePermission(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRolePermission(IList<Int32> pkList)
        {
            entityDao.DeleteRolePermission(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteRolePermission(IList<RolePermission> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteRolePermission(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
