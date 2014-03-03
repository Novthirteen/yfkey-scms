using System;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Distribution;
using System.Drawing;
using com.Sconit.Entity.Exception;
using NHibernate.Expression;

namespace com.Sconit.Utility
{
    public static class OrderHelper
    {
        public static char HuSeperateSymbol = ';';
        public static char HuLotNoQtySeperateSymbol = ',';

        public static string GetOrderPartyFromLabel(string type)
        {
            string partyFromLabel = string.Empty;
            if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                partyFromLabel = "${MasterData.Order.OrderHead.PartyFrom}";
            }
            else
            {
                partyFromLabel = "${MasterData.Order.OrderHead.PartyFrom.Region}";
            }
            return partyFromLabel;
        }

        public static string GetOrderPartyToLabel(string type)
        {
            string partyToLabel = string.Empty;
            if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                partyToLabel = "${MasterData.Order.OrderHead.PartyTo.Customer}";
            }
            else
            {
                partyToLabel = "${MasterData.Order.OrderHead.PartyTo.Region}";
            }
            return partyToLabel;
        }

        public static string GetOrderLabel(string type)
        {
            string OrderLabel = string.Empty;
            if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                OrderLabel = "${MasterData.Order.OrderHead.Distribution}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                OrderLabel = "${MasterData.Order.OrderHead.Procurement}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                OrderLabel = "${MasterData.Order.OrderHead.Production}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_INSPECTION)
            {
                OrderLabel = "${MasterData.Order.OrderHead.Inspection}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
            {
                OrderLabel = "${MasterData.Order.OrderHead.Transfer}";
            }
            //else if (type == BusinessConstants.ORDER_MODULETYPE_VALUE_PROCUREMENTCONFIRM)
            //{
            //    OrderLabel = "${Common.Business.Order}";
            //}
            return OrderLabel;
        }

        public static string GetOrderDetailLabel(string type)
        {
            string OrderLabel = string.Empty;
            if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                OrderLabel = "${MasterData.Order.OrderDetail.Distribution}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                OrderLabel = "${MasterData.Order.OrderDetail.Procurement}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                OrderLabel = "${MasterData.Order.OrderDetail.Production}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_INSPECTION)
            {
                OrderLabel = "${MasterData.Order.OrderDetail.Inspection}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
            {
                OrderLabel = "${MasterData.Order.OrderDetail.Transfer}";
            }
            return OrderLabel;
        }

        public static string GetOrderRoutingLabel(string type)
        {
            string OrderLabel = string.Empty;
            if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                OrderLabel = "${MasterData.Order.OrderHead.Routing.Distribution}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                OrderLabel = "${MasterData.Order.OrderHead.Routing.Procurement}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                OrderLabel = "${MasterData.Order.OrderHead.Routing.Production}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_INSPECTION)
            {
                OrderLabel = "${MasterData.Order.OrderHead.Routing.Inspection}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
            {
                OrderLabel = "${MasterData.Order.OrderHead.Routing.Transfer}";
            }
            return OrderLabel;
        }

        public static string GetOrderHeadAddLabel(string type)
        {
            string OrderHeadLabel = string.Empty;
            if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                OrderHeadLabel = "${MasterData.Order.OrderHead.Distribution.Add}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                OrderHeadLabel = "${MasterData.Order.OrderHead.Procurement.Add}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                OrderHeadLabel = "${MasterData.Order.OrderHead.Production.Add}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_INSPECTION)
            {
                OrderHeadLabel = "${MasterData.Order.OrderHead.Inspection.Add}";
            }
            else if (type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
            {
                OrderHeadLabel = "${MasterData.Order.OrderHead.Transfer.Add}";
            }
            return OrderHeadLabel;
        }

        public static IList<InProcessLocationDetail> FindMatchInProcessLocationDetail(ReceiptDetail receiptDetail, IList<InProcessLocationDetail> inProcessLocationDetailList)
        {
            if (inProcessLocationDetailList != null && inProcessLocationDetailList.Count > 0)
            {
                IList<InProcessLocationDetail> matchInProcessLocationDetailList = new List<InProcessLocationDetail>();

                foreach (InProcessLocationDetail inProcessLocationDetail in inProcessLocationDetailList)
                {
                    if (IsInProcessLocationDetailMatchReceiptDetail(inProcessLocationDetail, receiptDetail))
                    {
                        matchInProcessLocationDetailList.Add(inProcessLocationDetail);
                    }
                }
                return matchInProcessLocationDetailList;
            }

            return null;
        }

        public static bool IsInProcessLocationDetailMatchReceiptDetail(
            InProcessLocationDetail inProcessLocationDetail, ReceiptDetail receiptDetail)
        {
            if (receiptDetail.OrderLocationTransaction.OrderDetail.Id
                        == inProcessLocationDetail.OrderLocationTransaction.OrderDetail.Id)
            {
                if (inProcessLocationDetail.HuId != null && receiptDetail.HuId != null)
                {
                    if (receiptDetail.HuId == inProcessLocationDetail.HuId)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                //if (inProcessLocationDetail.HuId != null)
                //{
                //    //支持Hu，一定要HuId匹配
                //    if (receiptDetail.HuId == inProcessLocationDetail.HuId)
                //    {
                //        return true;
                //    }
                //}
                //else
                //{
                //    //不支持Hu，直接认为相同
                //    return true;
                //}

                return true;
            }

            return false;
        }


        public static Color GetWinTimeColor(DateTime startTime, DateTime winTime)
        {
            Color color = Color.Black;
            if (startTime > DateTime.Now)
            {
                color = Color.Green;
            }
            else if (startTime <= DateTime.Now && winTime >= DateTime.Now)
            {
                color = Color.Goldenrod;
            }
            else
            {
                color = Color.Red;
            }
            return color;
        }

        //重置发货数
        public static void ClearShippedQty(InProcessLocation inProcessLocation)
        {
            if (inProcessLocation.InProcessLocationDetails != null && inProcessLocation.InProcessLocationDetails.Count > 0)
            {
                foreach (InProcessLocationDetail inProcessLocationDetail in inProcessLocation.InProcessLocationDetails)
                {
                    inProcessLocationDetail.Qty = 0;
                }
            }
        }

        //重置收货数
        public static void ClearReceivedQty(Receipt receipt)
        {
            if (receipt.ReceiptDetails != null && receipt.ReceiptDetails.Count > 0)
            {
                foreach (ReceiptDetail receiptDetail in receipt.ReceiptDetails)
                {
                    receiptDetail.ReceivedQty = 0;
                }
            }
        }

        //反算待收数
        public static void CalculateQtyToReceive(Receipt receipt)
        {
            if (receipt.ReceiptDetails != null && receipt.ReceiptDetails.Count > 0)
            {
                bool isASN = false;
                if (receipt.InProcessLocations != null && receipt.InProcessLocations.Count > 0)
                    isASN = true;

                foreach (ReceiptDetail receiptDetail in receipt.ReceiptDetails)
                {
                    if (!isASN)
                    {
                        receiptDetail.ShippedQty = receiptDetail.OrderLocationTransaction.OrderDetail.RemainShippedQty;
                    }
                    receiptDetail.ReceivedQty = receiptDetail.ShippedQty;
                }
            }
        }

        public static bool CheckOrderOperationAuthrize(OrderHead orderHead, User user, string orderOperation, string partyAuthrizeOpt)
        {
            IList<string> orderOperationList = new List<string>();
            return CheckOrderOperationAuthrize(orderHead, user, orderOperationList, partyAuthrizeOpt);
        }

        public static bool CheckOrderOperationAuthrize(OrderHead orderHead, User user, IList<string> orderOperationList, string partyAuthrizeOpt)
        {
            if (user.Code == BusinessConstants.SYSTEM_USER_MONITOR)
            {
                return true;
            }           

            bool partyFromAuthrized = false;
            bool partyToAuthrized = false;

            if (partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM)
            {
                partyFromAuthrized = true;
            }
            else if (partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_TO)
            {
                partyToAuthrized = true;
            }

            int orderOperationAuthrizedQty = 0;
            foreach (Permission permission in user.Permissions)
            {
                if (permission.Code == orderHead.PartyFrom.Code)
                {
                    partyFromAuthrized = true;
                }

                if (permission.Code == orderHead.PartyTo.Code)
                {
                    partyToAuthrized = true;
                }

                foreach (string orderOperation in orderOperationList)
                {
                    if (permission.Code == orderOperation)
                    {
                        orderOperationAuthrizedQty++;
                        break;
                    }
                }

                if (partyFromAuthrized && partyToAuthrized && (orderOperationAuthrizedQty == orderOperationList.Count))
                {
                    break;
                }
            }

            if (!(partyFromAuthrized && partyToAuthrized))
            {
                //没有该订单的操作权限
                if (orderHead.OrderNo != null)
                {
                    throw new BusinessErrorException("Order.Error.NoAuthrization", orderHead.OrderNo);
                }
                else
                {
                    throw new BusinessErrorException("Order.Error.NoCreatePermission2", orderHead.PartyFrom.Code, orderHead.PartyTo.Code);
                }
            }

            return (orderOperationAuthrizedQty == orderOperationList.Count);
        }

        public static decimal GetDefaultReceiptQty(OrderDetail orderDetail, string defaultReceiptOpt)
        {
            decimal receiptQty = 0;
            if (defaultReceiptOpt == BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_RECEIPT_OPTION_GOODS_RECEIPT_LOTSIZE)
            {
                receiptQty = orderDetail.GoodsReceiptLotSize.HasValue ? (decimal)orderDetail.GoodsReceiptLotSize : 0;
                if (receiptQty == 0)
                {
                    receiptQty = orderDetail.OrderedQty - (orderDetail.ReceivedQty.HasValue ? (decimal)orderDetail.ReceivedQty : 0);
                }
            }
            else if (defaultReceiptOpt == BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_RECEIPT_OPTION_ZERO)
            {
            }
            return receiptQty;
        }

        /// <summary>
        /// 根据OrderLotSize拆分需求数
        /// </summary>
        /// <param name="reqQty"></param>
        /// <param name="orderLotSize"></param>
        /// <returns></returns>
        public static IList<decimal> SplitByOrderLotSize(decimal reqQty, decimal orderLotSize)
        {
            IList<decimal> reqQtyList = new List<decimal>();
            if (orderLotSize > 0)
            {
                int count = (int)Math.Floor(reqQty / orderLotSize);
                for (int i = 0; i < count; i++)
                {
                    reqQtyList.Add(orderLotSize);
                }

                decimal oddQty = reqQty % orderLotSize;
                if (oddQty > 0)
                {
                    reqQtyList.Add(oddQty);
                }
            }
            else
            {
                reqQtyList.Add(reqQty);
            }

            return reqQtyList;
        }

        /// <summary>
        /// 过滤OrderQty数量为0的明细
        /// </summary>
        /// <param name="orderHead"></param>
        public static void FilterZeroOrderQty(OrderHead orderHead)
        {
            if (orderHead.OrderDetails != null && orderHead.OrderDetails.Count > 0)
            {
                IList<OrderDetail> nonZeroOrderDetailList = new List<OrderDetail>();
                foreach (OrderDetail orderDetail in orderHead.OrderDetails)
                {
                    if (orderDetail.OrderedQty != 0)
                    {
                        nonZeroOrderDetailList.Add(orderDetail);
                    }
                }

                orderHead.OrderDetails = nonZeroOrderDetailList;
            }
        }
        /// <summary>
        /// 批量过滤OrderQty数量为0的明细
        /// </summary>
        /// <param name="orderHeadList"></param>
        public static void FilterZeroOrderQty(IList<OrderHead> orderHeadList)
        {
            if (orderHeadList != null && orderHeadList.Count > 0)
            {
                foreach (OrderHead orderHead in orderHeadList)
                {
                    FilterZeroOrderQty(orderHead);
                }
            }
        }

        public static bool GetIsDetailContainHu(bool isShipScanHu, bool isReceiptScanHu, string createHuOption)
        {
            return (isShipScanHu || isReceiptScanHu
                || createHuOption == BusinessConstants.CODE_MASTER_CREATE_HU_OPTION_VALUE_GI
                || createHuOption == BusinessConstants.CODE_MASTER_CREATE_HU_OPTION_VALUE_GR);
        }

        /// <summary>
        /// Status: Submit,In-Process,Complete,Close
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="propertyName"></param>
        public static void SetActiveOrderStatusCriteria(DetachedCriteria criteria, string propertyName)
        {
            criteria.Add(Expression.In(propertyName, new object[] {
                BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT,
                BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS,
                BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE,
                BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE}));
        }

        /// <summary>
        /// Status: Submit,In-Process
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="propertyName"></param>
        public static void SetOpenOrderStatusCriteria(DetachedCriteria criteria, string propertyName)
        {
            criteria.Add(Expression.In(propertyName, new string[] {
                BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT,
                BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS}));
        }

        public static bool IsOrderDetailValid(OrderDetail orderDetail, DateTime winTime)
        {
            bool isValid = false;

            DateTime? startDate = orderDetail.FlowDetail == null ? null : orderDetail.FlowDetail.StartDate;
            DateTime? endDate = orderDetail.FlowDetail == null ? null : orderDetail.FlowDetail.EndDate;

            if (winTime.Year.ToString() == "1")
            {
                isValid = true;
            }
            else if (startDate.HasValue && endDate.HasValue && (DateTime)startDate <= winTime && (DateTime)endDate >= winTime)
            {
                isValid = true;
            }
            else if (startDate.HasValue && !endDate.HasValue && (DateTime)startDate <= winTime)
            {
                isValid = true;
            }
            else if (!startDate.HasValue && endDate.HasValue && (DateTime)endDate >= winTime)
            {
                isValid = true;
            }
            else if (!startDate.HasValue && !endDate.HasValue)
            {
                isValid = true;
            }

            return isValid;
        }
    }
}
