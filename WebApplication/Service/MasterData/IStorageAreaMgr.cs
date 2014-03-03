using System;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IStorageAreaMgr : IStorageAreaBaseMgr
    {
        #region Customized Methods

        IList<StorageArea> GetStorageArea(Location location);
        IList<StorageArea> GetStorageArea(string locationCode);
        IList<StorageArea> GetStorageArea(string locationCode, string AreaCode);

        #endregion Customized Methods
    }
}
