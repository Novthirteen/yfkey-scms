using System;
using com.Sconit.Entity.Dss;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Dss
{
    public interface IDssFtpControlMgr : IDssFtpControlBaseMgr
    {
        #region Customized Methods

        IList<DssFtpControl> GetDssFtpControl(string IOType);

        #endregion Customized Methods
    }
}
