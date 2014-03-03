using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IUserBaseDao
    {
        #region Method Created By CodeSmith

        void CreateUser(User entity);

        User LoadUser(string code);

        IList<User> GetAllUser();

        IList<User> GetAllUser(bool includeInactive);

        void UpdateUser(User entity);

        void DeleteUser(string code);
		
        void DeleteUser(User entity);

        void DeleteUser(IList<String> pkList);
		
        void DeleteUser(IList<User> entityList);

        #endregion Method Created By CodeSmith
    }
}
