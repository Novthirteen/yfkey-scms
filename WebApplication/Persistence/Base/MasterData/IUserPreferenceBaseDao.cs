using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IUserPreferenceBaseDao
    {
        #region Method Created By CodeSmith

        void CreateUserPreference(UserPreference entity);

        UserPreference LoadUserPreference(com.Sconit.Entity.MasterData.User user, String code);
        UserPreference LoadUserPreference(String userCode, String code);
	
		IList<UserPreference> GetAllUserPreference();
	
        void UpdateUserPreference(UserPreference entity);
        
        void DeleteUserPreference(com.Sconit.Entity.MasterData.User user, String code);
    
        void DeleteUserPreference(String userCode, String code);
    
        void DeleteUserPreference(UserPreference entity);
    
        void DeleteUserPreference(IList<UserPreference> entityList);    
        #endregion Method Created By CodeSmith
    }
}
