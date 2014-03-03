using System;
using com.Sconit.Entity.Dss;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Dss
{
    public interface IDssOutboundControlMgr : IDssOutboundControlBaseMgr
    {
        #region Customized Methods

        IList<DssOutboundControl> GetDssOutboundControl();

        #endregion Customized Methods
    }
}
