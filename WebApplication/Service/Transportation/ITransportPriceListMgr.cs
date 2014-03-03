using System;
using com.Sconit.Entity.Transportation;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Transportation
{
    public interface ITransportPriceListMgr : ITransportPriceListBaseMgr
    {
        #region Customized Methods

        IList<TransportPriceList> GetTransportPriceList(string partyCode);

        #endregion Customized Methods
    }
}