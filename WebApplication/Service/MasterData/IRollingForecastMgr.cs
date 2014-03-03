using System;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IRollingForecastMgr : IRollingForecastBaseMgr
    {
        #region Customized Methods

        void SaveRollingForecast(IList<RollingForecast> rollingForecastList);

        #endregion Customized Methods
    }
}
