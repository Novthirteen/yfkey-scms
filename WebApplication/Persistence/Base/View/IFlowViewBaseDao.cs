using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface IFlowViewBaseDao
    {
        #region Method Created By CodeSmith

        void CreateFlowView(FlowView entity);

        FlowView LoadFlowView(Int32 id);
  
        IList<FlowView> GetAllFlowView();
  
        void UpdateFlowView(FlowView entity);
        
        void DeleteFlowView(Int32 id);
    
        void DeleteFlowView(FlowView entity);
    
        void DeleteFlowView(IList<Int32> pkList);
    
        void DeleteFlowView(IList<FlowView> entityList);    
        #endregion Method Created By CodeSmith
    }
}
