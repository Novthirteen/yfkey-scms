using System;
using System.Collections.Generic;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;


namespace com.Sconit.Service.MasterData
{
    public interface IOrderMgr
    {
        OrderHead LoadOrder(string orderNo, string userCode);

        OrderHead LoadOrder(string orderNo, User user);

        void UpdateOrder(OrderHead orderHead, string userCode);

        void UpdateOrder(OrderHead orderHead, User user);

        void UpdateOrder(OrderHead orderHead, string userCode, bool updateDetail);

        void UpdateOrder(OrderHead orderHead, User user, bool updateDetail);

        OrderHead TransferFlow2Order(string flowCode);

        OrderHead TransferFlow2Order(string flowCode, string orderSubType);

        OrderHead TransferFlow2Order(Flow flow);

        OrderHead TransferFlow2Order(Flow flow, string orderSubType);

        OrderHead TransferFlow2Order(string flowCode, bool isGenerateOrderSubsidiary);

        OrderHead TransferFlow2Order(string flowCode, string orderSubType, bool isGenerateOrderSubsidiary);

        OrderHead TransferFlow2Order(Flow flow, bool isGenerateOrderSubsidiary);

        OrderHead TransferFlow2Order(Flow flow, string orderSubType, bool isGenerateOrderSubsidiary);

        OrderHead TransferFlow2Order(string flowCode, bool isGenerateOrderSubsidiary, DateTime startTime);

        OrderHead TransferFlow2Order(string flowCode, string orderSubType, bool isGenerateOrderSubsidiary, DateTime startTime);

        OrderHead TransferFlow2Order(Flow flow, bool isGenerateOrderSubsidiary, DateTime startTime);

        OrderHead TransferFlow2Order(Flow flow, string orderSubType, bool isGenerateOrderSubsidiary, DateTime startTime);

        void CreateOrder(IList<OrderHead> orderHeadList, string userCode);

        void CreateOrder(OrderHead orderHead, string userCode);

        void CreateOrder(OrderHead orderHead, User user);

        void AddOrderDetail(OrderDetail orderDetail, string userCode);

        void AddOrderDetail(OrderDetail orderDetail, User user);

        void AddOrderLocationTransaction(OrderLocationTransaction orderLocationTransaction, string userCode);

        void AddOrderLocationTransaction(OrderLocationTransaction orderLocationTransaction, User user);

        void AddOrderLocationTransaction(IList<OrderLocationTransaction> orderLocTransList, string userCode);

        void AddOrderLocationTransaction(IList<OrderLocationTransaction> orderLocTransList, User user);

        void UpdateOrderDetail(OrderDetail orderDetail, string userCode);

        void UpdateOrderDetail(OrderDetail orderDetail, User user);

        void UpdateOrderQty(IList<OrderDetail> orderDetailList, string userCode);

        void UpdateOrderQty(IList<OrderDetail> orderDetailList, User user);

        void DeleteOrderDetail(OrderDetail orderDetail, string userCode);

        void DeleteOrderDetail(OrderDetail orderDetail, User user);

        void DeleteOrderDetail(int orderDetailId, string userCode);

        void DeleteOrderDetail(int orderDetailId, User user);

        void DeleteOrderLocationTransaction(OrderLocationTransaction orderLocationTransaction, string userCode);

        void DeleteOrderLocationTransaction(OrderLocationTransaction orderLocationTransaction, User user);

        void DeleteOrderLocationTransaction(int orderLocationTransactionId, string userCode);

        void DeleteOrderLocationTransaction(int orderLocationTransactionId, User user);

        void DeleteOrder(OrderHead orderHead, string userCode);

        void DeleteOrder(OrderHead orderHead, User user);

        void DeleteOrder(string orderNo, string userCode);

        void DeleteOrder(string orderNo, User user);

        void ReleaseOrder(OrderHead orderHead, string userCode);

        void ReleaseOrder(OrderHead orderHead, User user);

        void ReleaseOrder(string orderNo, string userCode);

        void ReleaseOrder(string orderNo, User user);

        void ReleaseOrder(OrderHead orderHead, string userCode, bool autoHandleAbstractItem);

        void ReleaseOrder(OrderHead orderHead, User user, bool autoHandleAbstractItem);

        void ReleaseOrder(string orderNo, string userCode, bool autoHandleAbstractItem);

        void ReleaseOrder(string orderNo, User user, bool autoHandleAbstractItem);

        void StartOrder(OrderHead orderHead, string userCode);

        void StartOrder(string orderNo, string userCode);

        void StartOrder(OrderHead orderHead, User user);

        void StartOrder(string orderNo, User user);

        void CancelOrder(OrderHead orderHead, string userCode);

