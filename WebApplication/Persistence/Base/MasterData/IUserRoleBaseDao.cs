using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IUserRoleBaseDao
    {
        #region Method Created By CodeSmith

        void CreateUserRole(UserRole entity);

        UserRole LoadUserRole(Int32 id);
  
        IList<UserRole> GetAllUserRole();
  
        void UpdateUserRole(UserRole entity);
        
        void DeleteUserRole(Int32 id);
    
        void DeleteUserRole(UserRole entity);
    
        void DeleteUserRole(IList<Int32> pkList);
    
        void DeleteUserRole(IList<UserRole> entityList);    
        #endregion Method Created By CodeSmith
    }
}
