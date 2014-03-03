using System.Collections.Generic;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Distribution
{
    public interface ISalesPriceListMgr : ISalesPriceListBaseMgr
    {
        #region Customized Methods

        IList<SalesPriceList> GetAllSalesPriceList(string addressCode);

        IList<SalesPriceList> GetAllSalesPriceList(BillAddress address);

        SalesPriceList LoadSalesPriceList(string code, bool includeDetail);

        #endregion Customized Methods
    }
}
