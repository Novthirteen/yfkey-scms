using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Transportation;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class VehicleMgr : VehicleBaseMgr, IVehicleMgr
    {
        #region Customized Methods

        //TODO: Add other methods here.

        public IList<ProjectMstr> GetAllProject()
        {

         return   base.FindAll<ProjectMstr>();
        }
      
        #endregion Customized Methods
    }
}