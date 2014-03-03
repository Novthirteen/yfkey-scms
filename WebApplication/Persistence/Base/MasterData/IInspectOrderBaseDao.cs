using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IInspectOrderBaseDao
    {
        #region Method Created By CodeSmith

        void CreateInspectOrder(InspectOrder entity);

        InspectOrder LoadInspectOrder(String inspectNo);
  
        IList<InspectOrder> GetAllInspectOrder();
  
        void UpdateInspectOrder(InspectOrder entity);
        
        void DeleteInspectOrder(String inspectNo);
    
        void DeleteInspectOrder(InspectOrder entity);
    
        void DeleteInspectOrder(IList<String> pkList);
    
        void DeleteInspectOrder(IList<InspectOrder> entityList);    
        #endregion Method Created By CodeSmith
    }
}
