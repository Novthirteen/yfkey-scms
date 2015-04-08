using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Transportation;

namespace com.Sconit.Service.Transportation
{
    public interface ITransportPriceListDetailMgr : ITransportPriceListDetailBaseMgr
    {
        #region Customized Methods

        IList<TransportPriceListDetail> GetAllTransportPriceListDetail(string transportPriceListCode);

        TransportPriceListDetail GetLastestTransportPriceListDetail(TransportPriceList priceList, Item item, DateTime effectiveDate, Currency currency, Uom uom, string priceListType, string billingMethod);

        TransportPriceListDetail GetLastestTransportPriceListDetail(TransportPriceList priceList, DateTime effectiveDate, Currency currency, string pricingMethod, TransportationAddress shipFrom, TransportationAddress shipTo, string priceListType, string vehicleType, string transportMethod);

        TransportPriceListDetail GetLastestTransportPriceListDetail(TransportPriceList priceList, Item item, DateTime effectiveDate, Currency currency, Uom uom, string pricingMethod, TransportationAddress shipFrom, TransportationAddress shipTo, string priceListType, string billingMethod, string vehicleType, string transportMethod);

        TransportPriceListDetail GetLastestLadderStereTransportPriceListDetail(TransportPriceList priceList, Item item, DateTime effectiveDate, Currency currency, Uom uom, string pricingMethod, TransportationAddress shipFrom, TransportationAddress shipTo, string priceListType, string billingMethod, string vehicleType,decimal qty);

        IList<TransportPriceListDetail> CheckOperation(string transportPriceListCode, string startDate, string endDate, string item, string billingMethod);
        #endregion Customized Methods
    }
}