        void CancelOrder(OrderHead orderHead, User user);

        void CancelOrder(string orderNo, string userCode);

        void CancelOrder(string orderNo, User user);

        void TryCloseOrder();

        void TryCloseOrder(OrderHead orderHead, string userCode);

        void TryCloseOrder(OrderHead orderHead, User user);

        void TryCloseOrder(string orderNo, string userCode);

        void TryCloseOrder(string orderNo, User user);

        InProcessLocation ShipOrder(IList<OrderDetail> orderDetailList, string userCode);

        InProcessLocation ShipOrder(IList<OrderDetail> orderDetailList, User user);

        InProcessLocation ShipOrder(IList<InProcessLocationDetail> inProcessLocationDetailList, string userCode);

        InProcessLocation ShipOrder(IList<InProcessLocationDetail> inProcessLocationDetailList, User user);

        InProcessLocation ShipOrder(InProcessLocation inProcessLocation, string userCode);

        InProcessLocation ShipOrder(InProcessLocation inProcessLocation, User user);

        InProcessLocation ShipOrder(string pickListNo, string userCode);

        InProcessLocation ShipOrder(string pickListNo, User user);

        InProcessLocation ShipOrder(PickList pickList, string userCode);

        InProcessLocation ShipOrder(PickList pickList, User user);

        Receipt ReceiveOrder(IList<OrderDetail> orderDetailList, string userCode);

        Receipt ReceiveOrder(IList<OrderDetail> orderDetailList, User user);

        Receipt ReceiveOrder(IList<OrderDetail> orderDetailList, string userCode, bool isOddCreateHu);

        Receipt ReceiveOrder(IList<OrderDetail> orderDetailList, User user, bool isOddCreateHu);

        Receipt ReceiveOrder(IList<ReceiptDetail> receiptDetailList, string userCode);

        Receipt ReceiveOrder(IList<ReceiptDetail> receiptDetailList, string userCode, InProcessLocation inProcessLocation);

        Receipt ReceiveOrder(IList<ReceiptDetail> receiptDetailList, string userCode, InProcessLocation inProcessLocation, string externalReceiptNo);

        Receipt ReceiveOrder(IList<ReceiptDetail> receiptDetailList, string userCode, InProcessLocation inProcessLocation, string externalReceiptNo, IList<WorkingHours> workingHoursList);

        Receipt ReceiveOrder(IList<ReceiptDetail> receiptDetailList, string userCode, InProcessLocation inProcessLocation, string externalReceiptNo, IList<WorkingHours> workingHoursList, bool createIp);

        Receipt ReceiveOrder(IList<ReceiptDetail> receiptDetailList, string userCode, InProcessLocation inProcessLocation, string externalReceiptNo, IList<WorkingHours> workingHoursList, bool createIp, bool isOddCreateHu);

        Receipt ReceiveOrder(Receipt receipt, string userCode);

        Receipt ReceiveOrder(Receipt receipt, string userCode, IList<WorkingHours> workingHoursList);

        Receipt ReceiveOrder(Receipt receipt, string userCode, IList<WorkingHours> workingHoursList, bool createIp);

        Receipt ReceiveOrder(Receipt receipt, string userCode, IList<WorkingHours> workingHoursList, bool createIp, bool isOddCreateHu);

        Receipt ReceiveOrder(IList<ReceiptDetail> receiptDetailList, User user);

        Receipt ReceiveOrder(IList<ReceiptDetail> receiptDetailList, User user, InProcessLocation inProcessLocation);

        Receipt ReceiveOrder(IList<ReceiptDetail> receiptDetailList, User user, InProcessLocation inProcessLocation, string externalReceiptNo);

        Receipt ReceiveOrder(IList<ReceiptDetail> receiptDetailList, User user, InProcessLocation inProcessLocation, string externalReceiptNo, IList<WorkingHours> workingHoursList);

        Receipt ReceiveOrder(IList<ReceiptDetail> receiptDetailList, User user, InProcessLocation inProcessLocation, string externalReceiptNo, IList<WorkingHours> workingHoursList, bool createIp);

        Receipt ReceiveOrder(IList<ReceiptDetail> receiptDetailList, User user, InProcessLocation inProcessLocation, string externalReceiptNo, IList<WorkingHours> workingHoursList, bool createIp, bool isOddCreateHu);

        Receipt ReceiveOrder(Receipt receipt, User user);

        Receipt ReceiveOrder(Receipt receipt, User user, IList<WorkingHours> workingHoursList);

        Receipt ReceiveOrder(Receipt receipt, User user, IList<WorkingHours> workingHoursList, bool createIp);

