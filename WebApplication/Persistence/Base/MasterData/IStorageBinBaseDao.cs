using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IStorageBinBaseDao
    {
        #region Method Created By CodeSmith

        void CreateStorageBin(StorageBin entity);

        StorageBin LoadStorageBin(String code);
  
        IList<StorageBin> GetAllStorageBin();
  
        void UpdateStorageBin(StorageBin entity);
        
        void DeleteStorageBin(String code);
    
        void DeleteStorageBin(StorageBin entity);
    
        void DeleteStorageBin(IList<String> pkList);
    
        void DeleteStorageBin(IList<StorageBin> entityList);    
        #endregion Method Created By CodeSmith
    }
}
