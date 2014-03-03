using System;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IStorageBinMgr : IStorageBinBaseMgr
    {
        #region Customized Methods

        IList<StorageBin> GetStorageBin(string AreaCode);
        IList<StorageBin> GetStorageBin(StorageArea Area);
        IList<StorageBin> GetStorageBin(Location location);
        IList<StorageBin> GetStorageBinByLocation(string location);

        StorageBin CheckAndLoadStorageBin(string binCode);

        bool CheckExistAndPermission(string binCode, string userCode);

        #endregion Customized Methods
    }
}
