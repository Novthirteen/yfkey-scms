using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IItemKitBaseDao
    {
        #region Method Created By CodeSmith

        void CreateItemKit(ItemKit entity);

        ItemKit LoadItemKit(com.Sconit.Entity.MasterData.Item parentItem, com.Sconit.Entity.MasterData.Item childItem);
        ItemKit LoadItemKit(String parentItemCode, String childItemCode);
  
        IList<ItemKit> GetAllItemKit();
  
        IList<ItemKit> GetAllItemKit(bool includeInactive);
  
        void UpdateItemKit(ItemKit entity);
        
        void DeleteItemKit(com.Sconit.Entity.MasterData.Item parentItem, com.Sconit.Entity.MasterData.Item childItem);
    
        void DeleteItemKit(String parentItemCode, String childItemCode);
    
        void DeleteItemKit(ItemKit entity);
    
        void DeleteItemKit(IList<ItemKit> entityList);    
        #endregion Method Created By CodeSmith
    }
}
