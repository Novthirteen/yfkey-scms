using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IPermissionCategoryMgr : IPermissionCategoryBaseMgr
    {
        #region Customized Methods

        IList<PermissionCategory> GetCategoryByType(string type);

        #endregion Customized Methods
    }
}
