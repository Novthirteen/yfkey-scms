using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IClientWorkingHoursMgr : IClientWorkingHoursBaseMgr
    {
        #region Customized Methods

        IList<ClientWorkingHours> GetAllClientWorkingHours(string OrderHeadId);

        #endregion Customized Methods
    }
}
