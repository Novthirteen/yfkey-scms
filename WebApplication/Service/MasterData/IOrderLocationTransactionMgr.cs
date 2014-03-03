using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;


//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IOrderLocationTransactionMgr : IOrderLocationTransactionBaseMgr
    {
        #region Customized Methods

        IList<OrderLocationTransaction> GetOrderLocationTransaction(string orderNo);

        IList<OrderLocationTransaction> GetOrderLocationTransaction(string orderNo, string ioType);

        IList<OrderLocationTransaction> GetOrderLocationTransaction(IList<string> orderNoList, string ioType);

        IList<OrderLocationTransaction> GetOrderLocationTransaction(OrderHead orderHead);

        IList<OrderLocationTransaction> GetOrderLocationTransaction(OrderHead orderHead, string ioType);

        IList<OrderLocationTransaction> GetOrderLocationTransaction(int orderDetailId);

        IList<OrderLocationTransaction> GetOrderLocationTransaction(OrderDetail orderDetail);

        IList<OrderLocationTransaction> GetOrderLocationTransaction(int orderDetailId, string ioType);

        IList<OrderLocationTransaction> GetOrderLocationTransaction(int orderDetailId, string ioType, string backFlushMethod);

        IList<OrderLocationTransaction> GetOrderLocationTransaction(OrderDetail orderDetail, string ioType);

        IList<OrderLocationTransaction> GetOrderLocationTransaction(OrderDetail orderDetail, string ioType, string backFlushMethod);

        //IList<OrderLocationTransaction> GetTobeBackFlushOrderLocationTransaction(string flowCode);

        OrderLocationTransaction GenerateOrderLocationTransaction(
            OrderDetail orderDetail, Item item, BomDetail bomDetail, Uom uom, int operation,
            string ioType, string transactionType, decimal unitQty, Location loc, bool isShipScanHu,
            int? huLotSize, bool needPrint, string backFlushMethod, string itemVersion, Location rejectLocation);

        void AutoReplaceAbstractItem(OrderLocationTransaction orderLocationTransaction);

        void ReplaceAbstractItem(OrderLocationTransaction orderLocationTransaction, BomDetail bomDetail);

        OrderLocationTransaction AddNewMaterial(OrderDetail orderDetail, BomDetail bomDetail, Location orgLocation, decimal orgOrderedQty);

        IList<OrderLocationTransaction> GetOpenOrderLocTransIn(string item, string location, string IOType, DateTime? winTime);

        IList<OrderLocationTransaction> GetOpenOrderLocTransOut(string item, string location, string IOType, DateTime? startTime);

        IList<OrderLocationTransaction> GetPairOrderLocTrans(int orderDetId, string item, string IOType);

        IList<OrderLocationTransaction> GetOpenOrderLocationTransaction(IList<string> itemList, IList<string> locList);

        OrderLocationTransaction GetOrderLocationTransaction(string orderNo, string tagNo, string itemCode);

        OrderLocationTransaction GetOrderLocationTransaction(string orderNo, string tagNo, string itemCode, string status);

        #endregion Customized Methods
    }
}
