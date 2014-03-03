using System;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IFlowTrackMgr : IFlowTrackBaseMgr
    {
        #region Customized Methods

        void CreateFlowTrack(Flow flow,string eventCode);

        #endregion Customized Methods
    }
}
