using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IFlowDetailTrackBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateFlowDetailTrack(FlowDetailTrack entity);

        FlowDetailTrack LoadFlowDetailTrack(Int32 id);

        IList<FlowDetailTrack> GetAllFlowDetailTrack();
    
        void UpdateFlowDetailTrack(FlowDetailTrack entity);

        void DeleteFlowDetailTrack(Int32 id);
    
        void DeleteFlowDetailTrack(FlowDetailTrack entity);
    
        void DeleteFlowDetailTrack(IList<Int32> pkList);
    
        void DeleteFlowDetailTrack(IList<FlowDetailTrack> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
