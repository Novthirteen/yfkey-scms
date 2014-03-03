using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IUserMgr : IUserBaseMgr
    {
        #region Customized Methods

        User CheckAndLoadUser(string userCode);

        User LoadUser(String code, bool isLoadUserPreference, bool isLoadPermission);

        IList<Permission> GetAllPermissions(string userCode);

        bool HasPermission(string userCode, string permissionCode);

        IList<User> GetAllUser(DateTime LastModifyDate);

        User GetMonitorUser();

        User GetMonitorUser(bool isLoadUserPreference, bool isLoadPermission);

        #endregion Customized Methods
    }
}
