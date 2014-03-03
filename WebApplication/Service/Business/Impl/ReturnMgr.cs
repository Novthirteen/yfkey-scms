using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.View;
using System.Collections.Generic;

namespace com.Sconit.Service.Business.Impl
{
    public class ReturnMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private IHuMgr huMgr;
        private IFlowMgr flowMgr;
        private IStorageBinMgr storageBinMgr;
        private ILocationMgr locationMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;

        public ReturnMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            IHuMgr huMgr,
            IFlowMgr flowMgr,
            IStorageBinMgr storageBinMgr,
            ILocationMgr locationMgr,
            ILocationLotDetailMgr locationLotDetailMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.huMgr = huMgr;
            this.flowMgr = flowMgr;
            this.storageBinMgr = storageBinMgr;
            this.locationMgr = locationMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {
            if (resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_BIN)
            {
                setBaseMgr.FillResolverByBin(resolver);
            }
            else if (resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_LOCATION)
            {
                setBaseMgr.FillResolverByLocation(resolver);
            }
            else if (resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_FLOW)
            {
                setBaseMgr.FillResolverByFlow(resolver);
                if (resolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                    && resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN)
                {
                    throw new BusinessErrorException("Flow.ShipReturn.Error.FlowTypeIsProcurement", resolver.OrderType);
                }
                if (resolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION
                    && resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN)
                {
                    throw new BusinessErrorException("Flow.ReceiveReturn.Error.FlowTypeIsDistribution", resolver.OrderType);
                }
            }
            else
            {
                throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
            }
        }

        protected override void GetDetail(Resolver resolver)
        {
            //setBaseMgr.FillDetailByFlow(resolver);
        }

        protected override void SetDetail(Resolver resolver)
        {
            List<string> flowTypes = new List<string>();
            flowTypes.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER);
            flowTypes.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT);
            flowTypes.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION);
            flowTypes.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS);
            Hu hu = huMgr.CheckAndLoadHu(resolver.Input);
            if (this.locationMgr.IsHuOcuppyByPickList(resolver.Input))
            {
                throw new BusinessErrorException("Order.Error.PickUp.HuOcuppied", resolver.Input);
            }

            FlowView flowView = null;
            //移库路线类型退货(退库)可以跟据库格和库位找出相对应的移库路线
            if (resolver.CodePrefix == null || resolver.CodePrefix.Trim() == string.Empty)
            {
                if (resolver.BinCode == null || resolver.BinCode.Trim() == string.Empty)
                {
                    throw new BusinessErrorException("Common.Business.Error.ScanFlowOrStorageBinFirst");
                }
                if (resolver.LocationFormCode == null || resolver.LocationFormCode.Trim() == string.Empty)
                {
                    throw new BusinessErrorException("Common.Business.Error.ScanFlowOrLocationFirst");
                }
                if (hu.Location != null)
                {
                    if (hu.Location != resolver.LocationFormCode)
                    {
                        throw new BusinessErrorException("Common.Business.Error.HuNoInventory", resolver.LocationFormCode, hu.HuId);
                    }
                }
                //确定flow和flowView
                List<string> transferType = new List<string>();
                transferType.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER);
                flowView = flowMgr.CheckAndLoadFlowView(null, resolver.UserCode, resolver.LocationToCode, resolver.LocationFormCode, hu, transferType);
                setBaseMgr.FillResolverByFlow(resolver, flowView.Flow);
                resolver.Result = resolver.LocationFormCode + " => " + resolver.LocationToCode;
            }

            //已经确定了Flow,匹配新的Hu
            if (resolver.CodePrefix != null && resolver.CodePrefix.Trim() != string.Empty)
            {
                flowView = flowMgr.CheckAndLoadFlowView(resolver.Code, null, null, null, hu, null);
                //退库检查库存
                if ((resolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER
                     || resolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                     || resolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS)
                    && flowView.Flow.IsReceiptScanHu
                    )
                {
                    LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(hu.HuId);
                    hu.Qty = locationLotDetail.Qty / hu.UnitQty;
                }
            }
            else
            {
                throw new BusinessErrorException("Common.Business.Error.ScanFlowFirst");
            }
            setDetailMgr.MatchHuByFlowView(resolver, flowView, hu);
        }

        protected override void ExecuteSubmit(Resolver resolver)
        {
            executeMgr.OrderReturn(resolver);
        }

        protected override void ExecuteCancel(Resolver resolver)
        {
            executeMgr.CancelOperation(resolver);
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
