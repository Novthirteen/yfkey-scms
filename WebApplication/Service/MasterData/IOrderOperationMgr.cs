using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IOrderOperationMgr : IOrderOperationBaseMgr
    {
        #region Customized Methods

        IList<OrderOperation> GenerateOrderOperation(OrderHead orderHead);

        OrderOperation GenerateOrderOperation(OrderHead orderHead, RoutingDetail routingDetail);

        IList<OrderOperation> GetOrderOperation(string orderNo);

        IList<OrderOperation> GetOrderOperation(OrderHead orderHead);

        void TryAddOrderOperation(OrderHead orderHead, int operation, string reference);

        void TryDeleteOrderOperation(OrderHead orderHead, int operation);

        void TryDeleteOrderOperation(OrderHead orderHead, IList<int> operationList);


        #endregion Customized Methods
    }
}
