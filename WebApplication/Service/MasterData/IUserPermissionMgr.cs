using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IUserPermissionMgr : IUserPermissionBaseMgr
    {
        #region Customized Methods

        IList<Permission> GetPermissionsNotInUser(string code);

        IList<Permission> GetPermissionsNotInUser(string code, string categoryCode);

        IList<Permission> GetPermissionsByUserCode(string code);

        IList<Permission> GetPermissionsByUserCode(string code, string categoryCode);

        UserPermission LoadUserPermission(string userCode, int permissionId);

        UserPermission LoadUserPermission(string userCode, string permissionCode);

        void CreateUserPermissions(User user, IList<Permission> permissioList);

        IList<UserPermission> GetUserPermission(string permissionCode);

        #endregion Customized Methods
    }
}