        Receipt ReceiveOrder(Receipt receipt, User user, IList<WorkingHours> workingHoursList, bool createIp, bool isOddCreateHu);

        Receipt QuickReceiveOrder(string flowCode, IList<OrderDetail> orderDetailList, string userCode);

        Receipt QuickReceiveOrder(string flowCode, IList<OrderDetail> orderDetailList, string userCode, string orderSubType, DateTime winTime, DateTime startTime, bool isUrgent, string referenceOrderNo, string externalOrderNo);

        Receipt QuickReceiveOrder(Flow flow, IList<OrderDetail> orderDetailList, User user);

        Receipt QuickReceiveOrder(Flow flow, IList<OrderDetail> orderDetailList, User user, string orderSubType, DateTime winTime, DateTime startTime, bool isUrgent, string referenceOrderNo, string externalOrderNo);

        void ManualCompleteOrder(string orderNo, string userCode);

        void ManualCompleteOrder(string orderNo, User user);

        void ManualCompleteOrder(OrderHead orderHead, string userCode);

        void ManualCompleteOrder(OrderHead orderHead, User user);

        void TryCompleteOrder(OrderHead orderHead, User user);

        void TryCompleteOrder(IList<OrderHead> orderHeadList, User user);

        InProcessLocation ConvertOrderLocTransToInProcessLocation(IList<OrderLocationTransaction> orderLocTransList);

        InProcessLocation ConvertOrderToInProcessLocation(string orderNo);

        InProcessLocation ConvertPickListToInProcessLocation(string pickListNo);

        Receipt ConvertOrderDetailToReceipt(IList<OrderDetail> orderDetailList);

        Receipt ConvertInProcessLocationToReceipt(InProcessLocation inProcessLocation);

        Receipt ConvertInProcessLocationToReceipt(InProcessLocation inProcessLocation, IDictionary<string, string> huIdStorageBinDic);

        Receipt ConvertInProcessLocationToReceipt(InProcessLocation inProcessLocation, IDictionary<string, string> huIdStorageBinDic, string externalOrderNo);

        IList<InProcessLocationDetail> ConvertTransformerToInProcessLocationDetail(List<Transformer> transformerList);

        IList<InProcessLocationDetail> ConvertTransformerToInProcessLocationDetail(List<Transformer> transformerList, bool includeZero);

        IList<ReceiptDetail> ConvertTransformerToReceiptDetail(List<Transformer> transformerList);

        IList<ReceiptDetail> ConvertTransformerToReceiptDetail(List<Transformer> transformerList, bool includeZero);

        IList<OrderHead> ConvertShiftPlanScheduleToOrders(IList<ShiftPlanSchedule> shiftPlanScheduleList);

        IList<OrderHead> ConvertShiftPlanScheduleToOrders(IList<ShiftPlanSchedule> shiftPlanScheduleList, decimal leadTime);

        IList<OrderHead> ConvertFlowPlanToOrders(IList<FlowPlan> flowPlanList);

        IList<OrderHead> ConvertOrderDetailToOrders(IList<OrderDetail> orderDetailList);

        Receipt ReceiveScrapOrder(string orderNo, string userCode);

        Receipt ReceiveScrapOrder(OrderHead orderHead, string userCode);

        Receipt ReceiveScrapOrder(string orderNo, User user);

        Receipt ReceiveScrapOrder(OrderHead orderHead, User user);

        Receipt ReleaseScrapOrder(OrderHead orderHead, string userCode);

        Receipt ReleaseScrapOrder(string orderNo, string userCode);

        Receipt ReleaseScrapOrder(string orderNo, User user);

        Receipt ReleaseScrapOrder(OrderHead orderHead, User user);

        //原材料回用
        Receipt ReleaseReuseOrder(OrderHead orderHead, string userCode, IList<Hu> huList);

        Receipt ReleaseReuseOrder(string orderNo, string userCode, IList<Hu> huList);

        Receipt ReleaseReuseOrder(OrderHead orderHead, User currentUser, IList<Hu> huList);

        Receipt ReleaseReuseOrder(string orderNo, User currentUser, IList<Hu> huList);

        Receipt ReceiveReuseOrder(OrderHead orderHead, User user, IList<Hu> huList);

        void TryCompleteOrder(string[] flowCodeArray);

        void CreateOrder(string flowCode, User user, IList<Hu> huList);

        void CreateOrder(string flowCode, string userCode, IList<Hu> huList);

        void CreateOrder(Flow flow, User user, IList<Hu> huList);

        void CreateOrder(Flow flow, string userCode, IList<Hu> huList);

        void DoReceiveWO(string huId, OrderDetail orderDetail, decimal qty);

    }
}
