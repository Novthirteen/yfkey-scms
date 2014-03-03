using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class RollingForecastMgr : RollingForecastBaseMgr, IRollingForecastMgr
    {
        public RollingForecastMgr(IRollingForecastDao entityDao)
            : base(entityDao)
        {
        }

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public void SaveRollingForecast(IList<RollingForecast> rollingForecastList)
        {
            if (rollingForecastList != null && rollingForecastList.Count > 0)
            {
                foreach (RollingForecast rollingForecast in rollingForecastList)
                {

                    this.Create(rollingForecast);
                }
            }

        }

        #endregion Customized Methods
    }
}