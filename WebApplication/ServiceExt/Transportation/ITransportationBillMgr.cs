using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Transportation;

namespace com.Sconit.Service.Transportation
{
    public interface ITransportationBillMgr : ITransportationBillBaseMgr
    {
        #region Customized Methods

        TransportationBill CheckAndLoadTransportationBill(string billNo);

        TransportationBill CheckAndLoadTransportationBill(string billNo, bool includeBillDetail);

        IList<TransportationBill> CreateTransportationBill(IList<TransportationActBill> transportationActBillList, User user);

        IList<TransportationBill> CreateTransportationBill(IList<TransportationActBill> transportationActBillList, string userCode);

        IList<TransportationBill> CreateTransportationBill(IList<TransportationActBill> transportationActBillList, User user, string status);

        IList<TransportationBill> CreateTransportationBill(IList<TransportationActBill> transportationActBillList, string userCode, string status);

        IList<TransportationBill> CreateTransportationBill(IList<TransportationActBill> transportationActBillList, User user, string status, decimal discount);

        IList<TransportationBill> CreateTransportationBill(IList<TransportationActBill> transportationActBillList, string userCode, string status, decimal discount);

        void DeleteTransportationBill(string billNo, string userCode);

        void DeleteTransportationBill(string billNo, User user);

        void DeleteTransportationBill(TransportationBill transportationBill, string userCode);

        void DeleteTransportationBill(TransportationBill transportationBill, User user);

        TransportationBill LoadTransportationBill(string billNo, bool includeBillDetail);

        void AddTransportationBillDetail(string billNo, IList<TransportationActBill> transportationActBillList, string userCode);

        void AddTransportationBillDetail(string billNo, IList<TransportationActBill> transportationActBillList, User user);

        void AddTransportationBillDetail(TransportationBill transportationBill, IList<TransportationActBill> transportationActBillList, string userCode);

        void AddTransportationBillDetail(TransportationBill transportationBill, IList<TransportationActBill> transportationActBillList, User user);

        void UpdateTransportationBill(TransportationBill transportationBill, string userCode);

        void UpdateTransportationBill(TransportationBill transportationBill, User user);

        void ReleaseTransportationBill(string billNo, string userCode);

        void ReleaseTransportationBill(string billNo, User user);

        void ReleaseTransportationBill(TransportationBill transportationBill, string userCode);

        void ReleaseTransportationBill(TransportationBill transportationBill, User user);

        void CancelTransportationBill(string billNo, string userCode);

        void CancelTransportationBill(string billNo, User user);

        void CancelTransportationBill(TransportationBill transportationBill, string userCode);

        void CancelTransportationBill(TransportationBill transportationBill, User user);

        void CloseTransportationBill(string billNo, string userCode);

        void CloseTransportationBill(string billNo, User user);

        void CloseTransportationBill(TransportationBill transportationBill, string userCode);

        void CloseTransportationBill(TransportationBill transportationBill, User user);

        TransportationBill VoidTransportationBill(string billNo, string userCode);

        TransportationBill VoidTransportationBill(string billNo, User user);

        TransportationBill VoidTransportationBill(TransportationBill transportationBill, string userCode);

        TransportationBill VoidTransportationBill(TransportationBill transportationBill, User user);

        void DeleteTransportationBillDetail(IList<TransportationBillDetail> transportationBillDetailList, string userCode);

        void DeleteTransportationBillDetail(IList<TransportationBillDetail> transportationBillDetailList, User user);

        #endregion Customized Methods
    }
}
