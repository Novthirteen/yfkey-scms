using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using Castle.Services.Transaction;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.Production;
using com.Sconit.Service.Report;

namespace com.Sconit.Service.Business.Impl
{
    public class ExecuteMgr : IExecuteMgr
    {
        private ILanguageMgr languageMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private IStorageBinMgr storageBinMgr;
        private IInspectOrderDetailMgr inspectOrderDetailMgr;
        private IHuMgr huMgr;
        private IOrderMgr orderMgr;
        private IEntityPreferenceMgr entityPreferenceMgr;
        private IItemMgr itemMgr;
        private IUomMgr uomMgr;
        private ILocationMgr locationMgr;
        private IFlowMgr flowMgr;
        private IUserMgr userMgr;
        private IReceiptMgr receiptMgr;
        private IReceiptDetailMgr receiptDetailMgr;
        private IReportMgr reportMgr;
        private ISetDetailMgr setDetailMgr;

        public ExecuteMgr(
            ILanguageMgr languageMgr,
            ILocationLotDetailMgr locationLotDetailMgr,
            IStorageBinMgr storageBinMgr,
            IInspectOrderDetailMgr inspectOrderDetailMgr,
            IHuMgr huMgr,
            IOrderMgr orderMgr,
            IEntityPreferenceMgr entityPreferenceMgr,
            IItemMgr itemMgr,
            IUomMgr uomMgr,
            ILocationMgr locationMgr,
            IFlowMgr flowMgr,
            IUserMgr userMgr,
            IReceiptMgr receiptMgr,
            IReceiptDetailMgr receiptDetailMgr,
            IReportMgr reportMgr,
            ISetDetailMgr setDetailMgr
            )
        {
            this.languageMgr = languageMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.storageBinMgr = storageBinMgr;
            this.inspectOrderDetailMgr = inspectOrderDetailMgr;
            this.huMgr = huMgr;
            this.orderMgr = orderMgr;
            this.entityPreferenceMgr = entityPreferenceMgr;
            this.itemMgr = itemMgr;
            this.uomMgr = uomMgr;
            this.locationMgr = locationMgr;
            this.flowMgr = flowMgr;
            this.userMgr = userMgr;
            this.receiptMgr = receiptMgr;
            this.receiptDetailMgr = receiptDetailMgr;
            this.reportMgr = reportMgr;
            this.setDetailMgr = setDetailMgr;
        }

        public void CancelRepackOperation(Resolver resolver)
        {
            if (resolver.Transformers.Count == 2)
            {
                if (resolver.Transformers[1].TransformerDetails != null && resolver.Transformers[1].TransformerDetails.Count > 0)
                {
                    //resolver.Transformers[1].TransformerDetails = null;
                    int maxSeq = setDetailMgr.FindMaxSeq(resolver.Transformers[1]);
                    resolver.Transformers[1].TransformerDetails.RemoveAt(maxSeq);
                    resolver.IOType = BusinessConstants.IO_TYPE_OUT;
                }
                else if ((resolver.Transformers[1].TransformerDetails == null || resolver.Transformers[1].TransformerDetails.Count == 0)
                    && resolver.IOType == BusinessConstants.IO_TYPE_OUT && resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_REPACK)
                {
                    resolver.IOType = BusinessConstants.IO_TYPE_IN;
                }
                else if (resolver.Transformers[0].TransformerDetails != null && resolver.Transformers[0].TransformerDetails.Count > 0)
                {
                    //resolver.Transformers[0].TransformerDetails = null;
                    //resolver.Transformers[1].TransformerDetails = null;
                    int maxSeq = setDetailMgr.FindMaxSeq(resolver.Transformers[0]);
                    resolver.Transformers[0].TransformerDetails.RemoveAt(maxSeq);
                    resolver.IOType = BusinessConstants.IO_TYPE_IN;
                }
            }
        }

