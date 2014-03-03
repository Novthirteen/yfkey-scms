using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IRolePermissionMgr : IRolePermissionBaseMgr
    {
        #region Customized Methods

        IList<Permission> GetPermissionsNotInRole(string code);

        IList<Permission> GetPermissionsByRoleCode(string code);

        IList<Permission> GetPermissionsNotInRole(string code, string categoryCode);

        IList<Permission> GetPermissionsByRoleCode(string code, string categoryCode);

        RolePermission LoadRolePermission(string roleCode, int permissionId);

        void CreateRolePermissions(Role user, IList<Permission> permissioList);

        #endregion Customized Methods
    }
}
