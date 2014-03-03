using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IFlowDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateFlowDetail(FlowDetail entity);

        FlowDetail LoadFlowDetail(Int32 id);
  
        IList<FlowDetail> GetAllFlowDetail();
  
        void UpdateFlowDetail(FlowDetail entity);
        
        void DeleteFlowDetail(Int32 id);
    
        void DeleteFlowDetail(FlowDetail entity);
    
        void DeleteFlowDetail(IList<Int32> pkList);
    
        void DeleteFlowDetail(IList<FlowDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
