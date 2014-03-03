using System;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IFlowDetailTrackMgr : IFlowDetailTrackBaseMgr
    {
        #region Customized Methods

        void CreateFlowDetailTrack(FlowDetail flowDetail, string eventCode);

        #endregion Customized Methods
    }
}
