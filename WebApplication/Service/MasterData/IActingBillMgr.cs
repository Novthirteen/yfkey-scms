using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using System;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IActingBillMgr : IActingBillBaseMgr
    {
        #region Customized Methods

        void ReverseUpdateActingBill(BillDetail oldBillDetail, BillDetail newBillDetail, User user);

        void RecalculatePrice(IList<ActingBill> actingBillList, User user);

        void RecalculatePrice(IList<ActingBill> actingBillList, User user, DateTime? efftiveDate);

        IList<ActingBill> GetUnBilledActingBill(OrderHead orderHead);

        IList<ActingBill> GetUnBilledActingBill(string orderNo);

        IList<ActingBill> GetActingBill(string partyCode, string receiver, DateTime? effDateFrom, DateTime? effDateTo, string itemCode, string currency, string transType, string exceptBillNo);

        IList<ActingBill> GetActingBill(string partyCode, string receiver, DateTime? effDateFrom, DateTime? effDateTo, string itemCode, string currency, string transType, string exceptBillNo, bool? IsProvisionalEstimate);

        #endregion Customized Methods
    }
}