        /// <summary>
        /// 分步取消
        /// </summary>
        /// <param name="resolver"></param>
        public void CancelOperation(Resolver resolver)
        {
            if (TotalCurrentQty(resolver) == 0)
            {
                resolver.Transformers = null;
                resolver.BinCode = null;
                resolver.Code = null;
                resolver.CodePrefix = null;
                resolver.Description = null;
                resolver.OrderType = null;
                resolver.PickBy = null;
                resolver.BarcodeHead = null;
                resolver.LocationCode = null;
                resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
                resolver.Result = string.Empty;//languageMgr.TranslateMessage("Resolver.Cancel.BinAllCancel", resolver.UserCode);
                return;
            }

            if (setDetailMgr.FindMaxSeq(resolver.Transformers) >= 0)
            {
                //最后一条记录的当前数设置CurrentQty为0 设置序号Sequence为0 //为了兼容带条码发货
                int[] indexArray = this.FindMaxSeqTransformerDetailRowAndColumnIndex(resolver.Transformers);
                //resolver.Transformers[indexArray[0]].TransformerDetails[indexArray[1]] = null;
                if (resolver.CodePrefix == BusinessConstants.BARCODE_HEAD_FLOW)
                {
                    resolver.Transformers[indexArray[0]].Qty -= resolver.Transformers[indexArray[0]].TransformerDetails[indexArray[1]].Qty;
                }
                resolver.Transformers[indexArray[0]].TransformerDetails[indexArray[1]].CurrentQty = 0;
                resolver.Transformers[indexArray[0]].TransformerDetails[indexArray[1]].Sequence = -1;
                //设置Bin为最后一库格
                indexArray = this.FindMaxSeqTransformerDetailRowAndColumnIndex(resolver.Transformers);
                if (indexArray != null)
                {
                    resolver.BinCode = resolver.Transformers[indexArray[0]].TransformerDetails[indexArray[1]].StorageBinCode;
                }
            }

            TransformerHelper.ProcessTransformer(resolver.Transformers);
            if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PUTAWAY
                || resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKUP
                || (IsHasTransformerDetail(resolver) && resolver.ModuleType != BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLIST))
            {
                resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
                if (resolver.BinCode != null && resolver.BinCode != string.Empty)
                {
                    resolver.Result = languageMgr.TranslateMessage("Warehouse.CurrentBinCode", resolver.UserCode, resolver.BinCode);
                }
            }
            else
            {
                resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
            }
            resolver.BarcodeHead = string.Empty;
        }

        public IList<LocationLotDetail> ConvertTransformersToLocationLotDetails(List<Transformer> transformerList, bool isPutAway)
        {
            IList<LocationLotDetail> locationLotDetailList = new List<LocationLotDetail>();
            foreach (Transformer transformer in transformerList)
            {
                if (transformer.TransformerDetails != null && transformer.TransformerDetails.Count > 0)
                {
                    foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                    {
                        locationLotDetailList.Add(this.ConvertTransformerDetailToLocationLotDetail(transformerDetail, isPutAway));
                    }
                }
            }

            return locationLotDetailList;
        }

        public LocationLotDetail ConvertTransformerDetailToLocationLotDetail(TransformerDetail transformerDetail, bool isPutAway)
        {
            LocationLotDetail locationLotDetail = locationLotDetailMgr.LoadLocationLotDetail(transformerDetail.Id);
            locationLotDetail.CurrentInspectQty = locationLotDetail.Qty;
            if (isPutAway && transformerDetail.StorageBinCode != null && transformerDetail.StorageBinCode.Trim() != string.Empty)
            {
                locationLotDetail.NewStorageBin = storageBinMgr.CheckAndLoadStorageBin(transformerDetail.StorageBinCode);
            }

            return locationLotDetail;
        }

