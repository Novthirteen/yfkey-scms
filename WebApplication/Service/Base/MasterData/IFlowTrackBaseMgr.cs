using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IFlowTrackBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateFlowTrack(FlowTrack entity);

        FlowTrack LoadFlowTrack(Int32 id);

        IList<FlowTrack> GetAllFlowTrack();
    
        IList<FlowTrack> GetAllFlowTrack(bool includeInactive);
      
        void UpdateFlowTrack(FlowTrack entity);

        void DeleteFlowTrack(Int32 id);
    
        void DeleteFlowTrack(FlowTrack entity);
    
        void DeleteFlowTrack(IList<Int32> pkList);
    
        void DeleteFlowTrack(IList<FlowTrack> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
