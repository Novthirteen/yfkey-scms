using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IRoleBaseDao
    {
        #region Method Created By CodeSmith

        void CreateRole(Role entity);

        Role LoadRole(String code);
  
        IList<Role> GetAllRole();
  
        void UpdateRole(Role entity);
        
        void DeleteRole(String code);
    
        void DeleteRole(Role entity);
    
        void DeleteRole(IList<String> pkList);
    
        void DeleteRole(IList<Role> entityList);    
        #endregion Method Created By CodeSmith
    }
}
