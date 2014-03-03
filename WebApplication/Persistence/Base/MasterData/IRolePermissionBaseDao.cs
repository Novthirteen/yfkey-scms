using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IRolePermissionBaseDao
    {
        #region Method Created By CodeSmith

        void CreateRolePermission(RolePermission entity);

        RolePermission LoadRolePermission(Int32 id);
  
        IList<RolePermission> GetAllRolePermission();
  
        void UpdateRolePermission(RolePermission entity);
        
        void DeleteRolePermission(Int32 id);
    
        void DeleteRolePermission(RolePermission entity);
    
        void DeleteRolePermission(IList<Int32> pkList);
    
        void DeleteRolePermission(IList<RolePermission> entityList);    
        #endregion Method Created By CodeSmith
    }
}
