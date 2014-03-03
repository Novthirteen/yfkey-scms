using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity;
using com.Sconit.Service.Hql;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class UserMgr : UserBaseMgr, IUserMgr
    {
        private IUserPermissionMgr userPermissionMgr;
        private IUserRoleMgr userRoleMgr;
        private IRolePermissionMgr rolePermissionMgr;
        private IHqlMgr hqlMgr;
        public UserMgr(IUserDao entityDao, IUserPermissionMgr userPermissionMgr, IUserRoleMgr userRoleMgr, IRolePermissionMgr rolePermissionMgr, IHqlMgr hqlMgr)
            : base(entityDao)
        {
            this.userPermissionMgr = userPermissionMgr;
            this.userRoleMgr = userRoleMgr;
            this.rolePermissionMgr = rolePermissionMgr;
            this.hqlMgr = hqlMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public User CheckAndLoadUser(string userCode)
        {
            User user = this.LoadUser(userCode, true, true);
            if (user == null)
            {
                throw new BusinessErrorException("Security.Error.UserCodeNotExist", userCode);
            }

            return user;
        }

        [Transaction(TransactionMode.Unspecified)]
        public User LoadUser(string code, bool isLoadUserPreference, bool isLoadPermission)
        {
            User user = entityDao.LoadUser(code);
            if (isLoadUserPreference)
            {
                if (user != null && user.UserPreferences != null && user.UserPreferences.Count > 0)
                {
                    //just for lazy load user.UserPreference
                }
            }
            if (user != null && isLoadPermission)
            {
                user.Permissions = GetAllPermissions(code);
            }
            return user;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Permission> GetAllPermissions(string usrCode)
        {
            IList<Permission> userPermissions = hqlMgr.FindEntityWithNativeSql<Permission>(@"select distinct PM_Id, PM_Code, PM_Desc, PM_CateCode from (
                                        select ACC_UserPermission.UP_USRCode as UserCode, ACC_Permission.PM_Id, ACC_Permission.PM_Code, ACC_Permission.PM_Desc, ACC_Permission.PM_CateCode
                                        from dbo.ACC_UserPermission
                                        inner join dbo.ACC_Permission on ACC_UserPermission.UP_PMID = ACC_Permission.PM_ID
                                        union
                                        select ACC_UserRole.UR_USRCode as UserCode, ACC_Permission.PM_Id, ACC_Permission.PM_Code, ACC_Permission.PM_Desc, ACC_Permission.PM_CateCode
                                        from dbo.ACC_UserRole 
                                        inner join dbo.ACC_RolePermission on ACC_UserRole.UR_RoleCode = ACC_RolePermission.RP_RoleCode
                                        inner join dbo.ACC_Permission on ACC_RolePermission.RP_PMID = ACC_Permission.PM_ID
                                        ) as a where a.UserCode = ?", usrCode);


            //List<Permission> userPermissions = (List<Permission>)(userPermissionMgr.GetPermissionsByUserCode(usrCode));
            //List<Role> userRoles = (List<Role>)(userRoleMgr.GetRolesByUserCode(usrCode));
            //if (userRoles.Count > 0)
            //{
            //    foreach (Role role in userRoles)
            //    {
            //        List<Permission> rolePermissions = (List<Permission>)(rolePermissionMgr.GetPermissionsByRoleCode(role.Code));
            //        foreach (Permission p in rolePermissions)
            //        {
            //            if (!userPermissions.Contains(p))
            //            {
            //                userPermissions.Add(p);
            //            }
            //        }
            //    }
            //}
            return userPermissions;
        }

        [Transaction(TransactionMode.Requires)]
        public bool HasPermission(string userCode, string permissionCode)
        {
            IList<Permission> listPermission = GetAllPermissions(userCode);
            foreach (Permission p in listPermission)
            {
                if (p.Code == permissionCode)
                {
                    return true;
                }
            }
            return false;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<User> GetAllUser(DateTime LastModifyDate)
        {
            IList<User> userList = GetAllUser(true);
            IList<User> resulteUserList = new List<User>();
            foreach (User user in userList)
            {
                if (user.LastModifyDate > LastModifyDate)
                {
                    resulteUserList.Add(user);
                }
            }
            return resulteUserList;
        }

      
        [Transaction(TransactionMode.Unspecified)]
        public User GetMonitorUser()
        {
            return GetMonitorUser(false, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public User GetMonitorUser(bool isLoadUserPreference, bool isLoadPermission)
        {
            User user = this.LoadUser(BusinessConstants.SYSTEM_USER_MONITOR, isLoadUserPreference, isLoadPermission);
            return user;
        }

        #endregion Customized Methods
    }
}