using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement
{
    public interface IPurchasePriceListMgr : IPurchasePriceListBaseMgr
    {
        #region Customized Methods

        IList<PurchasePriceList> GetAllPurchasePriceList(string addressCode);

        IList<PurchasePriceList> GetAllPurchasePriceList(BillAddress address);

        PurchasePriceList LoadPurchasePriceList(string code, bool includeDetail);

        #endregion Customized Methods
    }
}
