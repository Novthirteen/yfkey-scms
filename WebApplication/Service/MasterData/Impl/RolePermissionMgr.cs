using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class RolePermissionMgr : RolePermissionBaseMgr, IRolePermissionMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IPermissionMgr permissionMgr;
        public RolePermissionMgr(IRolePermissionDao entityDao, ICriteriaMgr criteriaMgr, IPermissionMgr permissionMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.permissionMgr = permissionMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public IList<Permission> GetPermissionsNotInRole(string code, string categoryCode)
        {
            IList<Permission> allPermissions = permissionMgr.GetALlPermissionsByCategory(categoryCode);
            IList<Permission> rolePermissions = GetPermissionsByRoleCode(code, categoryCode);
            List<Permission> otherPermissions = new List<Permission>();
            if (allPermissions != null && allPermissions.Count > 0)
            {
                foreach (Permission r in allPermissions)
                {
                    if (!rolePermissions.Contains(r))
                    {
                        otherPermissions.Add(r);
                    }
                }
            }
            otherPermissions = otherPermissions.OrderBy(p => p.Description).ToList();
            return otherPermissions;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Permission> GetPermissionsByRoleCode(string code, string categoryCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(RolePermission)).Add(Expression.Eq("Role.Code", code));
            criteria.SetProjection(Projections.Property("Permission"));
            if (categoryCode != null)
            {
                criteria.CreateAlias("Permission", "p").CreateAlias("p.Category", "c").Add(Expression.Eq("c.Code", categoryCode));
            }
            IList<Permission> permissions = criteriaMgr.FindAll<Permission>(criteria);
            permissions = permissions.OrderBy(p => p.Description).ToList();
            return permissions;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Permission> GetPermissionsNotInRole(string code)
        {
            return GetPermissionsNotInRole(code, null);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Permission> GetPermissionsByRoleCode(string code)
        {
            return GetPermissionsByRoleCode(code, null);
        }

        [Transaction(TransactionMode.Unspecified)]
        public RolePermission LoadRolePermission(string roleCode, int permissionId)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(RolePermission)).Add(Expression.Eq("Role.Code", roleCode)).Add(Expression.Eq("Permission.Id", permissionId));
            IList<RolePermission> urList = criteriaMgr.FindAll<RolePermission>(criteria);
            if (urList.Count == 0) return null;
            return urList[0];
        }

        [Transaction(TransactionMode.Unspecified)]
        public void CreateRolePermissions(Role role, IList<Permission> rList)
        {
            foreach (Permission permission in rList)
            {
                RolePermission rolePermission = new RolePermission();
                rolePermission.Role = role;
                rolePermission.Permission = permission;
                entityDao.CreateRolePermission(rolePermission);
            }
        }

        #endregion Customized Methods
    }
}