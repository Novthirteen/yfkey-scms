using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IMiscOrderBaseDao
    {
        #region Method Created By CodeSmith

        void CreateMiscOrder(MiscOrder entity);

        MiscOrder LoadMiscOrder(String orderNo);
  
        IList<MiscOrder> GetAllMiscOrder();
  
        void UpdateMiscOrder(MiscOrder entity);
        
        void DeleteMiscOrder(String orderNo);
    
        void DeleteMiscOrder(MiscOrder entity);
    
        void DeleteMiscOrder(IList<String> pkList);
    
        void DeleteMiscOrder(IList<MiscOrder> entityList);    
        #endregion Method Created By CodeSmith
    }
}
