using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IUserRoleMgr : IUserRoleBaseMgr
    {
        #region Customized Methods

        IList<Role> GetRolesNotInUser(string userCode);

        IList<Role> GetRolesByUserCode(string userCcode);

        IList<User> GetUsersNotInRole(string roleCode);

        IList<User> GetUsersByRoleCode(string roleCode);

        UserRole LoadUserRole(string userCode, string roleCode);

        void CreateUserRoles(User user, IList<Role> roleList);

        void CreateUserRoles(IList<User> userList, Role role);

        #endregion Customized Methods
    }
}
