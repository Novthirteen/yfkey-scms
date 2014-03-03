using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IItemBaseDao
    {
        #region Method Created By CodeSmith

        void CreateItem(Item entity);

        Item LoadItem(String code);
  
        IList<Item> GetAllItem();
  
        IList<Item> GetAllItem(bool includeInactive);
  
        void UpdateItem(Item entity);
        
        void DeleteItem(String code);
    
        void DeleteItem(Item entity);
    
        void DeleteItem(IList<String> pkList);
    
        void DeleteItem(IList<Item> entityList);    
        #endregion Method Created By CodeSmith
    }
}
