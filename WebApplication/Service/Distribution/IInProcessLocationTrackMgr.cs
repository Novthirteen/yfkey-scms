using System;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Distribution;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Distribution
{
    public interface IInProcessLocationTrackMgr : IInProcessLocationTrackBaseMgr
    {
        #region Customized Methods

        IList<InProcessLocationTrack> CreateIInProcessLocationTrack(InProcessLocation inProcessLocation, Routing routing);

        IList<InProcessLocationTrack> GetInProcessLocationTrack(string ipNo, int op);

        #endregion Customized Methods
    }
}
