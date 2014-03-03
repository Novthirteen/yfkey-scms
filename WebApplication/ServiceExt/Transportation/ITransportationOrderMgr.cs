using System;
using System.Collections.Generic;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Transportation;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Transportation
{
    public interface ITransportationOrderMgr : ITransportationOrderBaseMgr
    {
        #region Customized Methods

        TransportationOrder CreateTransportationOrder(string route, IList<InProcessLocation> ipList, User user);

        TransportationOrder CreateTransportationOrder(string route, IList<InProcessLocation> ipList, string userCode);

        TransportationOrder LoadTransportationOrder(String orderNo, bool includeDetail);

        TransportationOrder CreateTransportationOrder(string expenseCode, string userCode);

        TransportationOrder CreateTransportationOrder(Expense expense, string userCode);

        TransportationOrder CreateTransportationOrder(string expenseCode, User user);

        TransportationOrder CreateTransportationOrder(Expense expense, User user);

        void StartTransportationOrder(string orderNo, string userCode);

        void StartTransportationOrder(string orderNo, User user);

        void StartTransportationOrder(TransportationOrder order, string userCode);

        void StartTransportationOrder(TransportationOrder order, User user);

        void CompleteTransportationOrder(string orderNo, string userCode);

        void CompleteTransportationOrder(string orderNo, User user);

        void CompleteTransportationOrder(TransportationOrder order, string userCode);

        void CompleteTransportationOrder(TransportationOrder order, User user);


        void CancelTransportationOrder(string orderNo, string userCode);

        void CancelTransportationOrder(string orderNo, User user);

        void CancelTransportationOrder(TransportationOrder order, string userCode);

        void CancelTransportationOrder(TransportationOrder order, User user);

        void CloseTransportationOrder(string orderNo, string userCode);

        void CloseTransportationOrder(string orderNo, User user);

        void CloseTransportationOrder(TransportationOrder order, string userCode);

        void CloseTransportationOrder(TransportationOrder order, User user);

        void TryCloseTransportationOrder();

        IList<TransportationOrder> GetTransportationOrder(string status, bool isValuated);

        void ValuateTransportationOrder(string orderNo, string userCode);

        void ValuateTransportationOrder(string orderNo, User user);

        void ValuateTransportationOrder(TransportationOrder order, string userCode);

        void ValuateTransportationOrder(TransportationOrder order, User user);

        void ValuateTransportationOrder(IList<TransportationOrder> transportationOrderList, User user);

        void TryCompleteTransportationOrder(InProcessLocation inProcessLocation, User user);

        #endregion Customized Methods
    }
}
