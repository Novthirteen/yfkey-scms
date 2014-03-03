using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.Criteria;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using com.Sconit.Utility;
using com.Sconit.Entity.Distribution;
using com.Sconit.Service.Distribution;
using NHibernate.Expression;
using com.Sconit.Entity.Production;

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ScanBarcodeMgr : IScanBarcodeMgr
    {
        #region 变量
        private ICriteriaMgr criteriaMgr;
        private IStorageBinMgr storageBinMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private IHuMgr huMgr;
        private IFlowMgr flowMgr;
        private IFlowDetailMgr flowDetailMgr;
        private IPickListMgr pickListMgr;
        private IPickListDetailMgr pickListDetailMgr;
        private IPickListResultMgr pickListResultMgr;
        private IUserMgr userMgr;
        private IInProcessLocationMgr inProcessLocationMgr;
        private IReceiptMgr receiptMgr;
        private IOrderHeadMgr orderHeadMgr;
        private IOrderDetailMgr orderDetailMgr;
        private IOrderMgr orderMgr;
        private ILocationMgr locationMgr;
        private IItemMgr itemMgr;
        private IUomMgr uomMgr;
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;
        private IEmployeeMgr employeeMgr;
        private IEntityPreferenceMgr entityPreferenceMgr;
        private IUomConversionMgr uomConversionMgr;
        private INumberControlMgr numberControlMgr;
        private ILanguageMgr languageMgr;
        private IRepackMgr repackMgr;
        private IInspectOrderMgr inspectOrderMgr;
        private IInspectOrderDetailMgr inspectOrderDetailMgr;
        private IProductLineInProcessLocationDetailMgr productLineInProcessLocationDetailMgr;
        #endregion

        #region 构造函数
        public ScanBarcodeMgr(ICriteriaMgr criteriaMgr,
            IStorageBinMgr storageBinMgr,
            ILocationLotDetailMgr locationLotDetailMgr,
            IHuMgr huMgr,
            IFlowMgr flowMgr,
            IFlowDetailMgr flowDetailMgr,
            IPickListMgr pickListMgr,
            IPickListDetailMgr pickListDetailMgr,
            IPickListResultMgr pickListResultMgr,
            IUserMgr userMgr,
            IInProcessLocationMgr inProcessLocationMgr,
            IReceiptMgr receiptMgr,
            IOrderHeadMgr orderHeadMgr,
            IOrderDetailMgr orderDetailMgr,
            IOrderMgr orderMgr,
            ILocationMgr locationMgr,
            IItemMgr itemMgr,
            IUomMgr uomMgr,
            IOrderLocationTransactionMgr orderLocationTransactionMgr,
            IEmployeeMgr employeeMgr,
            IEntityPreferenceMgr entityPreferenceMgr,
            IUomConversionMgr uomConversionMgr,
            INumberControlMgr numberControlMgr,
            ILanguageMgr languageMgr,
            IRepackMgr repackMgr,
            IInspectOrderMgr inspectOrderMgr,
            IInspectOrderDetailMgr inspectOrderDetailMgr,
            IProductLineInProcessLocationDetailMgr productLineInProcessLocationDetailMgr)
        {
            this.criteriaMgr = criteriaMgr;
            this.storageBinMgr = storageBinMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.huMgr = huMgr;
            this.flowMgr = flowMgr;
            this.flowDetailMgr = flowDetailMgr;
            this.pickListMgr = pickListMgr;
            this.pickListDetailMgr = pickListDetailMgr;
            this.pickListResultMgr = pickListResultMgr;
            this.userMgr = userMgr;
            this.inProcessLocationMgr = inProcessLocationMgr;
            this.receiptMgr = receiptMgr;
            this.orderHeadMgr = orderHeadMgr;
            this.orderDetailMgr = orderDetailMgr;
            this.orderMgr = orderMgr;
            this.locationMgr = locationMgr;
            this.itemMgr = itemMgr;
            this.uomMgr = uomMgr;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.employeeMgr = employeeMgr;
            this.entityPreferenceMgr = entityPreferenceMgr;
            this.uomConversionMgr = uomConversionMgr;
            this.numberControlMgr = numberControlMgr;
            this.languageMgr = languageMgr;
            this.repackMgr = repackMgr;
            this.inspectOrderMgr = inspectOrderMgr;
            this.inspectOrderDetailMgr = inspectOrderDetailMgr;
            this.productLineInProcessLocationDetailMgr = productLineInProcessLocationDetailMgr;
        }
        #endregion

        #region IScanBarcodeMgr接口实现
        [Transaction(TransactionMode.Unspecified)]
        public Resolver ScanBarcode(Resolver resolver)
        {
            if (resolver == null || resolver.UserCode == null || resolver.UserCode == string.Empty)
            {
                resolver = new Resolver();
                resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
                return resolver;
            }
            if (resolver.Input == null || resolver.Input.Trim() == string.Empty)
            {
                throw new BusinessErrorException("Common.Business.Warn.Empty");
            }

            Resolver analyzeResolver = this.AnalyzeBarcode(resolver.Input, resolver.UserCode, resolver.ModuleType);

            #region 上线
            if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_ONLINE)
            {
                analyzeResolver.Command = BusinessConstants.BARCODE_HEAD_OK;
                orderMgr.StartOrder(resolver.Code, resolver.UserCode);
                resolver.Result = DateTime.Now.ToString("HH:mm:ss");
            }
            #endregion

            #region 下线
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_OFFLINE)
            {
                if (analyzeResolver.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER)
                {
                    resolver = analyzeResolver;
                    this.FillOffline(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
                {
                    resolver.Result = this.ReceiveWorkOrder(resolver);
                }
                else
                {
                    throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
                }
            }
            #endregion

            #region 发货
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP)
            {
                if (analyzeResolver.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER
                   || analyzeResolver.CodePrefix == BusinessConstants.CODE_PREFIX_PICKLIST)
                {
                    if (resolver.Transformers == null || resolver.Transformers.Count == 0
                        || analyzeResolver.BarcodeHead == BusinessConstants.CODE_PREFIX_PICKLIST)
                    {
                        resolver = analyzeResolver;
                    }
                    resolver.Input = analyzeResolver.Code;
                    this.FillShip(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_DEFAULT)
                {
                    if (resolver.Code == null || resolver.Code == string.Empty)
                    {
                        throw new BusinessErrorException("Common.Business.Error.RequireASNorOrder");
                    }
                    resolver.Input = analyzeResolver.Code;
                    this.MatchScan(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
                {
                    resolver = this.ShipOrder(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_CANCEL)
                {
                    this.CancelOperation(resolver);
                }
                else
                {
                    throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
                }
            }
            #endregion

            #region 收货
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE)
            {
                if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_BIN)
                {
                    if (resolver.Code == null || resolver.Code == string.Empty)
                    {
                        throw new BusinessErrorException("Common.Business.Error.RequireASNorOrder");
                    }
                    resolver.BinCode = analyzeResolver.BinCode;
                    resolver.Result = languageMgr.TranslateMessage("Warehouse.CurrentBinCode", resolver.UserCode, resolver.BinCode);
                }
                else if (analyzeResolver.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER
                    || analyzeResolver.CodePrefix == BusinessConstants.CODE_PREFIX_ASN)
                {
                    resolver = analyzeResolver;
                    this.FillReceive(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_DEFAULT)
                {
                    if (resolver.Code == null || resolver.Code == string.Empty)
                    {
                        throw new BusinessErrorException("Common.Business.Error.RequireASNorOrder");
                    }
                    resolver.Input = analyzeResolver.Code;
                    this.MatchScan(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
                {
                    resolver = this.ReceiveOrder(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_CANCEL)
                {
                    this.CancelOperation(resolver);
                }
                else
                {
                    throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
                }
            }
            #endregion

            #region 移库
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_TRANSFER)
            {
                if (analyzeResolver.CodePrefix == BusinessConstants.BARCODE_HEAD_FLOW)
                {
                    resolver = analyzeResolver;
                    resolver.Result = languageMgr.TranslateMessage("Common.Business.Message.TransferFlow", resolver.UserCode) + analyzeResolver.Description;
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_DEFAULT)
                {
                    if (resolver.Code == null || resolver.Code == string.Empty)
                    {
                        throw new BusinessErrorException("Common.Business.Error.RequireFlow");
                    }
                    resolver.Input = analyzeResolver.Code;
                    this.TransferOrderScan(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
                {
                    resolver.Result = this.TransferOrder(resolver);
                }
                else
                {
                    throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
                }
            }
            #endregion

            #region 拣货
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLIST)
            {
                if (analyzeResolver.CodePrefix == BusinessConstants.CODE_PREFIX_PICKLIST)
                {
                    resolver = analyzeResolver;
                    PickList pickList = pickListMgr.LoadPickList(resolver.Code, true);
                    resolver.Transformers = TransformerHelper.ConvertPickListDetailsToTransformers(pickList.PickListDetails);
                    resolver.Result = languageMgr.TranslateMessage("Common.Business.PickList", resolver.UserCode) + ":" + resolver.Code;
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_DEFAULT)
                {
                    if (resolver.Code == null || resolver.Code == string.Empty)
                    {
                        throw new BusinessErrorException("Common.Business.Error.RequireFlow");
                    }
                    this.MatchScan(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
                {
                    this.PickList(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_CANCEL)
                {
                    this.CancelOperation(resolver);
                }
                else
                {
                    throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
                }
            }
            #endregion

            #region 上架
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PUTAWAY)
            {
                if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_BIN)
                {
                    List<Transformer> transformerList = this.FillPutAway(resolver);
                    resolver = analyzeResolver;
                    resolver.Result = languageMgr.TranslateMessage("Warehouse.CurrentBinCode", analyzeResolver.UserCode, analyzeResolver.BinCode);
                    resolver.Transformers = transformerList;
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_DEFAULT)
                {
                    resolver.Input = analyzeResolver.Code;
                    this.MatchPutAway(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
                {
                    this.PutAway(resolver);
                }
                else
                {
                    throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
                }
            }
            #endregion

            #region 下架
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKUP)
            {
                if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_DEFAULT)
                {
                    resolver.Input = analyzeResolver.Code;
                    this.MatchPickup(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
                {
                    this.PickUp(resolver);
                }
                else
                {
                    throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
                }
            }
            #endregion

            #region 翻箱
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_REPACK)
            {
                if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_DEFAULT)
                {
                    this.MatchRepack(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
                {
                    this.CreateRepack(resolver);
                }
                else
                {
                    throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
                }
            }
            #endregion

            #region 拆箱
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING)
            {
                if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_DEFAULT)
                {
                    this.MatchRepack(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
                {
                    this.CreateDevanning(resolver);
                }
                else
                {
                    throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
                }
            }
            #endregion

            #region 投料
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_MATERIALIN)
            {
                if (analyzeResolver.CodePrefix == BusinessConstants.BARCODE_HEAD_FLOW)
                {
                    resolver = analyzeResolver;
                    resolver.Result = languageMgr.TranslateMessage("Common.Business.Message.Flow", resolver.UserCode) + analyzeResolver.Description;
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_DEFAULT)
                {
                    resolver.Input = analyzeResolver.Code;
                    this.MatchMaterialIn(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
                {
                    this.RawMaterialIn(resolver);
                }
                else
                {
                    throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
                }

            }
            #endregion

            #region 投料回冲
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_FLUSHBACK)
            {
                if (analyzeResolver.CodePrefix == BusinessConstants.BARCODE_HEAD_FLOW)
                {
                    resolver = analyzeResolver;
                    resolver.Result = languageMgr.TranslateMessage("Common.Business.Message.Flow", resolver.UserCode) + analyzeResolver.Description;
                    FillMaterialBackflush(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
                {
                    this.RawMaterialBackflush(resolver);
                }
                else
                {
                    throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
                }
            }
            #endregion

            #region 发货退货 要货退货
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN
                || resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN)
            {
                if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_FLOW)
                {
                    resolver = analyzeResolver;
                    this.FillByFLow(resolver);
                    resolver.Result = languageMgr.TranslateMessage("Common.Business.Message.Flow", resolver.UserCode) + analyzeResolver.Description;
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_DEFAULT)
                {
                    if (resolver.Code == null || resolver.Code == string.Empty)
                    {
                        throw new BusinessErrorException("Common.Business.Error.RequireFlow");
                    }
                    resolver.Input = analyzeResolver.Code;
                    this.MatchReturn(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
                {
                    this.OrderReturn(resolver);
                }
                else
                {
                    throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
                }
            }
            #endregion

            //#region 要货退货
            //else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN)
            //{

            //}
            //#endregion

            #region 报验
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_INSPECTION)
            {
                if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_DEFAULT)
                {
                    resolver.Input = analyzeResolver.Code;
                    this.MatchInspetOrder(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
                {
                    this.CreateInspectOrder(resolver);
                }
                else
                {
                    throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
                }
            }
            #endregion

            #region 检验
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_INSPECT)
            {
                if (analyzeResolver.CodePrefix == BusinessConstants.CODE_PREFIX_INSPECTION)
                {
                    resolver = analyzeResolver;
                    this.FillInspect(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_DEFAULT)
                {
                    resolver.Input = analyzeResolver.Code;
                    this.MatchInspet(resolver);
                }
                else if (analyzeResolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
                {
                    this.Inspect(resolver);
                }
                else
                {
                    throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
                }
            }
            #endregion



            #region 盘点
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_STOCKTAKING)
            {

            }
            #endregion

            resolver.Input = string.Empty;
            return resolver;
        }

        [Transaction(TransactionMode.Unspecified)]
        public Resolver AnalyzeBarcode(string barcode, string userCode, string moduleType)
        {
            User user = userMgr.CheckAndLoadUser(userCode);
            return this.AnalyzeBarcode(barcode, user, moduleType);
        }
        [Transaction(TransactionMode.Unspecified)]
        public Resolver AnalyzeBarcode(string barcode, User user, string moduleType)
        {
            Resolver resolver = new Resolver();
            resolver.ModuleType = moduleType;
            resolver.UserCode = user.Code;

            if (barcode == null || barcode.Trim() == string.Empty)
            {
                return resolver;
            }
            //Order
            if (barcode.StartsWith(BusinessConstants.CODE_PREFIX_ORDER))
            {
                resolver.BarcodeHead = BusinessConstants.CODE_PREFIX_ORDER;
                resolver.Code = barcode;
                OrderHead orderHead = orderHeadMgr.CheckAndLoadOrderHead(resolver.Code);
                Flow flow = this.flowMgr.LoadFlow(orderHead.Flow);

                #region CopyProperty from OrderHead
                resolver.Description = flow.Description;
                resolver.NeedPrintAsn = orderHead.NeedPrintAsn;
                resolver.NeedPrintReceipt = orderHead.NeedPrintReceipt;
                resolver.AllowExceed = orderHead.AllowExceed;
                resolver.OrderType = orderHead.Type;
                resolver.AntiResolveHu = orderHead.AntiResolveHu;
                if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP)
                {
                    resolver.IsScanHu = orderHead.IsShipScanHu;
                }
                else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE)
                {
                    resolver.IsScanHu = orderHead.IsReceiptScanHu;
                }
                else
                {
                    resolver.IsScanHu = orderHead.IsShipScanHu || orderHead.IsReceiptScanHu;
                }
                #endregion

                #region 校验
                //校验权限
                if (!user.HasPermission(orderHead.PartyFrom.Code)
                 || !user.HasPermission(orderHead.PartyTo.Code))
                {
                    throw new BusinessErrorException("Common.Business.Error.NoPermission");
                }

                #region 校验状态:Status
                if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP
                    || moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE
                    || moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_OFFLINE)
                {
                    if (orderHead.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
                    {
                        throw new BusinessErrorException("Common.Business.Error.StatusError", orderHead.OrderNo, orderHead.Status);
                    }
                }
                else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_ONLINE)
                {
                    if (orderHead.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
                    {
                        throw new BusinessErrorException("Common.Business.Error.StatusError", orderHead.OrderNo, orderHead.Status);
                    }
                }
                #endregion

                #region 校验类型:Type
                if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_OFFLINE
                    || moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_ONLINE)
                {
                    if (orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                    {
                        throw new BusinessErrorException("Order.Error.OrderOfflineIsNotProduction", orderHead.OrderNo, orderHead.Type);
                    }
                }
                else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP)
                {
                    if (orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                        && orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION
                        && orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
                    {
                        throw new BusinessErrorException("Order.Error.OrderShipIsNotProduction", orderHead.OrderNo, orderHead.Type);
                    }
                }
                else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE)
                {
                    if (orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS
                    && orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                    && orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
                    {
                        throw new BusinessErrorException("Order.Error.OrderReceiveIsNotProduction", orderHead.OrderNo, orderHead.Type);
                    }
                }
                #endregion

                #endregion
            }
            //PickList
            else if (barcode.StartsWith(BusinessConstants.CODE_PREFIX_PICKLIST))
            {
                resolver.BarcodeHead = BusinessConstants.CODE_PREFIX_PICKLIST;
                resolver.Code = barcode;
                PickList pickList = pickListMgr.CheckAndLoadPickList(resolver.Code);
                resolver.PickBy = pickList.PickBy;
                //resolver.IsDetailContainHu = true;
                resolver.IsScanHu = true;//目前只有支持Hu才支持拣货
                resolver.OrderType = pickList.OrderType;

                #region 校验
                //校验权限
                if (!user.HasPermission(pickList.PartyFrom.Code)
                || !user.HasPermission(pickList.PartyTo.Code))
                {
                    throw new BusinessErrorException("Common.Business.Error.NoPermission");
                }

                if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP)
                {
                    //校验状态
                    if (pickList.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT
                        && moduleType != BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLIST)
                    {
                        throw new BusinessErrorException("Common.Business.Error.StatusError", pickList.PickListNo, pickList.Status);
                    }
                    if (pickList.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE
                        && moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLIST)
                    {
                        throw new BusinessErrorException("Common.Business.Error.StatusError", pickList.PickListNo, pickList.Status);
                    }
                }
                #endregion
            }
            //ASN
            else if (barcode.StartsWith(BusinessConstants.CODE_PREFIX_ASN))
            {
                resolver.BarcodeHead = BusinessConstants.CODE_PREFIX_ASN;
                resolver.Code = barcode;
                InProcessLocation inProcessLocation = inProcessLocationMgr.CheckAndLoadInProcessLocation(resolver.Code);
                #region CopyProperty from ASN
                //resolver.IsDetailContainHu = inProcessLocation.IsDetailContainHu;
                resolver.IsScanHu = inProcessLocation.IsReceiptScanHu;
                //resolver.NeedPrintReceipt=inProcessLocation.
                resolver.PickBy = inProcessLocation.IsDetailContainHu ? BusinessConstants.CODE_MASTER_PICKBY_HU : BusinessConstants.CODE_MASTER_PICKBY_ITEM;
                //resolver.PickBy = inProcessLocation.IsReceiptScanHu ? BusinessConstants.CODE_MASTER_PICKBY_HU : BusinessConstants.CODE_MASTER_PICKBY_ITEM;
                resolver.OrderType = inProcessLocation.OrderType;
                //resolver.AntiResolveHu = 
                #endregion

                #region 校验
                //校验权限
                if (!user.HasPermission(inProcessLocation.PartyFrom.Code)
                    || !user.HasPermission(inProcessLocation.PartyTo.Code))
                {
                    throw new BusinessErrorException("Common.Business.Error.NoPermission");
                }

                if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE)
                {
                    //校验状态
                    if (inProcessLocation.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)
                    {
                        throw new BusinessErrorException("Common.Business.Error.StatusError", inProcessLocation.IpNo, inProcessLocation.Status);
                    }
                }
                #endregion
            }

            #region 检验 Inspect
            else if (barcode.StartsWith(BusinessConstants.CODE_PREFIX_INSPECTION))
            {
                resolver.BarcodeHead = BusinessConstants.CODE_PREFIX_INSPECTION;
                resolver.Code = barcode;
                InspectOrder inspectOrder = inspectOrderMgr.CheckAndLoadInspectOrder(resolver.Code);
                resolver.IsScanHu = inspectOrder.IsDetailHasHu;
                if (resolver.IsScanHu)
                {
                    resolver.PickBy = BusinessConstants.CODE_MASTER_PICKBY_HU;
                }
                else
                {
                    resolver.PickBy = BusinessConstants.CODE_MASTER_PICKBY_ITEM;
                }
                //if (inspectOrder.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                //{
                //    throw new BusinessErrorException("InspectOrder.Error.StatusIsNotValid", inspectOrder.Status);
                //}
            }
            #endregion
            //Special,"$"
            else if (barcode.StartsWith(BusinessConstants.BARCODE_SPECIAL_MARK))
            {
                if (barcode.Length > 1)
                {
                    resolver.BarcodeHead = barcode.Substring(1, 1);
                }
                if (barcode.Length > 2)
                {
                    resolver.Code = barcode.Substring(2, barcode.Length - 2);
                }

                //Bin
                if (resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_BIN)
                {
                    if (resolver.Code == null || resolver.Code == string.Empty)
                    {
                        return resolver;
                    }
                    StorageBin storageBin = storageBinMgr.CheckAndLoadStorageBin(resolver.Code);
                    resolver.Description = storageBin.Description;
                    resolver.BinCode = resolver.Code;

                    #region 校验
                    if (!storageBin.IsActive)
                    {
                        throw new BusinessErrorException("Common.Business.Error.EntityInActive", storageBin.Code);
                    }

                    if (!user.HasPermission(storageBin.Area.Location.Region.Code))
                    {
                        throw new BusinessErrorException("Common.Business.Error.NoPermission");
                    }
                    #endregion
                }
                //Flow
                else if (resolver.CodePrefix == BusinessConstants.BARCODE_HEAD_FLOW)
                {
                    Flow flow = flowMgr.CheckAndLoadFlow(resolver.Code);
                    resolver.Description = flow.Description;
                    resolver.IsScanHu = flow.IsShipScanHu || flow.IsReceiptScanHu;
                    resolver.OrderType = flow.Type;
                    resolver.AllowCreateDetail = flow.AllowCreateDetail;

                    #region 校验
                    if (!flow.IsActive)
                    {
                        throw new BusinessErrorException("Common.Business.Error.EntityInActive", flow.Code);
                    }

                    if (!user.HasPermission(flow.PartyFrom.Code)
                    || !user.HasPermission(flow.PartyTo.Code))
                    {
                        throw new BusinessErrorException("Common.Business.Error.NoPermission");
                    }

                    if (flow.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER
                        && moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_TRANSFER)
                    {
                        throw new BusinessErrorException("Flow.Error.FlowTypeIsNotTransfer", flow.Type);
                    }
                    else if (flow.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                        && moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN)
                    {
                        throw new BusinessErrorException("Flow.ReceiveReturn.Error.FlowTypeIsNotProcurement", flow.Type);
                    }
                    else if (flow.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION
                        && moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN)
                    {
                        throw new BusinessErrorException("Flow.ShipReturn.Error.FlowTypeIsNotDistribution", flow.Type);
                    }
                    else if (flow.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION
                        && (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_FLUSHBACK ||
                        moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_MATERIALIN))
                    {
                        throw new BusinessErrorException("Flow.ShipReturn.Error.FlowTypeIsNotDistribution", flow.Type);
                    }
                    #endregion
                }
                //Command
                else if (resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK
                    || resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_CANCEL)
                {
                    resolver.Command = resolver.BarcodeHead;
                }
            }
            else
            {
                resolver.BarcodeHead = BusinessConstants.BARCODE_HEAD_DEFAULT;
                resolver.Code = barcode;
            }
            return resolver;
        }

        /// <summary>
        /// 收货/发货/拣货/检验 Hu匹配
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void MatchScan(Resolver resolver)
        {
            //参数解析
            string huId = resolver.Input;
            string pickBy = resolver.PickBy;

            if (huId == null || huId.Trim() == string.Empty)
            {
                huId = string.Empty;
            }

            //校验并修正Hu
            Hu hu = null;
            if (!(resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING))
            {
                //采购类型不检查库存
                //if (resolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                //{
                hu = huMgr.LoadHu(huId);
                //}
                //else//否则检查库存和权限
                //{
                //LocationLotDetail locationLotDetail = locationLotDetailMgr.LoadHuLocationLotDetail(string.Empty, huId, resolver.UserCode);
                //hu = locationLotDetail.Hu;
                //hu.Qty = locationLotDetail.Qty / hu.UnitQty;
                //}
            }

            #region 自动解析HuId，生成Hu
            if (hu == null
                && (resolver.AntiResolveHu == BusinessConstants.CODE_MASTER_ANTI_RESOLVE_HU_VALUE_AUTO_RESOLVE
                || resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING))
            {
                IList<int> orderLocationTransactionIdList = new List<int>();
                if (resolver.Transformers != null && resolver.Transformers.Count > 0)
                {
                    foreach (Transformer transformer in resolver.Transformers)
                    {
                        if (transformer.OrderLocTransId > 0)
                        {
                            orderLocationTransactionIdList.Add(transformer.OrderLocTransId);
                        }
                    }
                }
                hu = this.ResolveAndCreateHu(huId, orderLocationTransactionIdList);
            }
            #endregion

            if (hu == null)
            {
                throw new BusinessErrorException("Warehouse.HuMatch.NotMatch", huId);
                //string locationCode = hu.Location == null ? string.Empty : hu.Location.Code;
                //throw new BusinessErrorException("Warehouse.Hu.NotMatch.HuInformation", hu.HuId, hu.Item.Code, hu.Item.Description,
                //    hu.UnitCount.ToString("0.########"), hu.Qty.ToString("0.########"), hu.LotNo, locationCode, hu.Status);
            }
            //校验重复扫描
            if (this.CheckMatchHuScanExist(resolver))
            {
                throw new BusinessErrorException("Warehouse.Error.HuReScan", huId);
            }

            bool isMatch = false;
            if (pickBy == BusinessConstants.CODE_MASTER_PICKBY_HU)
            {
                isMatch = MatchByHu(resolver, hu);
                if (!isMatch && resolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                && resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE)//采购收货 用lot再match一次
                {
                    isMatch = MatchByLot(resolver, hu);
                }
            }
            else if (pickBy == BusinessConstants.CODE_MASTER_PICKBY_LOTNO)
            {
                isMatch = MatchByLot(resolver, hu);
            }
            else
            {
                isMatch = MatchByItem(resolver, hu);
            }
            if (!isMatch)
            {
                //throw new BusinessErrorException("Warehouse.HuMatch.NotMatch", huId);
                string locationCode = hu.Location == null && hu.Location.Trim() != string.Empty ? string.Empty : hu.Location;
                throw new BusinessErrorException("Warehouse.Hu.NotMatch.HuInformation", hu.HuId, hu.Item.Code, hu.Item.Description,
                    hu.UnitCount.ToString("0.########"), hu.Qty.ToString("0.########"), hu.LotNo, locationCode, hu.Status);
            }
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void MatchMaterialIn(Resolver resolver)
        {
            this.CheckHuInTransformerDetails(resolver);
            LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(resolver.Input, resolver.UserCode);
            Hu hu = locationLotDetail.Hu;
            hu.Qty = locationLotDetail.Qty / hu.UnitQty;
            if (resolver.Transformers == null)
            {
                resolver.Transformers = new List<Transformer>();
                resolver.Transformers.Add(new Transformer());
            }
            if (resolver.Transformers[0].TransformerDetails == null)
            {
                resolver.Transformers[0].TransformerDetails = new List<TransformerDetail>();
            }
            resolver.Transformers[0].TransformerDetails.Add(TransformerHelper.ConvertLocationLotDetailToTransformerDetail(locationLotDetail, false));
        }

        [Transaction(TransactionMode.Unspecified)]
        public void MaterialInScan(string huId, IList<MaterialIn> materialInList)
        {
            if (materialInList == null)
            {
                materialInList = new List<MaterialIn>();
            }

            LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(huId);
            Hu hu = locationLotDetail.Hu;
            hu.Qty = locationLotDetail.Qty / hu.UnitQty;
            MaterialIn materialIn = new MaterialIn();
            materialIn.HuId = huId;
            materialIn.Location = locationLotDetail.Location;
            materialIn.RawMaterial = hu.Item;
            materialIn.LotNo = hu.LotNo;
            materialIn.Qty = hu.Qty * hu.UnitQty;
            materialInList.Add(materialIn);

        }
        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public Resolver ShipOrder(Resolver resolver)
        {
            InProcessLocation ip = null;
            if (resolver.BarcodeHead == BusinessConstants.CODE_PREFIX_ORDER)
            {
                IList<InProcessLocationDetail> ipDetList = orderMgr.ConvertTransformerToInProcessLocationDetail(resolver.Transformers);
                if (ipDetList.Count > 0)
                {
                    ip = orderMgr.ShipOrder(ipDetList, resolver.UserCode);
                }
                else
                {
                    throw new BusinessErrorException("OrderDetail.Error.OrderDetailShipEmpty");
                }
            }
            else if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_PICKLIST)
            {
                ip = orderMgr.ShipOrder(resolver.Code, resolver.UserCode);
            }
            else
            {
                throw new BusinessErrorException("Common.Business.Error.EntityNotExist", resolver.Code);
            }
            resolver.Transformers = TransformerHelper.ConvertInProcessLocationDetailsToTransformers(ip.InProcessLocationDetails);
            resolver.Result = ip.IpNo;
            resolver.Code = ip.IpNo;
            return resolver;
        }

        /// <summary>
        /// 翻箱匹配
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void MatchRepack(Resolver resolver)
        {
            string huId = resolver.Input;
            if (huId == string.Empty)
            {
                throw new BusinessErrorException("Common.Business.Hu.NotExist");
            }

            //为了转成统一对象，只能写死，第一个为投入，第二个为产出
            #region 翻箱前
            if (resolver.IOType == BusinessConstants.IO_TYPE_IN)
            {

                if (resolver.Transformers != null && resolver.Transformers.Count == 2)
                {
                    Transformer inTransformer = resolver.Transformers[0];

                    if (inTransformer.TransformerDetails == null)
                    {
                        inTransformer.TransformerDetails = new List<TransformerDetail>();
                    }

                    #region 拆箱输入只能一条
                    if (resolver.ModuleType == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_DEVANNING)
                    {
                        if (inTransformer.TransformerDetails.Count > 0)
                        {
                            throw new BusinessErrorException("MasterData.Inventory.Repack.Devanning.MoreThanOneInput");
                        }
                    }
                    #endregion


                    //校验重复扫描
                    if (inTransformer.TransformerDetails != null && inTransformer.TransformerDetails.Count > 0)
                    {
                        foreach (TransformerDetail inTransformDetail in inTransformer.TransformerDetails)
                        {
                            if (inTransformDetail.HuId == huId)
                            {
                                throw new BusinessErrorException("Repack.Error.HuReScan", huId);
                            }
                        }
                    }
                    LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(huId, resolver.UserCode);
                    if (inTransformer.LocationFromCode == null || inTransformer.LocationFromCode == string.Empty)
                    {
                        inTransformer.LocationFromCode = locationLotDetail.Location.Code;
                    }
                    if (locationLotDetail.Location.Code != inTransformer.LocationFromCode)
                    {
                        throw new BusinessErrorException("Repack.Error.Location.NotEqual");
                    }
                    RepackDetail repackDetail = new RepackDetail();
                    repackDetail.LocationLotDetail = locationLotDetail;
                    repackDetail.Hu = locationLotDetail.Hu;
                    repackDetail.IOType = BusinessConstants.IO_TYPE_IN;
                    repackDetail.Qty = repackDetail.Hu.Qty * repackDetail.Hu.UnitQty;
                    inTransformer.AddTransformerDetail(TransformerHelper.ConvertRepackDetailToTransformerDetail(repackDetail));

                    if (resolver.ModuleType == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_DEVANNING)
                    {
                        resolver.AntiResolveHu = locationLotDetail.Hu.AntiResolveHu;
                        resolver.IOType = BusinessConstants.IO_TYPE_OUT;
                    }
                }
            }
            #endregion

            #region 翻箱后
            else if (resolver.IOType == BusinessConstants.IO_TYPE_OUT)
            {

                if (resolver.Transformers != null && resolver.Transformers.Count == 2)
                {
                    Transformer outTransformer = resolver.Transformers[1];
                    Transformer inTransformer = resolver.Transformers[0];


                    if (outTransformer.TransformerDetails == null)
                    {
                        outTransformer.TransformerDetails = new List<TransformerDetail>();
                    }

                    #region 拆箱必须先扫描输入
                    if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING)
                    {
                        if (inTransformer.TransformerDetails.Count == 0)
                        {
                            throw new BusinessErrorException("Devanning.Error.Input.Empty");
                        }
                    }
                    #endregion

                    #region 校验重复扫描
                    if (outTransformer.TransformerDetails != null && outTransformer.TransformerDetails.Count > 0)
                    {
                        foreach (TransformerDetail inTransformDetail in outTransformer.TransformerDetails)
                        {
                            if (inTransformDetail.HuId == huId)
                            {
                                throw new BusinessErrorException("Repack.Error.HuReScan", huId);
                            }
                        }
                    }
                    #endregion

                    #region 根据投入的反向解析hu选项来判断产出
                    Hu hu = null;
                    if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING)
                    {
                        TransformerDetail inTransformerDetail = inTransformer.TransformerDetails[0];
                        if (resolver.AntiResolveHu == BusinessConstants.CODE_MASTER_ANTI_RESOLVE_HU_VALUE_NOT_RESOLVE
                             || resolver.AntiResolveHu == null || resolver.AntiResolveHu.Trim() == string.Empty)
                        {
                            hu = huMgr.CheckAndLoadHu(huId);
                        }
                        else if (resolver.AntiResolveHu == BusinessConstants.CODE_MASTER_ANTI_RESOLVE_HU_VALUE_AUTO_RESOLVE)
                        {
                            hu = huMgr.LoadHu(huId);
                        }

                        if ((hu == null && resolver.AntiResolveHu == BusinessConstants.CODE_MASTER_ANTI_RESOLVE_HU_VALUE_AUTO_RESOLVE))
                        {
                            Hu inputHu = huMgr.LoadHu(inTransformerDetail.HuId);
                            hu = this.ResolveAndCreateHu(huId, inputHu.Uom, inputHu.ManufactureParty);
                        }
                        if (hu == null)
                        {
                            throw new BusinessErrorException("Common.Business.Hu.NotExist");
                        }
                    }
                    else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_REPACK)
                    {
                        hu = huMgr.CheckAndLoadHu(huId);
                    }
                    #endregion


                    TransformerDetail transformerDetail = TransformerHelper.ConvertHuToTransformerDetail(hu);
                    transformerDetail.IOType = resolver.IOType;

                    outTransformer.AddTransformerDetail(transformerDetail);
                }
            }
            #endregion

            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
            // return transformerList;
        }

        /// <summary>
        /// 翻箱
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public void CreateRepack(Resolver resolver)
        {
            IList<RepackDetail> repackDetailList = this.ConvertTransformerListToRepackDetail(resolver.Transformers);
            if (repackDetailList.Count == 0)
            {
                throw new BusinessErrorException("MasterData.Inventory.Repack.Error.RepackDetailEmpty");
            }
            Repack repack = repackMgr.CreateRepack(repackDetailList, this.userMgr.LoadUser(resolver.UserCode, false, true));
            resolver.Result = repack.RepackNo;

        }

        /// <summary>
        /// 拆箱
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public void CreateDevanning(Resolver resolver)
        {
            IList<RepackDetail> repackDetailList = this.ConvertTransformerListToRepackDetail(resolver.Transformers);
            if (repackDetailList.Count == 0)
            {
                throw new BusinessErrorException("MasterData.Inventory.Repack.Error.RepackDetailEmpty");
            }
            Repack repack = repackMgr.CreateDevanning(repackDetailList, this.userMgr.LoadUser(resolver.UserCode, false, true));
            resolver.Result = repack.RepackNo;

        }

        /// <summary>
        /// 报验单
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public void CreateInspectOrder(Resolver resolver)
        {
            IList<LocationLotDetail> locationLotDetailList = this.ConvertTransformerDetailToLocationLotDetail(resolver.Transformers[0].TransformerDetails, false);
            if (locationLotDetailList.Count == 0)
            {
                throw new BusinessErrorException("MasterData.Inventory.Repack.Error.RepackDetailEmpty");
            }
            User user = this.userMgr.CheckAndLoadUser(resolver.UserCode);
            InspectOrder inspectOrder = inspectOrderMgr.CreateInspectOrder(locationLotDetailList, user);
            resolver.Result = inspectOrder.InspectNo;

        }


        /// <summary>
        /// 生产单下线
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public string ReceiveWorkOrder(Resolver resolver)
        {
            Receipt receipt = new Receipt();
            foreach (Transformer transformer in resolver.Transformers)
            {
                ReceiptDetail receiptDetail = new ReceiptDetail();
                receiptDetail.OrderLocationTransaction = orderLocationTransactionMgr.LoadOrderLocationTransaction(transformer.OrderLocTransId);
                receiptDetail.HuId = null;
                receiptDetail.ReceivedQty = transformer.CurrentQty;
                receiptDetail.RejectedQty = transformer.CurrentRejectQty;
                receiptDetail.ScrapQty = transformer.ScrapQty;
                receiptDetail.Receipt = receipt;
                receipt.AddReceiptDetail(receiptDetail);
            }
            List<WorkingHours> workingHoursList = new List<WorkingHours>();
            foreach (string[] stringArray in resolver.WorkingHours)
            {
                WorkingHours workingHours = new WorkingHours();
                workingHours.Employee = employeeMgr.LoadEmployee(stringArray[0]);
                workingHours.Hours = Convert.ToDecimal(stringArray[1]);
                workingHoursList.Add(workingHours);
            }
            return orderMgr.ReceiveOrder(receipt, resolver.UserCode, workingHoursList).ReceiptNo;
        }

        /// <summary>
        /// 分步取消
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void CancelOperation(Resolver resolver)
        {
            if (TotalCurrentQty(resolver) == 0)
            {
                string userCode = resolver.UserCode;
                string ModuleType = resolver.ModuleType;
                resolver.Transformers = null;
                resolver.BinCode = null;
                resolver.Code = null;
                resolver.Description = null;
                resolver.OrderType = null;
                resolver.PickBy = null;
                resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
                resolver.ModuleType = ModuleType;
                resolver.UserCode = userCode;
                resolver.Result = languageMgr.TranslateMessage("Resolver.Cancel.BinAllCancel", resolver.UserCode);
                return;
            }

            string binCode = resolver.BinCode == null ? string.Empty : resolver.BinCode.Trim();
            if (binCode == string.Empty)
            {
                this.ClearTransformerDetail(resolver.Transformers);
                resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
                resolver.Result = languageMgr.TranslateMessage("Resolver.Cancel.BinAllCancel", resolver.UserCode);
            }
            else
            {
                //取出列表
                resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
                List<TransformerDetail> newTransformerDetailList = new List<TransformerDetail>();
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer.TransformerDetails != null)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            if (transformerDetail.CurrentQty != 0)
                            {
                                newTransformerDetailList.Add(transformerDetail);
                            }
                        }
                    }
                }
                newTransformerDetailList.Sort(this.TransformerDetailSeqComparer);

                List<TransformerDetail> transformerDetailList = new List<TransformerDetail>();
                for (int i = newTransformerDetailList.Count - 1; i >= 0; i--)
                {
                    if (newTransformerDetailList[i].StorageBinCode == binCode)
                    {
                        transformerDetailList.Add(newTransformerDetailList[i]);
                    }
                    else
                    {
                        binCode = newTransformerDetailList[i].StorageBinCode;
                        break;
                    }
                }

                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer != null)
                    {
                        for (int i = transformer.TransformerDetails.Count - 1; i >= 0; i--)
                        {
                            foreach (TransformerDetail transformerDetail in transformerDetailList)
                            {
                                if (transformer.TransformerDetails[i].HuId == transformerDetail.HuId)
                                {
                                    transformer.TransformerDetails[i].CurrentQty = 0;
                                    transformer.TransformerDetails[i].Sequence = 0;
                                    break;
                                }
                            }
                        }
                    }
                }
                resolver.Result = languageMgr.TranslateMessage("Resolver.Cancel.BinStepCancel", resolver.UserCode, resolver.BinCode, binCode);
                if (TotalCurrentQty(resolver) == 0)
                {
                    resolver.Result = languageMgr.TranslateMessage("Resolver.Cancel.BinAllCancel", resolver.UserCode);
                    resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
                }
            }
            resolver.BinCode = binCode;
            TransformerHelper.ProcessTransformer(resolver.Transformers);
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
            Receipt receipt = orderMgr.ReceiveOrder(receiptDetailList, resolver.UserCode, ip, string.Empty);
            resolver.Transformers = TransformerHelper.ConvertReceiptToTransformer(receipt.ReceiptDetails);
            resolver.Result = receipt.ReceiptNo;
            resolver.Code = receipt.ReceiptNo;
            return resolver;
        }

        /// <summary>
        /// 下架匹配
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public void MatchPickup(Resolver resolver)
        {
            this.CheckHuInTransformerDetails(resolver);
            LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(resolver.Input, resolver.Code);            //校验并修正Hu
            Hu hu = locationLotDetail.Hu;
            hu.Qty = locationLotDetail.Qty / hu.UnitQty;

            //已经下架
            if (locationLotDetail.StorageBin == null)
            {
                throw new BusinessErrorException("Warehouse.PickUp.Error.IsAlreadyPickUp", resolver.Input);
            }
            resolver.Transformers[0].TransformerDetails.Add(TransformerHelper.ConvertLocationLotDetailToTransformerDetail(locationLotDetail, false));
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }

        /// <summary>
        /// 下架
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="transformerList"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void PickUp(Resolver resolver)
        {
            if (resolver.Transformers == null || resolver.Transformers.Count != 1 || resolver.Transformers[0].TransformerDetails == null || resolver.Transformers[0].TransformerDetails.Count < 1)
            {
                throw new BusinessErrorException("PickUp.Error.HuDetailEmpty");
            }

            IList<LocationLotDetail> locationLotDetailList = this.ConvertTransformerDetailToLocationLotDetail(resolver.Transformers[0].TransformerDetails, false);
            locationMgr.InventoryPick(locationLotDetailList, resolver.UserCode);
            resolver.Result = languageMgr.TranslateMessage("Warehouse.PickUp.Successfully", resolver.UserCode);
        }

        /// <summary>
        /// 上架匹配
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void MatchPutAway(Resolver resolver)
        {
            this.CheckHuInTransformerDetails(resolver);
            //校验Bin
            StorageBin bin = storageBinMgr.CheckAndLoadStorageBin(resolver.BinCode);
            LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(resolver.Input, resolver.UserCode, bin.Area.Location.Code);
            //校验并修正Hu
            Hu hu = locationLotDetail.Hu;
            hu.Qty = locationLotDetail.Qty / hu.UnitQty;
            //已经上架
            if (locationLotDetail.StorageBin != null)
            {
                throw new BusinessErrorException("Warehouse.PutAway.Error.IsOnBin", resolver.Input, bin.Code);
            }
            locationLotDetail.NewStorageBin = bin;
            resolver.Transformers[0].TransformerDetails.Add(TransformerHelper.ConvertLocationLotDetailToTransformerDetail(locationLotDetail, true));
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }

        /// <summary>
        /// 上架
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void PutAway(Resolver resolver)
        {
            if (resolver.Transformers == null || resolver.Transformers.Count != 1 || resolver.Transformers[0].TransformerDetails == null
                || resolver.Transformers[0].TransformerDetails.Count < 1)
            {
                throw new BusinessErrorException("PutAway.Error.HuDetailEmpty");
            }

            IList<LocationLotDetail> locationLotDetailList = this.ConvertTransformerDetailToLocationLotDetail(resolver.Transformers[0].TransformerDetails, true);
            locationMgr.InventoryPut(locationLotDetailList, resolver.UserCode);
            resolver.Result = languageMgr.TranslateMessage("Warehouse.PutAway.Successfully", resolver.UserCode);
        }

        /// <summary>
        /// 移库匹配  //已废弃
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void TransferOrderScan(Resolver resolver)
        {
            if (resolver.IsScanHu && this.CheckMatchHuScanExist(resolver))
            {
                throw new BusinessErrorException("Common.Business.Error.ReScan", resolver.Input);
            }
            else if (resolver.IsScanHu && this.CheckTransformerItemExist(resolver.Input, resolver.Transformers))
            {
                throw new BusinessErrorException("Common.Business.Error.ReScan", resolver.Input);
            }
            //基于Hu移库与基本移库逻辑不同,不考虑新建明细和明细匹配等选项
            Flow flow = flowMgr.LoadFlow(resolver.Code, true);
            string locationFromCode = flow.LocationFrom != null ? flow.LocationFrom.Code : null;
            string locationToCode = flow.LocationTo != null ? flow.LocationTo.Code : null;
            if (resolver.IsScanHu)
            {
                Hu hu = huMgr.CheckAndLoadHu(resolver.Input);
                // hu = this.AmendHuQty(hu, locationFromCode);
                TransformerDetail transformerDetail = TransformerHelper.ConvertHuToTransformerDetail(hu);
                bool isExist = false;
                if (resolver.Transformers != null && resolver.Transformers.Count > 0)
                {
                    foreach (Transformer transformer in resolver.Transformers)
                    {
                        if (transformer.ItemCode.Trim().ToUpper() == transformerDetail.ItemCode.Trim().ToUpper()
                            && transformer.UomCode.Trim().ToUpper() == transformerDetail.UomCode.Trim().ToUpper()
                            && transformer.UnitCount == transformerDetail.UnitCount)
                        {
                            transformer.AddTransformerDetail(transformerDetail);
                            isExist = true;
                            break;
                        }
                    }
                }
                if (!isExist)
                {
                    Transformer transformer = TransformerHelper.ConvertTransformerDetailToTransformer(transformerDetail);
                    transformer.LocationFromCode = locationFromCode;
                    transformer.LocationToCode = locationToCode;
                    transformer.AddTransformerDetail(transformerDetail);
                    resolver.Transformers.Add(transformer);//头和明细不统一
                    resolver.Transformers = SumTransformer(resolver.Transformers);
                }
            }
            else
            {
                Item item = itemMgr.CheckAndLoadItem(resolver.Code);
                FlowDetail flowDetail = GetMatchFlowDetail(flow.FlowDetails, item.Code);
                if (flowDetail == null)
                {
                    flowDetail = GetMatchFlowDetail(flow.FlowDetails, item.Code, item.Uom.Code, item.UnitCount);
                }
                Transformer transformer = null;
                if (flowDetail == null)
                {
                    if (flow.AllowCreateDetail && orderDetailMgr.CheckOrderDet(resolver.Code, locationFromCode, flow.CheckDetailOption))
                    {
                        transformer = TransformerHelper.ConvertItemToTransformer(item);
                        transformer.LocationFromCode = flow.LocationFrom != null ? flow.LocationFrom.Code : null;
                        transformer.LocationToCode = flow.LocationTo != null ? flow.LocationTo.Code : null;
                    }
                }
                else
                {
                    transformer = TransformerHelper.ConvertFlowDetailToTransformer(flowDetail);
                }
                if (transformer == null)
                {
                    throw new BusinessErrorException("Common.Business.Error.NotMatch", resolver.Code);
                }
                resolver.Transformers.Add(transformer);
                resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
            }
        }

        /// <summary>
        /// 移库
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="transformerList"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public string TransferOrder(Resolver resolver)
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
            return orderMgr.QuickReceiveOrder(resolver.Code, orderDetailList, resolver.UserCode).ReceiptNo;
        }

        /// <summary>
        /// 获取发货明细
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void FillShip(Resolver resolver)
        {
            InProcessLocation inProcessLocation = null;
            //订单发货
            if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER)
            {
                if (resolver.Transformers != null)
                {
                    foreach (Transformer transformer in resolver.Transformers)
                    {
                        if (resolver.Input.Trim().ToUpper() == transformer.OrderNo.Trim().ToUpper())
                        {
                            throw new BusinessErrorException("Common.Business.Error.ReScan", resolver.Code);
                        }
                    }
                    if (resolver.Transformers.Count > 0)
                    {
                        //校验订单配置选项
                        this.CheckOrderConfigValid(resolver.Input, resolver.Transformers[0].OrderNo);
                    }
                }
                else
                {
                    resolver.Transformers = new List<Transformer>();
                }
                inProcessLocation = orderMgr.ConvertOrderToInProcessLocation(resolver.Input);
            }
            //拣货单发货
            else if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_PICKLIST)
            {
                inProcessLocation = orderMgr.ConvertPickListToInProcessLocation(resolver.Input);
            }
            if (inProcessLocation == null || inProcessLocation.InProcessLocationDetails == null || inProcessLocation.InProcessLocationDetails.Count == 0)
            {
                throw new BusinessErrorException("Common.Business.Error.EntityNotExist", resolver.Input);
            }
            if (resolver.IsScanHu || resolver.CodePrefix == BusinessConstants.CODE_PREFIX_PICKLIST)
            {
                OrderHelper.ClearShippedQty(inProcessLocation);//清空发货数
            }
            List<Transformer> newTransformerList = TransformerHelper.ConvertInProcessLocationDetailsToTransformers(inProcessLocation.InProcessLocationDetails);
            if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER) //基于订单发货,可以合并发货
            {
                resolver.Transformers.AddRange(newTransformerList);
            }
            else if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_PICKLIST)//Picklist不合并发货
            {
                resolver.Transformers = newTransformerList;
            }
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }

        /// <summary>
        /// 获取收货明细
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void FillReceive(Resolver resolver)
        {
            Receipt receipt = null;
            //订单收货
            if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER)
            {
                OrderHead orderHead = orderHeadMgr.LoadOrderHead(resolver.Code, true);
                if (orderHead == null || orderHead.OrderDetails == null || orderHead.OrderDetails.Count == 0)
                {
                    throw new BusinessErrorException("Common.Business.Error.EntityNotExist", resolver.Code);
                }
                receipt = orderMgr.ConvertOrderDetailToReceipt(orderHead.OrderDetails);
            }
            //ASN收货
            else if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ASN)
            {
                InProcessLocation inProcessLocation = inProcessLocationMgr.LoadInProcessLocation(resolver.Code, true);
                receipt = orderMgr.ConvertInProcessLocationToReceipt(inProcessLocation);
            }
            if (receipt == null || receipt.ReceiptDetails == null || receipt.ReceiptDetails.Count == 0)
            {
                throw new BusinessErrorException("Common.Business.Error.EntityNotExist", resolver.Code);
            }
            if (resolver.IsScanHu)// || resolver.IsDetailContainHu)
            {
                OrderHelper.ClearReceivedQty(receipt);//清空收货数
            }
            List<Transformer> newTransformerList = TransformerHelper.ConvertReceiptToTransformer(receipt.ReceiptDetails);
            resolver.Transformers = resolver.Transformers == null ? new List<Transformer>() : resolver.Transformers;
            resolver.Transformers.AddRange(newTransformerList);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }

        /// <summary>
        /// 拣货
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void PickList(Resolver resolver)
        {
            PickList pickList = pickListMgr.LoadPickList(resolver.Code, false);
            pickList.PickListDetails = new List<PickListDetail>();
            if (resolver.Transformers != null)
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    PickListDetail pickListDetail = pickListDetailMgr.LoadPickListDetail(transformer.Id, true);
                    if (transformer != null)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            PickListResult pickListResult = new PickListResult();
                            pickListResult.LocationLotDetail = locationLotDetailMgr.LoadLocationLotDetail(transformerDetail.LocationLotDetId);
                            pickListResult.PickListDetail = pickListDetail;
                            pickListResult.Qty = transformerDetail.Qty * pickListDetail.OrderLocationTransaction.UnitQty;
                            pickListDetail.AddPickListResult(pickListResult);
                        }
                    }
                    pickList.AddPickListDetail(pickListDetail);
                }
            }
            pickListMgr.DoPick(pickList, resolver.UserCode);
        }

        /// <summary>
        /// 获取下架明细
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void FillOffline(Resolver resolver)
        {
            OrderHead orderHead = orderHeadMgr.LoadOrderHead(resolver.Code, true);
            if (orderHead == null || orderHead.OrderDetails == null || orderHead.OrderDetails.Count == 0)
            {
                throw new BusinessErrorException("Common.Business.Error.EntityNotExist", resolver.Code);
            }
            Receipt receipt = orderMgr.ConvertOrderDetailToReceipt(orderHead.OrderDetails);
            if (receipt == null || receipt.ReceiptDetails == null || receipt.ReceiptDetails.Count == 0)
            {
                throw new BusinessErrorException("Common.Business.Error.EntityNotExist", resolver.Code);
            }
            OrderHelper.ClearReceivedQty(receipt);//清空收货数
            List<Transformer> newTransformerList = TransformerHelper.ConvertReceiptToTransformer(receipt.ReceiptDetails);
            resolver.Transformers.AddRange(newTransformerList);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }


        /// <summary>
        /// 检验
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void Inspect(Resolver resolver)
        {
            if (resolver.Transformers == null || resolver.Transformers.Count != 1 || resolver.Transformers[0].TransformerDetails == null
                || resolver.Transformers[0].TransformerDetails.Count < 1)
            {
                throw new BusinessErrorException("MasterData.InspectOrder.Detail.Empty");
            }

            IList<InspectOrderDetail> repackDetailList = this.ConvertResolverToInspectOrderDetails(resolver);
            inspectOrderMgr.ProcessInspectOrder(repackDetailList, resolver.UserCode);
            resolver.Result = languageMgr.TranslateMessage("MasterData.InspectOrder.Process.Successfully", resolver.Code);
        }


        public void MatchInspetOrder(Resolver resolver)
        {
            this.CheckHuInTransformerDetails(resolver);
            LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(resolver.Input, resolver.UserCode);

            if (resolver.Transformers == null)
            {
                resolver.Transformers = new List<Transformer>();
                resolver.Transformers.Add(new Transformer());
            }
            if (resolver.Transformers[0].TransformerDetails == null)
            {
                resolver.Transformers[0].TransformerDetails = new List<TransformerDetail>();
            }
            TransformerDetail transformerDetail = TransformerHelper.ConvertLocationLotDetailToTransformerDetail(locationLotDetail, false);
            transformerDetail.Qty = locationLotDetail.Hu.Qty;
            transformerDetail.UomCode = locationLotDetail.Hu.Uom.Code;
            resolver.Transformers[0].TransformerDetails.Add(transformerDetail);

        }

        public void MatchInspet(Resolver resolver)
        {
            if (resolver.IsScanHu)
            {
                //if (this.CheckMatchHuScanExist(resolver))
                //{
                //    throw new BusinessErrorException("Warehouse.Error.HuReScan", resolver.Input);
                //}
                //没有必要重新检查库存
                //LocationLotDetail locationLotDetail = locationLotDetailMgr.LoadHuLocationLotDetail(string.Empty, resolver.Input, string.Empty);
                //Hu hu = locationLotDetail.Hu;
                Hu hu = new Hu();
                hu.HuId = resolver.Input;
                if (!this.MatchByHu(resolver, hu))
                {
                    //throw new BusinessErrorException("Warehouse.HuMatch.NotMatch", resolver.Input);
                    string locationCode = hu.Location == null && hu.Location.Trim() != string.Empty ? string.Empty : hu.Location;
                    throw new BusinessErrorException("Warehouse.Hu.NotMatch.HuInformation", hu.HuId, hu.Item.Code, hu.Item.Description,
                        hu.UnitCount.ToString("0.########"), hu.Qty.ToString("0.########"), hu.LotNo, locationCode, hu.Status);
                }
            }
            else
            {
                bool isMatch = false;
                if (resolver.Transformers != null)
                {
                    foreach (Transformer transformer in resolver.Transformers)
                    {
                        if (transformer.TransformerDetails != null && transformer.TransformerDetails.Count == 1)
                        {
                            foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                            {

                                if (transformerDetail.CurrentQty + transformerDetail.CurrentRejectQty > transformerDetail.Qty)
                                {
                                    transformerDetail.CurrentQty = 0;
                                    transformerDetail.CurrentRejectQty = 0;
                                    throw new BusinessErrorException("Warehouse.Inspect.ItemMatch.QtyExcceed", transformerDetail.ItemCode);
                                }
                                else
                                {
                                    transformerDetail.CurrentQty = transformerDetail.Qty;
                                    transformerDetail.CurrentRejectQty = 0;
                                }
                                if (transformerDetail.ItemCode.ToLower() == resolver.Input.ToLower().Trim())
                                {
                                    isMatch = true;
                                }
                            }
                        }
                    }
                }
                if (!isMatch)
                {
                    throw new BusinessErrorException("Warehouse.ItemMatch.NotMatch", resolver.Input);
                }
            }
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }
        #endregion














        #region Private Method

        /// <summary>
        /// 上架,下架,投料transformerDetails处理
        /// </summary>
        /// <param name="resolver"></param>
        private void CheckHuInTransformerDetails(Resolver resolver)
        {
            if (resolver.Transformers == null || resolver.Transformers.Count == 0)
            {
                resolver.Transformers = new List<Transformer>();
                resolver.Transformers.Add(new Transformer());
                resolver.Transformers[0].TransformerDetails = new List<TransformerDetail>();
            }
            if (resolver.Input == null || resolver.Input.Trim() == string.Empty)
            {
                resolver.Input = string.Empty;
            }//初始化默认值
            if (resolver.BinCode == null || resolver.BinCode.Trim() == string.Empty)
            {
                resolver.BinCode = string.Empty;
            }
            //是否重新扫描

            if (CheckMatchHuScanExist(resolver))
            {
                throw new BusinessErrorException("Warehouse.Error.HuReScan", resolver.Input);
            }

        }

        private bool CheckMatchHuScanExist(Resolver resolver)
        {
            foreach (Transformer transformer in resolver.Transformers)
            {
                if (transformer.TransformerDetails != null && transformer.TransformerDetails.Count > 0)
                {
                    foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                    {
                        if (transformerDetail.HuId != null
                            && transformerDetail.HuId.Trim().ToUpper() == resolver.Input.Trim().ToUpper()
                            && transformerDetail.CurrentQty != 0)
                            return true;
                    }
                }
            }
            return false;
        }

        private bool CheckTransformerOrderExist(Resolver resolver)
        {
            foreach (Transformer transformer in resolver.Transformers)
            {
                if (resolver.Code.Trim().ToUpper() == transformer.OrderNo.Trim().ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckTransformerItemExist(string itemCode, List<Transformer> transformerList)
        {
            foreach (Transformer transformer in transformerList)
            {
                if (itemCode.Trim().ToUpper() == transformer.ItemCode.Trim().ToUpper())
                {
                    return true;
                }
            }
            return false;
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

        private FlowDetail GetMatchFlowDetail(IList<FlowDetail> flowDetailList, string itemCode)
        {
            FlowDetail fd = null;
            if (flowDetailList != null && flowDetailList.Count > 0)
            {
                foreach (FlowDetail flowDetail in flowDetailList)
                {
                    if (flowDetail.Item.Code.Trim().ToUpper() == itemCode.Trim().ToUpper())
                    {
                        if (fd == null)
                            fd = flowDetail;
                        else
                            return null;
                    }
                }
            }
            return fd;
        }
        private FlowDetail GetMatchFlowDetail(IList<FlowDetail> flowDetailList, string itemCode, string UomCode, decimal UnitCount)
        {
            if (flowDetailList != null && flowDetailList.Count > 0)
            {
                foreach (FlowDetail flowDetail in flowDetailList)
                {
                    if (flowDetail.Item.Code.Trim().ToUpper() == itemCode.Trim().ToUpper()
                        && flowDetail.UnitCount == UnitCount
                        && flowDetail.Uom.Code.Trim().ToUpper() == UomCode.Trim().ToUpper())
                    {
                        return flowDetail;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 重新统计Transformer的箱数和总数
        /// </summary>
        /// <param name="transformerList"></param>
        /// <returns></returns>
        private List<Transformer> SumTransformer(List<Transformer> transformerList)
        {
            foreach (Transformer transformer in transformerList)
            {
                transformer.CurrentQty = 0;
                transformer.Cartons = 0;
                transformer.Qty = 0;
                foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                {
                    transformer.CurrentQty += transformerDetail.CurrentQty;
                    transformer.Qty += transformerDetail.Qty;
                    if (transformerDetail.CurrentQty != 0)
                    {
                        transformer.Cartons++;
                    }
                }

            }
            return transformerList;
        }

        //[Transaction(TransactionMode.Unspecified)]
        private IList<RepackDetail> ConvertTransformerListToRepackDetail(IList<Transformer> transformerList)
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
                            if (transformerDetail.LocationLotDetId != null && transformerDetail.LocationLotDetId != 0)
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
        /// 把Resolver转成RepackDetail
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        private IList<InspectOrderDetail> ConvertResolverToInspectOrderDetails(Resolver resolver)
        {
            IList<InspectOrderDetail> inspectDetailList = new List<InspectOrderDetail>();

            if (resolver != null && resolver.Transformers != null)
            {
                foreach (TransformerDetail transformerDetail in resolver.Transformers[0].TransformerDetails)
                {
                    InspectOrderDetail inspectDetail = inspectOrderDetailMgr.LoadInspectOrderDetail(transformerDetail.Id);
                    inspectDetail.CurrentQualifiedQty = transformerDetail.CurrentQty;
                    //if (resolver.IsScanHu)
                    //{
                    //    inspectDetail.CurrentRejectedQty = transformerDetail.Qty - transformerDetail.CurrentQty;
                    //}
                    //else
                    //{
                    inspectDetail.CurrentRejectedQty = transformerDetail.CurrentRejectQty;
                    //}
                    if (inspectDetail.CurrentQualifiedQty != 0 || inspectDetail.CurrentRejectedQty != 0)
                    {
                        inspectDetailList.Add(inspectDetail);
                    }
                }
            }
            return inspectDetailList;
        }

        /// <summary>
        /// 根据LocationLotDetail修正Hu数量
        /// </summary>
        /// <param name="hu"></param>
        /// <param name="locationCode"></param>
        /// <returns></returns>
        //private Hu AmendHuQty(Hu hu, string locationCode)
        //{
        //    LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(locationCode, hu.HuId);
        //    hu.Qty = locationLotDetail.Qty / hu.UnitQty;//修正数量,单位换算
        //    return hu;
        //}

        private object[] ResolveBarCode(string barCode)
        {
            try
            {
                string[] splitedBarcode = BarcodeHelper.SplitBarcode(barCode);

                Item item = null;
                string supMark = null;
                string lotNo = null;
                decimal qty = 0;
                int seq = 0;
                DateTime manufactureDate = DateTime.Now;

                item = this.itemMgr.CheckAndLoadItem(splitedBarcode[0]);
                supMark = splitedBarcode[1];
                lotNo = splitedBarcode[2];
                qty = decimal.Parse(splitedBarcode[3]);
                seq = int.Parse(splitedBarcode[4]);
                manufactureDate = LotNoHelper.ResolveLotNo(lotNo);

                object[] result = new object[6];
                result[0] = item;
                result[1] = supMark;
                result[2] = lotNo;
                result[3] = qty;
                result[4] = seq;
                result[5] = manufactureDate;

                return result;
            }
            catch (BusinessErrorException ex)
            {
                throw new BusinessErrorException("Hu.Error.HuIdNotExist", ex, barCode);
            }
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

        private int TransformerDetailSeqComparer(TransformerDetail x, TransformerDetail y)
        {
            return x.Sequence - y.Sequence;
        }

        private int FindMaxSeq(List<Transformer> transformerList)
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
                            if (transformerDetail.Sequence > maxSeq)
                            {
                                maxSeq = transformerDetail.Sequence;
                            }
                        }
                    }
                }
            }
            return maxSeq;
        }

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

        /// <summary>
        /// 按Hu匹配
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns>Resolver</returns>
        private bool MatchByHu(Resolver resolver, Hu hu)
        {
            bool isMatch = false;
            if (resolver.Transformers != null)
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer.TransformerDetails != null && transformer.TransformerDetails.Count > 0)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            if (transformerDetail.HuId.ToUpper() == hu.HuId.ToUpper())
                            {
                                transformerDetail.StorageBinCode = resolver.BinCode;
                                transformerDetail.CurrentQty = transformerDetail.Qty;
                                transformerDetail.Sequence = this.FindMaxSeq(resolver.Transformers) + 1;
                                isMatch = true;
                                break;
                            }
                        }

                        if (isMatch)
                        {
                            TransformerHelper.ProcessTransformer(transformer);
                            break;
                        }
                    }
                }
            }
            return isMatch;
        }

        /// <summary>
        /// 按LotNo匹配
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="hu"></param>
        /// <returns></returns>
        private bool MatchByLot(Resolver resolver, Hu hu)
        {
            bool isMatch = false;
            if (resolver.Transformers != null)
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer.TransformerDetails != null && transformer.TransformerDetails.Count > 0)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            if (transformerDetail.LotNo.ToUpper() == hu.LotNo.ToUpper())
                            {
                                transformerDetail.StorageBinCode = resolver.BinCode;
                                transformerDetail.CurrentQty = transformerDetail.Qty;
                                transformerDetail.Sequence = this.FindMaxSeq(resolver.Transformers) + 1;
                                isMatch = true;
                                break;
                            }
                        }

                        if (isMatch)
                        {
                            TransformerHelper.ProcessTransformer(transformer);
                            break;
                        }
                    }
                }
            }
            return isMatch;
        }

        /// <summary>
        /// 缺省匹配规则,按Item匹配
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="hu"></param>
        /// <returns></returns>
        private bool MatchByItem(Resolver resolver, Hu hu)
        {
            bool isMatch = false;
            if (resolver.Transformers != null)
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer.TransformerDetails != null)
                    {
                        if (transformer.ItemCode == hu.Item.Code
                           && transformer.UomCode == hu.Uom.Code
                           && transformer.UnitCount == hu.UnitCount)
                        {
                            if (transformer.Qty - transformer.CurrentQty >= hu.Qty
                                || resolver.CodePrefix == BusinessConstants.BARCODE_HEAD_FLOW)
                            {
                                //if (transformer.LocationFrom != null && transformer.LocationFrom.Trim() != string.Empty)
                                //{
                                //    if (!(resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN
                                //        || resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE))
                                //    {
                                //        hu = this.AmendHuQty(hu, transformer.LocationFrom);
                                //    }
                                //}
                                TransformerDetail transformerDetail = TransformerHelper.ConvertHuToTransformerDetail(hu);
                                transformerDetail.StorageBinCode = resolver.BinCode;
                                transformerDetail.Sequence = this.FindMaxSeq(resolver.Transformers) + 1;
                                transformer.AddTransformerDetail(transformerDetail);
                                TransformerHelper.ProcessTransformer(transformer);
                                //如果是flow,改变订单数
                                if (resolver.CodePrefix == BusinessConstants.BARCODE_HEAD_FLOW)
                                {
                                    transformer.Qty = transformer.CurrentQty;
                                }
                                isMatch = true;
                                break;
                            }
                        }
                    }
                }
            }
            return isMatch;
        }

        /// <summary>
        /// 根据Flow检查库存中的Hu,存在返回Hu,否则返回null
        /// 如果时退货(isNegative为true),检查目的库位库存;否则检查来源库位库存
        /// </summary>
        /// <param name="huId"></param>
        /// <param name="flowCode"></param>
        /// <param name="userCode"></param>
        /// <param name="isNegative"></param>
        /// <returns></returns>
        private Hu CheckHuLocationLotDetailExist(string huId, string flowCode, string userCode, bool isNegative)
        {
            LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(huId, userCode);

            Hu hu = locationLotDetail.Hu;
            string locationCode = locationLotDetail.Location.Code;
            Flow flow = flowMgr.LoadFlow(flowCode, true);
            if (flow == null)
            {
                return hu;
            }
            Location flowLocation = isNegative ? flow.LocationTo : flow.LocationFrom;
            if (flowLocation == null)
            {
                return hu;
            }
            else
            {
                if (!flow.AllowCreateDetail)
                {
                    foreach (FlowDetail flowDetail in flow.FlowDetails)
                    {
                        if (flowDetail.Item.Code == hu.Item.Code
                            && flowDetail.Item.UnitCount == hu.Item.UnitCount
                            && flowDetail.Item.Uom.Code == hu.Item.Uom.Code)
                        {
                            Location flowDetailLocation = isNegative ? flowDetail.LocationTo : flowDetail.LocationFrom;
                            flowDetailLocation = flowDetailLocation == null ? flowLocation : flowDetailLocation;
                            if (flowDetailLocation.Code == locationCode)
                            {
                                return hu;
                            }
                        }
                    }
                }
                else
                {
                    if (flowLocation.Code == locationCode)
                    {
                        return hu;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transformerDetailList"></param>
        /// <param name="isPutAway"></param>
        /// <returns></returns>
        private IList<LocationLotDetail> ConvertTransformerDetailToLocationLotDetail(List<TransformerDetail> transformerDetailList, bool isPutAway)
        {
            IList<LocationLotDetail> locationLotDetailList = new List<LocationLotDetail>();
            if (transformerDetailList != null && transformerDetailList.Count > 0)
            {
                foreach (TransformerDetail transformerDetail in transformerDetailList)
                {
                    locationLotDetailList.Add(this.ConvertTransformerDetailToLocationLotDetail(transformerDetail, isPutAway));
                }
            }

            return locationLotDetailList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transformerDetail"></param>
        /// <param name="isPutAway"></param>
        /// <returns></returns>
        private LocationLotDetail ConvertTransformerDetailToLocationLotDetail(TransformerDetail transformerDetail, bool isPutAway)
        {
            LocationLotDetail locationLotDetail = locationLotDetailMgr.LoadLocationLotDetail(transformerDetail.Id);
            locationLotDetail.CurrentInspectQty = locationLotDetail.Qty;
            if (isPutAway && transformerDetail.StorageBinCode != null && transformerDetail.StorageBinCode.Trim() != string.Empty)
            {
                locationLotDetail.NewStorageBin = storageBinMgr.CheckAndLoadStorageBin(transformerDetail.StorageBinCode);
            }

            return locationLotDetail;
        }


        private IList<MaterialIn> ConvertTransformerDetailListToMaterialIn(List<TransformerDetail> transformerDetailList)
        {
            IList<MaterialIn> materialInList = new List<MaterialIn>();
            if (transformerDetailList != null && transformerDetailList.Count > 0)
            {
                foreach (TransformerDetail transformerDetail in transformerDetailList)
                {
                    materialInList.Add(this.ConvertTransformerDetailToMaterialIn(transformerDetail));
                }
            }

            return materialInList;
        }

        private MaterialIn ConvertTransformerDetailToMaterialIn(TransformerDetail transformerDetail)
        {
            MaterialIn materialIn = new MaterialIn();
            materialIn.HuId = transformerDetail.HuId;
            materialIn.RawMaterial = itemMgr.LoadItem(transformerDetail.ItemCode);
            materialIn.Location = locationMgr.LoadLocation(transformerDetail.LocationCode);
            materialIn.LotNo = transformerDetail.LotNo;
            materialIn.Operation = transformerDetail.Operation;
            materialIn.Qty = transformerDetail.Qty;

            return materialIn;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void RawMaterialIn(Resolver resolver)
        {

            IList<MaterialIn> materialInList = ConvertTransformerDetailListToMaterialIn(resolver.Transformers[0].TransformerDetails);
            productLineInProcessLocationDetailMgr.RawMaterialIn(resolver.Code, materialInList, userMgr.CheckAndLoadUser(resolver.UserCode));
            resolver.Transformers = new List<Transformer>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="orderLocationTransactionIdList"></param>
        /// <returns></returns>
        private Hu ResolveAndCreateHu(string barCode, IList<int> orderLocationTransactionIdList)
        {
            object[] result = ResolveBarCode(barCode);
            Item item = (Item)result[0];
            string supMark = (string)result[1];
            string lotNo = (string)result[2];
            decimal qty = (decimal)result[3];
            int seq = (int)result[4];
            DateTime manufactureDate = (DateTime)result[5];

            foreach (int orderLocationTransactionId in orderLocationTransactionIdList)
            {
                OrderLocationTransaction orderLocationTransaction = this.orderLocationTransactionMgr.LoadOrderLocationTransaction(orderLocationTransactionId);
                OrderDetail orderDetail = orderLocationTransaction.OrderDetail;

                //对于一条flow上同一个Item出现有多次，使用第一个。
                if (item.Code == orderDetail.Item.Code
                    //&& (supMark == orderDetail.IdMark || (supMark == BusinessConstants.DEFAULT_SUPPLIER_ID_MARK
                    //&& (orderDetail.IdMark == null || orderDetail.IdMark.Trim() == string.Empty)))
                    )
                {
                    Hu hu = new Hu();
                    hu.HuId = barCode;
                    hu.Item = item;
                    hu.Uom = orderDetail.Uom;   //用Flow单位
                    #region 单位用量
                    if (item.Uom.Code != orderDetail.Uom.Code)
                    {
                        hu.UnitQty = this.uomConversionMgr.ConvertUomQty(item, orderDetail.Uom, 1, item.Uom);   //单位用量
                    }
                    else
                    {
                        hu.UnitQty = 1;
                    }
                    #endregion
                    hu.QualityLevel = BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1;
                    hu.Qty = qty / hu.UnitQty;
                    hu.UnitCount = orderDetail.UnitCount;
                    hu.LotNo = lotNo;
                    hu.ManufactureDate = manufactureDate;
                    hu.ManufactureParty = orderDetail.OrderHead.PartyFrom;
                    hu.CreateUser = this.userMgr.GetMonitorUser();
                    hu.CreateDate = DateTime.Now;
                    hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CREATE;
                    hu.LotSize = orderDetail.HuLotSize.HasValue ? orderDetail.HuLotSize.Value : hu.Qty;

                    this.huMgr.CreateHu(hu);
                    this.numberControlMgr.ReverseUpdateHuId(barCode);

                    return hu;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        private Hu ResolveAndCreateHu(string barCode, string orderNo)
        {
            object[] result = ResolveBarCode(barCode);
            Item item = (Item)result[0];
            string supMark = (string)result[1];
            string lotNo = (string)result[2];
            decimal qty = (decimal)result[3];
            int seq = (int)result[4];
            DateTime manufactureDate = (DateTime)result[5];

            IList<OrderDetail> orderDetailList = this.orderDetailMgr.GetOrderDetail(orderNo);
            foreach (OrderDetail orderDetail in orderDetailList)
            {
                //对于一条flow上同一个Item出现有多次，使用第一个。
                if (item.Code == orderDetail.Item.Code
                    //&& (supMark == orderDetail.IdMark || (supMark == BusinessConstants.DEFAULT_SUPPLIER_ID_MARK
                    //&& (orderDetail.IdMark == null || orderDetail.IdMark.Trim() == string.Empty)))
                    )
                {
                    Hu hu = new Hu();
                    hu.HuId = barCode;
                    hu.Item = item;
                    hu.Uom = orderDetail.Uom;   //用Flow单位
                    #region 单位用量
                    if (item.Uom.Code != orderDetail.Uom.Code)
                    {
                        hu.UnitQty = this.uomConversionMgr.ConvertUomQty(item, orderDetail.Uom, 1, item.Uom);   //单位用量
                    }
                    else
                    {
                        hu.UnitQty = 1;
                    }
                    #endregion
                    hu.QualityLevel = BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1;
                    hu.Qty = qty / hu.UnitQty;
                    hu.UnitCount = orderDetail.UnitCount;
                    hu.LotNo = lotNo;
                    hu.ManufactureDate = manufactureDate;
                    hu.ManufactureParty = orderDetail.OrderHead.PartyFrom;
                    hu.CreateUser = this.userMgr.GetMonitorUser();
                    hu.CreateDate = DateTime.Now;
                    hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CREATE;
                    hu.LotSize = orderDetail.HuLotSize.HasValue ? orderDetail.HuLotSize.Value : hu.Qty;

                    this.huMgr.CreateHu(hu);
                    this.numberControlMgr.ReverseUpdateHuId(barCode);

                    return hu;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="uom"></param>
        /// <param name="manufactureParty"></param>
        /// <returns></returns>
        private Hu ResolveAndCreateHu(string barCode, Uom uom, Party manufactureParty)
        {
            object[] result = ResolveBarCode(barCode);
            Item item = (Item)result[0];
            string supMark = (string)result[1];
            string lotNo = (string)result[2];
            decimal qty = (decimal)result[3];
            int seq = (int)result[4];
            DateTime manufactureDate = (DateTime)result[5];

            Hu hu = new Hu();
            hu.HuId = barCode;
            hu.Item = item;
            hu.Uom = uom;
            #region 单位用量
            if (item.Uom.Code != uom.Code)
            {
                hu.UnitQty = this.uomConversionMgr.ConvertUomQty(item, uom, 1, item.Uom);   //单位用量
            }
            else
            {
                hu.UnitQty = 1;
            }
            #endregion
            hu.QualityLevel = BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1;
            hu.Qty = qty / hu.UnitQty;
            hu.UnitCount = qty;
            hu.LotNo = lotNo;
            hu.ManufactureDate = manufactureDate;
            hu.ManufactureParty = manufactureParty;
            hu.CreateUser = this.userMgr.GetMonitorUser();
            hu.CreateDate = DateTime.Now;
            hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CREATE;
            hu.LotSize = hu.Qty;

            this.huMgr.CreateHu(hu);
            this.numberControlMgr.ReverseUpdateHuId(barCode);

            return hu;
        }

        /// <summary>
        /// 初始化上架 Transformers和TransformerDetails
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        private List<Transformer> FillPutAway(Resolver resolver)
        {
            List<Transformer> transformerList = resolver.Transformers;
            if (transformerList == null)
            {
                transformerList = new List<Transformer>();
                transformerList.Add(new Transformer());
                transformerList[0].TransformerDetails = new List<TransformerDetail>();
            }
            return transformerList;
        }

        /// <summary>
        /// 初始化检验 Transformers和TransformerDetails
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        private void FillInspect(Resolver resolver)
        {
            InspectOrder inspectOrder = inspectOrderMgr.LoadInspectOrder(resolver.Code, true);
            Transformer transformer = TransformerHelper.ConvertInspectOrderToTransformer(inspectOrder);
            resolver.AddTransformer(transformer);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
            resolver.Result = languageMgr.TranslateMessage("MasterData.Inventory.CurrentInspectOrder", resolver.UserCode, resolver.Code);
        }

        /// <summary>
        /// 根据Flow填充Transformers
        /// </summary>
        /// <param name="resolver"></param>
        private void FillByFLow(Resolver resolver)
        {
            //bool isOrderReturn = false;
            //if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN
            //    || resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN)
            //{
            //    isOrderReturn = true;
            //}
            Flow flow = flowMgr.CheckAndLoadFlow(resolver.Code, true);
            foreach (FlowDetail flowDetail in flow.FlowDetails)
            {
                flowDetail.LocationFrom = flowDetail.LocationFrom == null ? flow.LocationFrom : flowDetail.LocationFrom;
                flowDetail.LocationTo = flowDetail.LocationTo == null ? flow.LocationTo : flowDetail.LocationTo;
                //if (isOrderReturn)
                //{
                //    Location tempLocation = flowDetail.LocationFrom;
                //    flowDetail.LocationFrom = flowDetail.LocationTo;
                //    flowDetail.LocationTo = tempLocation;
                //}
            }
            resolver.Transformers = TransformerHelper.ConvertFlowDetailsToTransformers(flow.FlowDetails);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }

        private void MatchReturn(Resolver resolver)
        {
            Hu hu = null;
            if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN)
            {
                LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(resolver.Input, resolver.UserCode);
                hu = locationLotDetail.Hu;
            }
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN)
            {
                hu = huMgr.CheckAndLoadHu(resolver.Input);
            }

            //校验重复扫描
            if (this.CheckMatchHuScanExist(resolver))
            {
                throw new BusinessErrorException("Warehouse.Error.HuReScan", resolver.Input);
            }
            if (!MatchByItem(resolver, hu))
            {
                //throw new BusinessErrorException("Warehouse.HuMatch.NotMatch", resolver.Input);
                string locationCode = hu.Location == null && hu.Location.Trim() == string.Empty ? string.Empty : hu.Location;
                throw new BusinessErrorException("Warehouse.Hu.NotMatch.HuInformation", hu.HuId, hu.Item.Code, hu.Item.Description,
                    hu.UnitCount.ToString("0.########"), hu.Qty.ToString("0.########"), hu.LotNo, locationCode, hu.Status);
            }
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }

        private void OrderReturn(Resolver resolver)
        {
            Flow flow = this.flowMgr.CheckAndLoadFlow(resolver.Code, true);
            User user = this.userMgr.CheckAndLoadUser(resolver.UserCode);
            string orderSubType = BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN;
            DateTime winTime = DateTime.Now;
            DateTime startTime = DateTime.Now;
            IList<OrderDetail> orderDetails = ConvertResolverToOrderDetails(resolver, flow);

            Receipt receipt = orderMgr.QuickReceiveOrder(flow, orderDetails, user, orderSubType, winTime, startTime, false, null, null);
        }

        /// <summary>
        /// todo:未考虑生产调整
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="flow"></param>
        /// <returns></returns>
        private IList<OrderDetail> ConvertResolverToOrderDetails(Resolver resolver, Flow flow)
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
                        OrderDetail newOrderDetail = new OrderDetail();
                        newOrderDetail.IsScanHu = true;
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
                        if (!(resolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT))
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
                        orderDetails.Add(newOrderDetail);
                    }
                }
            }
            return orderDetails;
        }

        private void FillMaterialBackflush(Resolver resolver)
        {
            IList<ProductLineInProcessLocationDetail> productLineIpList
                = productLineInProcessLocationDetailMgr.GetProductLineInProcessLocationDetailGroupByItem(resolver.Code, BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE);

            List<Transformer> transformerList = new List<Transformer>();
            transformerList.Add(new Transformer());
            transformerList[0].TransformerDetails =
                Utility.TransformerHelper.ConvertProductLineInProcessLocationDetailsToTransformerDetails(productLineIpList);
            resolver.Transformers = transformerList;
        }



        private void RawMaterialBackflush(Resolver resolver)
        {
            if (resolver.Transformers == null || resolver.Transformers.Count != 1 || resolver.Transformers[0].TransformerDetails == null
                || resolver.Transformers[0].TransformerDetails.Count < 1)
            {
                throw new BusinessErrorException("MaterialBackflush.Error.HuDetailEmpty");
            }
            User user = userMgr.LoadUser(resolver.UserCode);
            IDictionary<string, decimal> itemDictionary = new Dictionary<string, decimal>();
            foreach (TransformerDetail transformerDetail in resolver.Transformers[0].TransformerDetails)
            {
                itemDictionary.Add(transformerDetail.ItemCode, transformerDetail.CurrentQty);
            }
            productLineInProcessLocationDetailMgr.RawMaterialBackflush(resolver.Code, itemDictionary, user);
        }
        #endregion
    }
}
