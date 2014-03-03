using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IStorageAreaBaseDao
    {
        #region Method Created By CodeSmith

        void CreateStorageArea(StorageArea entity);

        StorageArea LoadStorageArea(String code);
  
        IList<StorageArea> GetAllStorageArea();
  
        void UpdateStorageArea(StorageArea entity);
        
        void DeleteStorageArea(String code);
    
        void DeleteStorageArea(StorageArea entity);
    
        void DeleteStorageArea(IList<String> pkList);
    
        void DeleteStorageArea(IList<StorageArea> entityList);    
        #endregion Method Created By CodeSmith
    }
}
