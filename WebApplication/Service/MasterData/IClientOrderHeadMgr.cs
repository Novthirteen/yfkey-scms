using System;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IClientOrderHeadMgr : IClientOrderHeadBaseMgr
    {
        #region Customized Methods

        //TODO: Add other methods here.
        void CreateClientOrderHead(ClientOrderHead clientOrderHead, IList<ClientOrderDetail> clientOrderDetailList, IList<ClientWorkingHours> clientWorkingHoursList);

        #endregion Customized Methods
    }
}
