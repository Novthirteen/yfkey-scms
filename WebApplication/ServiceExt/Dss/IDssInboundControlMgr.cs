using System;
using com.Sconit.Entity.Dss;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Dss
{
    public interface IDssInboundControlMgr : IDssInboundControlBaseMgr
    {
        #region Customized Methods

        IList<DssInboundControl> GetDssInboundControl();

        #endregion Customized Methods
    }
}