        /// <summary>
        /// 把Resolver转成InspectOrderDetails
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public IList<InspectOrderDetail> ConvertResolverToInspectOrderDetails(Resolver resolver)
        {
            IList<InspectOrderDetail> inspectDetailList = new List<InspectOrderDetail>();

            if (resolver != null && resolver.Transformers != null)
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer.TransformerDetails != null)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            InspectOrderDetail inspectDetail = inspectOrderDetailMgr.LoadInspectOrderDetail(transformerDetail.Id);
                            inspectDetail.CurrentQualifiedQty = transformerDetail.CurrentQty;
                            inspectDetail.CurrentRejectedQty = transformerDetail.CurrentRejectQty;
                            if (inspectDetail.CurrentQualifiedQty != 0 || inspectDetail.CurrentRejectedQty != 0)
                            {
                                inspectDetailList.Add(inspectDetail);
                            }
                        }
                    }
                }
            }
            return inspectDetailList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<RepackDetail> ConvertTransformerListToRepackDetail(IList<Transformer> transformerList)
        {
            IList<RepackDetail> repackDetailList = new List<RepackDetail>();

            if (transformerList != null && transformerList.Count == 2)
            {
                foreach (Transformer transformer in transformerList)
                {
                    if (transformer.TransformerDetails != null)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            RepackDetail repackDetail = new RepackDetail();
                            repackDetail.IOType = transformerDetail.IOType;

                            if (transformerDetail.HuId != string.Empty)
                            {
                                repackDetail.Hu = huMgr.LoadHu(transformerDetail.HuId);
                                repackDetail.Qty = repackDetail.Hu.Qty * repackDetail.Hu.UnitQty;
                            }
                            else
                            {
                                repackDetail.Qty = transformerDetail.Qty;
                                repackDetail.itemCode = transformerDetail.ItemCode;
                            }
                            if (transformerDetail.LocationLotDetId != 0)
                            {
                                repackDetail.LocationLotDetail = locationLotDetailMgr.LoadLocationLotDetail(transformerDetail.LocationLotDetId);
                            }
                            repackDetailList.Add(repackDetail);
                        }
                    }
                }
            }
            return repackDetailList;
        }

        /// <summary>
        /// todo:未考虑生产调整  移库 退货  //newOrderDetail.IsScanHu = true;??
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="flow"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderDetail> ConvertResolverToOrderDetails(Resolver resolver, Flow flow)
        {
            OrderHead orderHead = orderMgr.TransferFlow2Order(flow);
            IList<OrderDetail> orderDetails = new List<OrderDetail>();
            if (resolver.Transformers == null)
            {
                throw new BusinessErrorException("OrderDetail.Error.OrderDetailEmpty");
            }
            foreach (Transformer transformer in resolver.Transformers)
            {
                if (transformer.TransformerDetails != null)
                {
                    foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                    {
                        if (transformerDetail.CurrentQty == 0) //数量为零的过滤掉
                        {
                            continue;
                        }

                        OrderDetail newOrderDetail = new OrderDetail();
                        //newOrderDetail.IsScanHu = true;
                        int seqInterval = int.Parse(entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL).Value);
                        if (orderDetails == null || orderDetails.Count == 0)
                        {
                            newOrderDetail.Sequence = seqInterval;
                        }
                        else
                        {
                            newOrderDetail.Sequence = orderDetails.Last<OrderDetail>().Sequence + seqInterval;
                        }
                        newOrderDetail.Item = itemMgr.LoadItem(transformerDetail.ItemCode);
                        newOrderDetail.Uom = uomMgr.LoadUom(transformerDetail.UomCode);
                        newOrderDetail.HuId = transformerDetail.HuId;
                        if ((resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN)
                                || resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN)
                        {
                            newOrderDetail.OrderedQty = -transformerDetail.CurrentQty;
                            newOrderDetail.HuQty = -transformerDetail.Qty;
                        }
                        else
                        {
                            newOrderDetail.OrderedQty = transformerDetail.CurrentQty;
                            newOrderDetail.HuQty = transformerDetail.Qty;
                        }
                        if (!(resolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                            || resolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS))
                        {
                            newOrderDetail.LocationFrom = locationMgr.LoadLocation(transformer.LocationFromCode);
                        }
                        if (!(resolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION))
                        {
                            newOrderDetail.LocationTo = locationMgr.LoadLocation(transformer.LocationToCode);
                        }
                        newOrderDetail.ReferenceItemCode = transformer.ReferenceItemCode;
                        newOrderDetail.UnitCount = transformerDetail.UnitCount;
                        //newOrderDetail.PackageType = transformerDetail.PackageType;
                        newOrderDetail.OrderHead = orderHead;
                        newOrderDetail.IsScanHu = true;
                        newOrderDetail.PutAwayBinCode = resolver.BinCode;
                        orderDetails.Add(newOrderDetail);
                    }
                }
            }
            return orderDetails;
        }

        /// <summary>
        /// 移库使用
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        public IList<OrderDetail> ConvertResolverToOrderDetails(Resolver resolver)
        {
            IList<OrderDetail> orderDetailList = new List<OrderDetail>();
            if (resolver.IsScanHu)
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.UnitCount = transformerDetail.UnitCount;
                        orderDetail.Item = itemMgr.LoadItem(transformerDetail.ItemCode);
                        orderDetail.Uom = uomMgr.LoadUom(transformerDetail.UomCode);
                        orderDetail.LocationFrom = locationMgr.LoadLocation(transformer.LocationFromCode);
                        orderDetail.LocationTo = locationMgr.LoadLocation(transformer.LocationToCode);
                        orderDetail.HuId = transformerDetail.HuId;
                        orderDetail.OrderedQty = transformerDetail.CurrentQty;
                        orderDetail.RequiredQty = transformerDetail.CurrentQty;
                        orderDetail.PutAwayBinCode = transformerDetail.StorageBinCode;
                        orderDetailList.Add(orderDetail);
                    }
                }
            }
            else
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.UnitCount = transformer.UnitCount;
                    orderDetail.Item = itemMgr.LoadItem(transformer.ItemCode);
                    orderDetail.Uom = uomMgr.LoadUom(transformer.UomCode);
                    orderDetail.LocationFrom = locationMgr.LoadLocation(transformer.LocationFromCode);
                    orderDetail.LocationTo = locationMgr.LoadLocation(transformer.LocationToCode);
                    orderDetail.OrderedQty = transformer.CurrentQty;
                    orderDetail.RequiredQty = transformer.CurrentQty;
                    orderDetailList.Add(orderDetail);
                }
            }
            return orderDetailList;
        }

        public IList<MaterialIn> ConvertTransformersToMaterialIns(List<Transformer> transformerList)
        {
            IList<MaterialIn> materialInList = new List<MaterialIn>();
            if (transformerList != null)
            {
                foreach (Transformer transformer in transformerList)
                {
                    //如果是扫描Hu的
                    if (transformer.TransformerDetails != null && transformer.TransformerDetails.Count > 0)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            MaterialIn materialIn = new MaterialIn();
                            materialIn.HuId = transformerDetail.HuId;
                            materialIn.RawMaterial = itemMgr.LoadItem(transformerDetail.ItemCode);
                            materialIn.Location = locationMgr.LoadLocation(transformerDetail.LocationCode);
                            materialIn.LotNo = transformerDetail.LotNo;
                            materialIn.Operation = transformerDetail.Operation;
                            materialIn.Qty = transformerDetail.CurrentQty;
                            materialInList.Add(materialIn);
                        }
                    }
                    //如果是直接输入数量的
                    else
                    {
                        MaterialIn materialIn = new MaterialIn();
                        materialIn.RawMaterial = itemMgr.LoadItem(transformer.ItemCode);
                        materialIn.Location = locationMgr.LoadLocation(transformer.LocationCode);
                        materialIn.Operation = transformer.Operation;
                        materialIn.Qty = transformer.CurrentQty;
                        materialInList.Add(materialIn);
                    }
                }
            }
            return materialInList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void OrderReturn(Resolver resolver)
        {
            Flow flow = this.flowMgr.CheckAndLoadFlow(resolver.Code, true);
            User user = this.userMgr.CheckAndLoadUser(resolver.UserCode);
            string orderSubType = BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN;
            DateTime winTime = DateTime.Now;
            DateTime startTime = DateTime.Now;
            IList<OrderDetail> orderDetails = this.ConvertResolverToOrderDetails(resolver, flow);

            Receipt receipt = orderMgr.QuickReceiveOrder(flow, orderDetails, user, orderSubType, winTime, startTime, false, null, null);
            resolver.Code = receipt.ReceiptNo;
            resolver.Result = languageMgr.TranslateMessage("MasterData.Order.Return.Successfully", resolver.UserCode, receipt.ReceiptNo);
            resolver.Transformers = null;//TransformerHelper.ConvertReceiptToTransformer(receipt.ReceiptDetails);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }

        [Transaction(TransactionMode.Unspecified)]
        public Resolver GetReceiptNotes(Resolver resolver, params string[] orderTypes)
        {
            string[] row = resolver.Code.Split('|');
            int firstRow = int.Parse(row[0]);
            int maxRows = int.Parse(row[1]);
            IList<Receipt> receiptList = receiptMgr.GetReceipts(resolver.UserCode, firstRow, maxRows, orderTypes);
            resolver.ReceiptNotes = ConvertReceipsToReceiptNotes(receiptList);
            return resolver;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void PrintReceipt(Resolver resolver)
        {
            Receipt receipt = receiptMgr.LoadReceipt(resolver.Code, true);
            receipt.ReceiptDetails = receiptDetailMgr.SummarizeReceiptDetails(receipt.ReceiptDetails);

            IList<object> list = new List<object>();
            list.Add(receipt);
            list.Add(receipt.ReceiptDetails);
            resolver.PrintUrl = reportMgr.WriteToFile("ReceiptNotes.xls", list);
        }

        #region Private Method
        private decimal TotalCurrentQty(Resolver resolver)
        {
            decimal totalQty = 0;
            if (resolver != null && resolver.Transformers != null)
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer.TransformerDetails != null)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            totalQty += transformerDetail.CurrentQty;
                        }
                    }
                }
            }
            return totalQty;
        }

        private int TransformerDetailSeqComparer(TransformerDetail x, TransformerDetail y)
        {
            return x.Sequence - y.Sequence;
        }

        private void ClearTransformerDetail(List<Transformer> transformerList)
        {
            if (transformerList != null && transformerList.Count > 0)
            {
                foreach (Transformer transformer in transformerList)
                {
                    if (transformer.TransformerDetails != null)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            transformerDetail.CurrentQty = 0;
                            transformerDetail.Sequence = 0;
                        }
                    }
                    transformer.CurrentQty = 0;
                    transformer.Cartons = 0;
                }
            }
        }

        private List<ReceiptNote> ConvertReceipsToReceiptNotes(IList<Receipt> Receipts)
        {
            if (Receipts == null)
            {
                return null;
            }
            List<ReceiptNote> receiptNotes = new List<ReceiptNote>();
            int seq = 1;
            foreach (Receipt receipt in Receipts)
            {
                ReceiptNote receiptNote = new ReceiptNote();
                receiptNote.CreateDate = receipt.CreateDate;
                receiptNote.CreateUser = receipt.CreateUser == null ? string.Empty : receipt.CreateUser.Name;
                receiptNote.Dock = receipt.DockDescription;
                receiptNote.IpNo = receipt.ReferenceIpNo;
                receiptNote.PartyFrom = receipt.PartyFrom == null ? string.Empty : receipt.PartyFrom.Name;
                receiptNote.PartyTo = receipt.PartyTo == null ? string.Empty : receipt.PartyTo.Name;
                receiptNote.ReceiptNo = receipt.ReceiptNo;
                receiptNote.Sequence = seq;
                receiptNotes.Add(receiptNote);
                seq++;
            }
            return receiptNotes;
        }

        /// <summary>
        /// 查找最大序号的TransformerDetail所在的行和列
        /// </summary>
        /// <param name="transformerList"></param>
        /// <returns></returns>
        private int[] FindMaxSeqTransformerDetailRowAndColumnIndex(List<Transformer> transformerList)
        {
            int maxSeq = FindMaxSeqFilterByCurrentQty(transformerList);
            if (transformerList != null)
            {
                for (int i = 0; i < transformerList.Count; i++)
                {
                    if (transformerList[i].TransformerDetails != null)
                    {
                        for (int j = 0; j < transformerList[i].TransformerDetails.Count; j++)
                        {
                            if (transformerList[i].TransformerDetails[j].Sequence == maxSeq)
                            {
                                return new int[] { i, j };
                            }
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 检查TransformerDetail是否为空(检查CurrentQty)
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        private bool IsHasTransformerDetail(Resolver resolver)
        {
            if (resolver != null && resolver.Transformers != null)
            {
                decimal CurrentQty = 0;
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer != null)
                    {
                        if (transformer.TransformerDetails != null)
                        {
                            foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                            {
                                CurrentQty += transformerDetail.CurrentQty;
                            }
                        }
                    }
                }
                if (CurrentQty > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private int FindMaxSeqFilterByCurrentQty(List<Transformer> transformerList)
        {
            int maxSeq = 0;
            if (transformerList != null)
            {
                foreach (Transformer transformer in transformerList)
                {
                    if (transformer.TransformerDetails != null)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            if (transformerDetail.Sequence > maxSeq && transformerDetail.CurrentQty != 0)
                            {
                                maxSeq = transformerDetail.Sequence;
                            }
                        }
                    }
                }
            }
            return maxSeq;
        }


        #endregion

    }
}
