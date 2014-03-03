using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IPermissionBaseMgr
    {
        #region Method Created By CodeSmith

        void CreatePermission(Permission entity);

        Permission LoadPermission(Int32 id);
		
        void UpdatePermission(Permission entity);

        void DeletePermission(Int32 id);
		
        void DeletePermission(Permission entity);
		
        void DeletePermission(IList<Int32> pkList);
		
        void DeletePermission(IList<Permission> entityList);
		
		//Permission GetPermission(String code);
		
        //void DeletePermission(IList<String> UniqueList);
		
        #endregion Method Created By CodeSmith
    }
}
