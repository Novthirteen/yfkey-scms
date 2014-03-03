using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IPermissionCategoryBaseDao
    {
        #region Method Created By CodeSmith

        void CreatePermissionCategory(PermissionCategory entity);

        PermissionCategory LoadPermissionCategory(String code);

        void UpdatePermissionCategory(PermissionCategory entity);
        
        void DeletePermissionCategory(String code);
		
        void DeletePermissionCategory(PermissionCategory entity);
		
        void DeletePermissionCategory(IList<String> pkList);
		
        void DeletePermissionCategory(IList<PermissionCategory> entityList);

        #endregion Method Created By CodeSmith
    }
}
