using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IItemReferenceBaseDao
    {
        #region Method Created By CodeSmith

        void CreateItemReference(ItemReference entity);

        ItemReference LoadItemReference(Int32 id);
  
        IList<ItemReference> GetAllItemReference();
  
        IList<ItemReference> GetAllItemReference(bool includeInactive);
  
        void UpdateItemReference(ItemReference entity);
        
        void DeleteItemReference(Int32 id);
    
        void DeleteItemReference(ItemReference entity);
    
        void DeleteItemReference(IList<Int32> pkList);
    
        void DeleteItemReference(IList<ItemReference> entityList);    
        
        ItemReference LoadItemReference(com.Sconit.Entity.MasterData.Item item, com.Sconit.Entity.MasterData.Party party, String referenceCode);
    
        void DeleteItemReference(String itemCode, String partyCode, String referenceCode);
        
        ItemReference LoadItemReference(String itemCode, String partyCode, String referenceCode);
        #endregion Method Created By CodeSmith
    }
}
