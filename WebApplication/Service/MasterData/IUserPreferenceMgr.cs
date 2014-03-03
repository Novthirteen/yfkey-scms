using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IUserPreferenceMgr : IUserPreferenceBaseMgr
    {
        #region Customized Methods

        bool CheckUserPreferenceUniqueExist(string userCode, string code);
        void UpdateUserPreference(IList<UserPreference> userPreferenceList);

        #endregion Customized Methods
    }
}
