using System;
using System.Collections;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface ISpecialTimeMgr : ISpecialTimeBaseMgr
    {
        #region Customized Methods

        IList GetReferSpecialTimeWizard(DateTime starttime, DateTime endtime, string region, string workcenter);

        IList GetSpecialTime(DateTime starttime, DateTime endtime, string region, string workcenter);

        #endregion Customized Methods
    }
}
