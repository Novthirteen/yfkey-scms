using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IUserPermissionBaseDao
    {
        #region Method Created By CodeSmith

        void CreateUserPermission(UserPermission entity);

        UserPermission LoadUserPermission(Int32 id);
	
		IList<UserPermission> GetAllUserPermission();
	
        void UpdateUserPermission(UserPermission entity);
        
        void DeleteUserPermission(Int32 id);
    
        void DeleteUserPermission(UserPermission entity);
    
        void DeleteUserPermission(IList<Int32> pkList);
    
        void DeleteUserPermission(IList<UserPermission> entityList);    
        #endregion Method Created By CodeSmith
    }
}
