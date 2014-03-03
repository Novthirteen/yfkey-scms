using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.Distribution;
using com.Sconit.Service.Distribution;
using com.Sconit.Utility;
using Castle.Services.Transaction;

namespace com.Sconit.Service.Business.Impl
{
    public class ReceiveMgr : AbstractBusinessMgr
    {
        private ILanguageMgr languageMgr;
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private IOrderHeadMgr orderHeadMgr;
        private IOrderMgr orderMgr;
        private IInProcessLocationMgr inProcessLocationMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private IHuMgr huMgr;
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;
        private IPartyMgr partyMgr;

        public ReceiveMgr(
            ILanguageMgr languageMgr,
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            IOrderHeadMgr orderHeadMgr,
            IOrderMgr orderMgr,
            IInProcessLocationMgr inProcessLocationMgr,
            ILocationLotDetailMgr locationLotDetailMgr,
            IHuMgr huMgr,
            IOrderLocationTransactionMgr orderLocationTransactionMgr,
            IPartyMgr partyMgr
            )
            : base()
        {
            this.languageMgr = languageMgr;
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.orderHeadMgr = orderHeadMgr;
            this.orderMgr = orderMgr;
            this.inProcessLocationMgr = inProcessLocationMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.huMgr = huMgr;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.partyMgr = partyMgr;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {
            if (resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_BIN)
            {
                setBaseMgr.FillResolverByBin(resolver);
            }
            else if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER)
            {
                resolver.Transformers = null;

                OrderHead orderHead = orderHeadMgr.LoadOrderHead(resolver.Input);

                #region 校验

                if (!partyMgr.CheckPartyPermission(resolver.UserCode, orderHead.PartyTo.Code))
                {
                    throw new BusinessErrorException("Common.Error.NoRegionPermission", orderHead.PartyTo.Code);
                }
                if (orderHead.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
                {
                    throw new BusinessErrorException("Order.Error.StatusErrorWhenReceive", orderHead.Status, orderHead.OrderNo);
                }

                if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                {
                    throw new BusinessErrorException("Order.Error.OrderShipIsNotProduction", orderHead.OrderNo, orderHead.Type);
                }
                #endregion
                setBaseMgr.FillResolverByOrder(resolver);

              
            }
            else if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ASN)
            {
                resolver.Transformers = null;
              

                InProcessLocation ip = inProcessLocationMgr.LoadInProcessLocation(resolver.Input);

                #region 校验

                if (!partyMgr.CheckPartyPermission(resolver.UserCode, ip.PartyTo.Code))
                {
                    throw new BusinessErrorException("Common.Error.NoRegionPermission", ip.PartyTo.Code);
                }
                if (ip.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)
                {
                    throw new BusinessErrorException("InProcessLocation.Error.StatusErrorWhenReceive", ip.Status, ip.IpNo);
                }
                #endregion

                setBaseMgr.FillResolverByASN(resolver);
            }
            else
            {
                throw new TechnicalException("Error BarcodeHead:" + resolver.BarcodeHead + " and CodePrefix:" + resolver.CodePrefix);
            }
        }

        protected override void GetDetail(Resolver resolver)
        {
            InProcessLocation inProcessLocation = null;
            //订单收货
            if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER)
            {
                inProcessLocation = orderMgr.ConvertOrderToInProcessLocation(resolver.Input);
            }
            //ASN收货
            else if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ASN)
            {
                inProcessLocation = inProcessLocationMgr.LoadInProcessLocation(resolver.Code, true);
            }
            if (inProcessLocation == null || inProcessLocation.InProcessLocationDetails == null || inProcessLocation.InProcessLocationDetails.Count == 0)
            {
                throw new BusinessErrorException("Common.Business.Error.NoDetailToReceive");
            }


            List<Transformer> newTransformerList = TransformerHelper.ConvertInProcessLocationDetailsToTransformers(inProcessLocation.InProcessLocationDetails);

            if (resolver.IsScanHu)
            {
                ClearReceivedQty(newTransformerList);
            }
            resolver.Transformers = resolver.Transformers == null ? new List<Transformer>() : resolver.Transformers;
            resolver.Transformers.AddRange(newTransformerList);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;

            if (resolver.Transformers != null && resolver.Transformers.Count > 0)
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    OrderLocationTransaction outOrderLocationTransaction = this.orderLocationTransactionMgr.LoadOrderLocationTransaction(transformer.OrderLocTransId);
                    OrderLocationTransaction inOrderLocationTransaction = orderLocationTransactionMgr.GetOrderLocationTransaction(outOrderLocationTransaction.OrderDetail.Id, BusinessConstants.IO_TYPE_IN)[0];

                    transformer.OrderLocTransId = inOrderLocationTransaction.Id;
                }
            }
        }

        protected override void SetDetail(Resolver resolver)
        {
            if (resolver.CodePrefix == string.Empty)
            {
                throw new BusinessErrorException("Common.Business.Error.ScanFlowFirst");
            }
            if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PRODUCTIONRECEIVE)
            {
                LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(resolver.Input, resolver.UserCode);
                TransformerDetail newTransformerDetail = TransformerHelper.ConvertLocationLotDetailToTransformerDetail(locationLotDetail, false);
                resolver.AddTransformerDetail(newTransformerDetail);
            }
            else
            {
                setDetailMgr.MatchReceive(resolver);
                if (resolver.BinCode != null && resolver.BinCode != string.Empty)
                {
                    resolver.Result = languageMgr.TranslateMessage("Warehouse.CurrentBinCode", resolver.UserCode, resolver.BinCode);
                }
            }
        }

        protected override void ExecuteSubmit(Resolver resolver)
        {
            resolver = this.ReceiveOrder(resolver);
        }

        protected override void ExecuteCancel(Resolver resolver)
        {
            executeMgr.CancelOperation(resolver);
        }

        /// <summary>
        /// 物流收货
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public Resolver ReceiveOrder(Resolver resolver)
        {
            ReceiptDetail recDet = new ReceiptDetail();
            IList<ReceiptDetail> receiptDetailList = orderMgr.ConvertTransformerToReceiptDetail(resolver.Transformers);
            InProcessLocation ip = null;
            if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ASN)
            {
                ip = inProcessLocationMgr.LoadInProcessLocation(resolver.Code);
            }
            Receipt receipt = orderMgr.ReceiveOrder(receiptDetailList, resolver.UserCode, ip, resolver.ExternalOrderNo);
            resolver.Transformers = TransformerHelper.ConvertReceiptToTransformer(receipt.ReceiptDetails);
            resolver.Code = receipt.ReceiptNo;
            resolver.BinCode = string.Empty;
            resolver.Result = languageMgr.TranslateMessage("Common.Business.RecNoIs", resolver.UserCode, receipt.ReceiptNo);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
            return resolver;
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void ExecutePrint(Resolver resolver)
        {
            executeMgr.PrintReceipt(resolver);
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void GetReceiptNotes(Resolver resolver)
        {
            string[] orderTypes = new string[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT,
                BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER };

            resolver = executeMgr.GetReceiptNotes(resolver, orderTypes);
        }

        private void ClearReceivedQty(List<Transformer> transformers)
        {
            if (transformers != null)
            {
                foreach (Transformer transformer in transformers)
                {
                    transformer.CurrentQty = 0M;
                    if (transformer.TransformerDetails != null)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            transformerDetail.CurrentQty = 0M;
                        }
                    }
                }
            }
        }
    }
}
