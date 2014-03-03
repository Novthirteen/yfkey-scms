using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Transportation;

namespace com.Sconit.Service.Transportation
{
    public interface ITransportationActBillMgr : ITransportationActBillBaseMgr
    {
        #region Customized Methods

        void ReverseUpdateTransportationActBill(TransportationBillDetail oldBillDetail, TransportationBillDetail newBillDetail, User user);

        TransportationActBill CreateTransportationActBill(TransportationOrder order, User user);

        IList<TransportationActBill> GetTransportationActBill(string partyCode, string expenseNo, DateTime? effDateFrom, DateTime? effDateTo, string itemCode, string currency, string exceptBillNo);

        IList<TransportationActBill> GetTransportationActBill(string partyCode, string expenseNo, DateTime? effDateFrom, DateTime? effDateTo, string itemCode, string currency, string exceptBillNo, bool? isProvisionalEstimate);

        void RecalculatePrice(IList<TransportationActBill> transportationActBillList, User user);

        void RecalculatePrice(IList<TransportationActBill> transportationActBillList, User user, DateTime? efftiveDate);

        IList<TransportationActBill> GetTransportationActBill(string orderNo);

        IList<TransportationActBill> GetTransportationActBill(TransportationOrder order);

        void CalculateFixPriceList();

        void CreateTransportationActBill(TransportPriceListDetail priceListDetail);

        TransportationActBill CreateTransportationItemActBill(Receipt receipt, string billingMethod);

        #endregion Customized Methods
    }
}
