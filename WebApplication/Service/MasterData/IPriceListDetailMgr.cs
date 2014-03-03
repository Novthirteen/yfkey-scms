using System;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IPriceListDetailMgr : IPriceListDetailBaseMgr
    {
        #region Customized Methods

        PriceListDetail GetLastestPriceListDetail(string priceListCode, string itemCode, DateTime effectiveDate, string currencyCode);

        PriceListDetail GetLastestPriceListDetail(PriceList priceList, Item item, DateTime effectiveDate, Currency currency);

        PriceListDetail GetLastestPriceListDetail(string priceListCode, string itemCode, DateTime effectiveDate, string currencyCode, string uomCode);

        PriceListDetail GetLastestPriceListDetail(PriceList priceList, Item item, DateTime effectiveDate, Currency currency, Uom uom);

        PriceListDetail LoadPriceListDetail(string priceListCode, string currencyCode, string itemCode, string uomCode, DateTime startDate);

        #endregion Customized Methods
    }
}
