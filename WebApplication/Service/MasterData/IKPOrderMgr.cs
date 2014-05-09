using System;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IKPOrderMgr : IKPOrderBaseMgr
    {
        #region Customized Methods

        KPOrder LoadKPOrder(Decimal orderId, bool includeDetail);

        void ImportKPOrder();

        #endregion Customized Methods
    }
}
