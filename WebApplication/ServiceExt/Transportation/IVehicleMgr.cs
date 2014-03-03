using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Service.Transportation
{
    public interface IVehicleMgr : IVehicleBaseMgr
    {
        #region Customized Methods

        //TODO: Add other methods here.
        IList<ProjectMstr> GetAllProject();
        #endregion Customized Methods
    }
}