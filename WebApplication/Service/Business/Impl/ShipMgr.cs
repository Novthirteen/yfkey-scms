using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.MasterData;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;
using com.Sconit.Service.Distribution;
using com.Sconit.Service.Report;

namespace com.Sconit.Service.Business.Impl
{
    public class ShipMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IOrderMgr orderMgr;
        private IOrderHeadMgr orderHeadMgr;
        private IExecuteMgr executeMgr;
        private ILanguageMgr languageMgr;
        private IReportMgr reportMgr;
        private IInProcessLocationMgr inProcessLocationMgr;
        private IPickListResultMgr pickListResultMgr;
        private IHuMgr huMgr;
        private IFlowMgr flowMgr;
        private IPartyMgr partyMgr;
        private IPickListMgr pickListMgr;

        public ShipMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IOrderMgr orderMgr,
            IOrderHeadMgr orderHeadMgr,
            IExecuteMgr executeMgr,
            ILanguageMgr languageMgr,
            IReportMgr reportMgr,
            IInProcessLocationMgr inProcessLocationMgr,
            IPickListResultMgr pickListResultMgr,
            IHuMgr huMgr,
            IFlowMgr flowMgr,
            IPartyMgr partyMgr,
            IPickListMgr pickListMgr
            )
            : base()
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.orderMgr = orderMgr;
            this.orderHeadMgr = orderHeadMgr;
            this.executeMgr = executeMgr;
            this.languageMgr = languageMgr;
            this.reportMgr = reportMgr;
            this.inProcessLocationMgr = inProcessLocationMgr;
            this.pickListResultMgr = pickListResultMgr;
            this.huMgr = huMgr;
            this.flowMgr = flowMgr;
            this.partyMgr = partyMgr;
            this.pickListMgr = pickListMgr;
        }


        protected override void SetBaseInfo(Resolver resolver)
        {
            if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER)
            {
                bool isHasOrderNo = false;
                if (resolver.Transformers != null)
                {
                    foreach (Transformer transformer in resolver.Transformers)
                    {
                        if (transformer.OrderNo == resolver.Code)
                        {
                            isHasOrderNo = true;
                            break;
                        }
                    }
                }
                if (!isHasOrderNo)
                {
                    #region 校验
                    OrderHead orderHead = orderHeadMgr.LoadOrderHead(resolver.Input);
                    if (!partyMgr.CheckPartyPermission(resolver.UserCode, orderHead.PartyFrom.Code))
                    {
                        throw new BusinessErrorException("Common.Error.NoRegionPermission", orderHead.PartyFrom.Code);
                    }

                    if (!orderHead.IsShipByOrder && resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPORDER)
                    {
                        throw new BusinessErrorException("Order.Error.NotShipByOrder", orderHead.OrderNo);
                    }

                    if (orderHead.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
                    {
                        throw new BusinessErrorException("Common.Business.Error.StatusError", orderHead.OrderNo, orderHead.Status);
                    }

                    if (orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                        && orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION
                        && orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
                    {
                        throw new BusinessErrorException("Order.Error.OrderShipIsNotProduction", orderHead.OrderNo, orderHead.Type);
                    }
                    #endregion

                    setBaseMgr.FillResolverByOrder(resolver);
                }

            }
            else if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_PICKLIST)
            {
                resolver.Transformers = null;

                PickList pickList = pickListMgr.LoadPickList(resolver.Input);
                if (!partyMgr.CheckPartyPermission(resolver.UserCode, pickList.PartyFrom.Code))
                {
                    throw new BusinessErrorException("Common.Error.NoRegionPermission", pickList.PartyFrom.Code);
                }

                if (pickList.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
                {
                    throw new BusinessErrorException("Common.Business.Error.StatusError", pickList.PickListNo, pickList.Status);
                }

                setBaseMgr.FillResolverByPickList(resolver);
            }
            else
            {
                throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void GetDetail(Resolver resolver)
        {
            InProcessLocation inProcessLocation = null;
            //订单发货
            if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER)
            {
                if (resolver.Transformers != null && resolver.Transformers.Count > 0)
                {
                    foreach (Transformer transformer in resolver.Transformers)
                    {
                        if (resolver.Input.Trim().ToUpper() == transformer.OrderNo.Trim().ToUpper())
                        {
                            throw new BusinessErrorException("Common.Business.Error.ReScan", resolver.Code);
                        }
                    }
                    //校验订单配置选项
                    this.CheckOrderConfigValid(resolver.Input, resolver.Transformers[0].OrderNo);
                }
                else
                {
                    resolver.Transformers = new List<Transformer>();
                }
                inProcessLocation = orderMgr.ConvertOrderToInProcessLocation(resolver.Input);

                if (inProcessLocation == null || inProcessLocation.InProcessLocationDetails == null || inProcessLocation.InProcessLocationDetails.Count == 0)
                {
                    throw new BusinessErrorException("Common.Business.Error.NoDetailToShip");
                }
                if (resolver.IsScanHu && resolver.CodePrefix != BusinessConstants.CODE_PREFIX_PICKLIST)
                {
                    OrderHelper.ClearShippedQty(inProcessLocation);//清空发货数
                }
                List<Transformer> newTransformerList = TransformerHelper.ConvertInProcessLocationDetailsToTransformers(inProcessLocation.InProcessLocationDetails);
                resolver.Transformers = resolver.Transformers == null ? new List<Transformer>() : resolver.Transformers;
                resolver.Transformers.AddRange(newTransformerList);
            }
            //拣货单发货
            else if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_PICKLIST)
            {
                //inProcessLocation = orderMgr.ConvertPickListToInProcessLocation(resolver.Input);
                IList<PickListResult> pickListResultList = pickListResultMgr.GetPickListResult(resolver.Input);
                resolver.Transformers = Utility.TransformerHelper.ConvertPickListResultToTransformer(pickListResultList);
            }
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void SetDetail(Resolver resolver)
        {
            if (resolver.CodePrefix == string.Empty)
            {
                throw new BusinessErrorException("Common.Business.Error.ScanFlowFirst");
            }
            setDetailMgr.MatchShip(resolver);
        }

        protected override void ExecuteSubmit(Resolver resolver)
        {
            resolver = this.ShipOrder(resolver);
        }

        protected override void ExecuteCancel(Resolver resolver)
        {
            if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER)
            {
                executeMgr.CancelOperation(resolver);
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        private Resolver ShipOrder(Resolver resolver)
        {
            InProcessLocation inProcessLocation = null;
            if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_PICKLIST)
            {
                inProcessLocation = orderMgr.ShipOrder(resolver.Code, resolver.UserCode);
            }
            else
            {
                IList<InProcessLocationDetail> inProcessLocationDetailList = orderMgr.ConvertTransformerToInProcessLocationDetail(resolver.Transformers);
                if (inProcessLocationDetailList.Count > 0)
                {
                    inProcessLocation = orderMgr.ShipOrder(inProcessLocationDetailList, resolver.UserCode);
                }
                else
                {
                    throw new BusinessErrorException("OrderDetail.Error.OrderDetailShipEmpty");
                }
            }
            #region 打印
            if (resolver.NeedPrintAsn && resolver.IsCSClient)
            {
                resolver.PrintUrl = PrintASN(inProcessLocation);
            }
            #endregion

            resolver.Transformers = TransformerHelper.ConvertInProcessLocationDetailsToTransformers(inProcessLocation.InProcessLocationDetails);
            resolver.Result = languageMgr.TranslateMessage("Common.Business.ASNIs", resolver.UserCode, inProcessLocation.IpNo);
            resolver.Code = inProcessLocation.IpNo;
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
            return resolver;
        }

        /// <summary>
        /// 校验订单配置选项，是否允许同时发货
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="originalOrderNo"></param>
        private void CheckOrderConfigValid(string orderNo, string originalOrderNo)
        {
            OrderHead originalOrderHead = orderHeadMgr.CheckAndLoadOrderHead(originalOrderNo);
            OrderHead orderHead = orderHeadMgr.CheckAndLoadOrderHead(orderNo);
            Flow flow = this.flowMgr.LoadFlow(orderHead.Flow);
            Flow originalFlow = this.flowMgr.LoadFlow(originalOrderHead.Flow);
            #region 合并发货校验
            if (flow.Code != originalFlow.Code || flow.Type != originalFlow.Type)
                throw new BusinessErrorException("Order.Error.ShipOrder.OrderTypeNotEqual");

            if (orderHead.PartyFrom.Code != originalOrderHead.PartyFrom.Code)
                throw new BusinessErrorException("Order.Error.ShipOrder.PartyFromNotEqual");

            if (orderHead.PartyTo.Code != originalOrderHead.PartyTo.Code)
                throw new BusinessErrorException("Order.Error.ShipOrder.PartyToNotEqual");

            string shipFromCode = orderHead.ShipFrom != null ? orderHead.ShipFrom.Code : string.Empty;
            string originalShipFromCode = originalOrderHead.ShipFrom != null ? originalOrderHead.ShipFrom.Code : string.Empty;
            if (shipFromCode != originalShipFromCode)
                throw new BusinessErrorException("Order.Error.ShipOrder.ShipFromNotEqual");

            string shipToCode = orderHead.ShipTo != null ? orderHead.ShipTo.Code : string.Empty;
            string originalShipToCode = originalOrderHead.ShipTo != null ? originalOrderHead.ShipTo.Code : string.Empty;
            if (shipToCode != originalShipToCode)
                throw new BusinessErrorException("Order.Error.ShipOrder.ShipToNotEqual");

            string routingCode = orderHead.Routing != null ? orderHead.Routing.Code : string.Empty;
            string originalRoutingCode = originalOrderHead.Routing != null ? originalOrderHead.Routing.Code : string.Empty;
            if (routingCode != originalRoutingCode)
                throw new BusinessErrorException("Order.Error.ShipOrder.RoutingNotEqual");

            if (orderHead.IsShipScanHu != originalOrderHead.IsShipScanHu)
                throw new BusinessErrorException("Order.Error.ShipOrder.IsShipScanHuNotEqual");

            if (orderHead.IsReceiptScanHu != originalOrderHead.IsReceiptScanHu)
                throw new BusinessErrorException("Order.Error.ShipOrder.IsReceiptScanHuNotEqual");

            if (orderHead.IsAutoReceive != originalOrderHead.IsAutoReceive)
                throw new BusinessErrorException("Order.Error.ShipOrder.IsAutoReceiveNotEqual");

            if (orderHead.GoodsReceiptGapTo != originalOrderHead.GoodsReceiptGapTo)
                throw new BusinessErrorException("Order.Error.ShipOrder.GoodsReceiptGapToNotEqual");
            if (orderHead.AntiResolveHu != originalOrderHead.AntiResolveHu)
            {
                throw new BusinessErrorException("Order.Error.ShipOrder.AntiResolveHuNotEqual");
            }
            #endregion
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void ExecutePrint(Resolver resolver)
        {
            InProcessLocation inProcessLocation = inProcessLocationMgr.LoadInProcessLocation(resolver.Code, true);
            resolver.PrintUrl = PrintASN(inProcessLocation);
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void GetReceiptNotes(Resolver resolver)
        {
            string[] orderTypes = new string[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION,
                BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER };
            string[] row = resolver.Code.Split('|');
            int firstRow = int.Parse(row[0]);
            int maxRows = int.Parse(row[1]);
            IList<InProcessLocation> inProcessLocationList = inProcessLocationMgr.GetInProcessLocation(resolver.UserCode, firstRow, maxRows, orderTypes);
            resolver.ReceiptNotes = ConvertInProcessLocationsToReceiptNotes(inProcessLocationList);
        }

        private List<ReceiptNote> ConvertInProcessLocationsToReceiptNotes(IList<InProcessLocation> inProcessLocations)
        {
            if (inProcessLocations == null)
            {
                return null;
            }
            int seq = 1;
            List<ReceiptNote> receiptNotes = new List<ReceiptNote>();
            foreach (InProcessLocation inProcessLocation in inProcessLocations)
            {
                ReceiptNote receiptNote = new ReceiptNote();
                receiptNote.CreateDate = inProcessLocation.CreateDate;
                receiptNote.CreateUser = inProcessLocation.CreateUser == null ? string.Empty : inProcessLocation.CreateUser.Name;
                receiptNote.Dock = inProcessLocation.DockDescription;
                receiptNote.IpNo = inProcessLocation.IpNo;
                receiptNote.PartyFrom = inProcessLocation.PartyFrom == null ? string.Empty : inProcessLocation.PartyFrom.Name;
                receiptNote.PartyTo = inProcessLocation.PartyTo == null ? string.Empty : inProcessLocation.PartyTo.Name;
                receiptNote.Sequence = seq;
                receiptNotes.Add(receiptNote);
                seq++;
            }
            return receiptNotes;
        }

        private string PrintASN(InProcessLocation inProcessLocation)
        {
            IList<object> list = new List<object>();
            list.Add(inProcessLocation);
            list.Add(inProcessLocation.InProcessLocationDetails);

            string printUrl = reportMgr.WriteToFile(inProcessLocation.AsnTemplate, list);
            //this.PrintOrder(printUrl);
            //报表url
            return printUrl;
        }

    }
}
