using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IFlowBaseDao
    {
        #region Method Created By CodeSmith

        void CreateFlow(Flow entity);

        Flow LoadFlow(String code);
  
        IList<Flow> GetAllFlow();
  
        IList<Flow> GetAllFlow(bool includeInactive);
  
        void UpdateFlow(Flow entity);
        
        void DeleteFlow(String code);
    
        void DeleteFlow(Flow entity);
    
        void DeleteFlow(IList<String> pkList);
    
        void DeleteFlow(IList<Flow> entityList);    
        #endregion Method Created By CodeSmith
    }
}
