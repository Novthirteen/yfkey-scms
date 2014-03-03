using System;

//TODO: Add other using statements here

namespace com.Sconit.Persistence.MasterData
{
    public interface IPermissionDao : IPermissionBaseDao
    {
        #region Customized Methods

        void DeletePermission(string code);

        #endregion Customized Methods
    }
}