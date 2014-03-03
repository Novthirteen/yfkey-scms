using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using Castle.Services.Transaction;
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.View;
using com.Sconit.Service.Report;


namespace com.Sconit.Service.Business.Impl
{
    public class TransferMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private IFlowMgr flowMgr;
        private IHuMgr huMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private IItemMgr itemMgr;
        private IOrderDetailMgr orderDetailMgr;
        private IOrderMgr orderMgr;
        private ILanguageMgr languageMgr;
        private IReceiptDetailMgr receiptDetailMgr;
        private IReportMgr reportMgr;
        private IStorageBinMgr storageBinMgr;
        private ILocationMgr locationMgr;

        public TransferMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            IFlowMgr flowMgr,
            IHuMgr huMgr,
            ILocationLotDetailMgr locationLotDetailMgr,
            IItemMgr itemMgr,
            IOrderDetailMgr orderDetailMgr,
            IOrderMgr orderMgr,
            ILanguageMgr languageMgr,
            IReceiptDetailMgr receiptDetailMgr,
            IReportMgr reportMgr,
            IStorageBinMgr storageBinMgr,
            ILocationMgr locationMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.flowMgr = flowMgr;
            this.huMgr = huMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.itemMgr = itemMgr;
            this.orderDetailMgr = orderDetailMgr;
            this.orderMgr = orderMgr;
            this.languageMgr = languageMgr;
            this.receiptDetailMgr = receiptDetailMgr;
            this.reportMgr = reportMgr;
            this.storageBinMgr = storageBinMgr;
            this.locationMgr = locationMgr;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {
            if (resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_BIN)
            {
                setBaseMgr.FillResolverByBin(resolver);
            }
            //暂不支持不扫描物流路线移库到库位
            //else if (resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_LOCATION)
            //{
            //    setBaseMgr.FillResolverByLocation(resolver);
            //}
            else if (resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_FLOW)
            {
                setBaseMgr.FillResolverByFlow(resolver);
                if (resolver.OrderType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
                {
                    throw new BusinessErrorException("Flow.Error.FlowTypeIsNotTransfer", resolver.OrderType);
                }
            }
            else
            {
                throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
            }
        }

        protected override void GetDetail(Resolver resolver)
        {
            ///setBaseMgr.FillDetailByFlow(resolver);///
        }

        /// <summary>
        /// 只有发货扫描条码才支持不扫物流路线,扫描库格移库
        /// </summary>
        /// <param name="resolver"></param>
        protected override void SetDetail(Resolver resolver)
        {
            List<string> flowTypes = new List<string>();
            flowTypes.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER);
            Hu hu = huMgr.CheckAndLoadHu(resolver.Input);
            if (this.locationMgr.IsHuOcuppyByPickList(resolver.Input))
            {
                throw new BusinessErrorException("Order.Error.PickUp.HuOcuppied", resolver.Input);
            }
            FlowView flowView = null;
            if (resolver.CodePrefix != null && resolver.CodePrefix.Trim() != string.Empty)
            {
                flowView = flowMgr.CheckAndLoadFlowView(resolver.Code, null, null, null, hu, flowTypes);
            }
            else
            {
                if (resolver.BinCode == null || resolver.BinCode == string.Empty)
                {
                    throw new BusinessErrorException("Common.Business.Error.ScanFlowOrStorageBinFirst");
                }
                else
                {
                    flowView = flowMgr.CheckAndLoadFlowView(null, resolver.UserCode, hu.Location, resolver.LocationToCode, hu, flowTypes);
                    setBaseMgr.FillResolverByFlow(resolver, flowView.Flow);
                }
            }

            #region 先进先出校验
            if (flowView.Flow.IsGoodsReceiveFIFO)
            {
                IList<string> huIdList = new List<string>();
                if (resolver.Transformers != null && resolver.Transformers.Count > 0)
                {
                    foreach (Transformer transformer in resolver.Transformers)
                    {
                        if (transformer.TransformerDetails != null && transformer.TransformerDetails.Count > 0)
                        {
                            foreach (TransformerDetail det in transformer.TransformerDetails)
                            {
                                if (det.CurrentQty != decimal.Zero) {
                                    huIdList.Add(det.HuId);
                                }
                            }
                        }
                    }
                }

                string minLot = setDetailMgr.CheckFIFO(hu, huIdList) ;
                if (minLot != string.Empty && minLot != null)
                {
                    throw new BusinessErrorException("FIFO.ERROR", hu.HuId, minLot);
                }
            }
            #endregion

            setDetailMgr.MatchHuByFlowView(resolver, flowView, hu);
        }

        protected override void ExecuteSubmit(Resolver resolver)
        {
            this.TransferOrder(resolver);
        }

        protected override void ExecuteCancel(Resolver resolver)
        {
            executeMgr.CancelOperation(resolver);
        }


        /// <summary>
        /// 移库
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="transformerList"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public void TransferOrder(Resolver resolver)
        {
            IList<OrderDetail> orderDetails = executeMgr.ConvertResolverToOrderDetails(resolver);

            Receipt receipt = orderMgr.QuickReceiveOrder(resolver.Code, orderDetails, resolver.UserCode);

            #region Print
            if (resolver.NeedPrintReceipt && resolver.IsCSClient)
            {
                receipt.ReceiptDetails = receiptDetailMgr.SummarizeReceiptDetails(receipt.ReceiptDetails);

                IList<object> list = new List<object>();
                list.Add(receipt);
                list.Add(receipt.ReceiptDetails);
                resolver.PrintUrl = reportMgr.WriteToFile("ReceiptNotes.xls", list);
            }
            #endregion
            resolver.Result = languageMgr.TranslateMessage("Receipt.Transfer.Successfully", resolver.UserCode, receipt.ReceiptNo);
            resolver.Code = receipt.ReceiptNo;
            resolver.Transformers = null;//TransformerHelper.ConvertReceiptToTransformer(receipt.ReceiptDetails);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void ExecutePrint(Resolver resolver)
        {
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void GetReceiptNotes(Resolver resolver)
        {
        }
    }
}
