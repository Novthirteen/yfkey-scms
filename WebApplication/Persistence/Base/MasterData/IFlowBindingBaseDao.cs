using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IFlowBindingBaseDao
    {
        #region Method Created By CodeSmith

        void CreateFlowBinding(FlowBinding entity);

        FlowBinding LoadFlowBinding(Int32 id);
  
        IList<FlowBinding> GetAllFlowBinding();
  
        void UpdateFlowBinding(FlowBinding entity);
        
        void DeleteFlowBinding(Int32 id);
    
        void DeleteFlowBinding(FlowBinding entity);
    
        void DeleteFlowBinding(IList<Int32> pkList);
    
        void DeleteFlowBinding(IList<FlowBinding> entityList);    
        #endregion Method Created By CodeSmith
    }
}
