using System;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here

namespace com.Sconit.Persistence.MasterData
{
    public interface IUserDao : IUserBaseDao
    {
        #region Customized Methods

        User FindUserByCode(String code);

        #endregion Customized Methods
    }
}