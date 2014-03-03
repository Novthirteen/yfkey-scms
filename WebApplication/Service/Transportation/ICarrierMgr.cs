using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Transportation;

namespace com.Sconit.Service.Transportation
{
    public interface ICarrierMgr : ICarrierBaseMgr
    {
        #region Customized Methods

        void CreateCarrier(Carrier entity, User currentUser);

        IList<Carrier> GetCarrier(string userCode);

        IList<Carrier> GetCarrier(string userCode, bool includeInactive);

        #endregion Customized Methods
    }
}