using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Exception;
using Castle.Services.Transaction;
using com.Sconit.Entity.Distribution;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity;
using com.Sconit.Utility;

namespace com.Sconit.Service.Business.Impl
{
    public class SetBaseMgr : ISetBaseMgr
    {
        private IUserMgr userMgr;
        private IOrderHeadMgr orderHeadMgr;
        private IPickListMgr pickListMgr;
        private IStorageBinMgr storageBinMgr;
        private ILanguageMgr languageMgr;
        private IInProcessLocationMgr inProcessLocationMgr;
        private IFlowMgr flowMgr;
        private ILocationMgr locationMgr;

        public SetBaseMgr(
            IUserMgr userMgr,
            IOrderHeadMgr orderHeadMgr,
            IPickListMgr pickListMgr,
            IStorageBinMgr storageBinMgr,
            ILanguageMgr languageMgr,
            IInProcessLocationMgr inProcessLocationMgr,
            IFlowMgr flowMgr,
            ILocationMgr locationMgr
            )
        {
            this.userMgr = userMgr;
            this.orderHeadMgr = orderHeadMgr;
            this.pickListMgr = pickListMgr;
            this.storageBinMgr = storageBinMgr;
            this.languageMgr = languageMgr;
            this.inProcessLocationMgr = inProcessLocationMgr;
            this.flowMgr = flowMgr;
            this.locationMgr = locationMgr;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void FillResolverByOrder(Resolver resolver)
        {
            User user = userMgr.CheckAndLoadUser(resolver.UserCode);
            OrderHead orderHead = orderHeadMgr.CheckAndLoadOrderHead(resolver.Input);
            Flow flow = this.flowMgr.LoadFlow(orderHead.Flow);

            //if (!user.HasPermission(orderHead.PartyFrom.Code) ||
            //    !user.HasPermission(orderHead.PartyTo.Code))
            //{
            //    throw new BusinessErrorException("Common.Business.Error.NoPermission");
            //}
            #region CopyProperty from OrderHead
            resolver.Code = orderHead.OrderNo;
            resolver.Description = flow.Description;
            resolver.Status = orderHead.Status;
            resolver.OrderType = orderHead.Type;
            resolver.NeedPrintAsn = orderHead.NeedPrintAsn;
            resolver.NeedPrintReceipt = orderHead.NeedPrintReceipt;
            resolver.AutoPrintHu = orderHead.AutoPrintHu && (orderHead.CreateHuOption != BusinessConstants.CODE_MASTER_CREATE_HU_OPTION_VALUE_NONE);
            resolver.AllowExceed = orderHead.AllowExceed;
            resolver.OrderType = orderHead.Type;
            resolver.AntiResolveHu = orderHead.AntiResolveHu;
            resolver.IsOddCreateHu = orderHead.IsOddCreateHu;
            resolver.IsPickFromBin = orderHead.IsPickFromBin;
            resolver.FulfillUnitCount = orderHead.FulfillUnitCount;
            resolver.IsShipByOrder = orderHead.IsShipByOrder;
            if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE)
            {
                resolver.IsScanHu = orderHead.IsReceiptScanHu;
            }
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP
                || resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPORDER)
            {
                resolver.IsScanHu = orderHead.IsShipScanHu;
            }
            #endregion
        }

        [Transaction(TransactionMode.Unspecified)]
        public void FillResolverByASN(Resolver resolver)
        {
            User user = userMgr.CheckAndLoadUser(resolver.UserCode);
            InProcessLocation inProcessLocation = inProcessLocationMgr.CheckAndLoadInProcessLocation(resolver.Input);

            //if (!user.HasPermission(inProcessLocation.PartyFrom.Code) ||
            //    !user.HasPermission(inProcessLocation.PartyTo.Code))
            //{
            //    throw new BusinessErrorException("Common.Business.Error.NoPermission");
            //}

            #region CopyProperty from ASN
            resolver.Code = inProcessLocation.IpNo;
            resolver.Status = inProcessLocation.Status;
            //resolver.IsDetailContainHu = inProcessLocation.IsDetailContainHu;
            resolver.IsScanHu = inProcessLocation.IsReceiptScanHu;
            if (inProcessLocation.InProcessLocationDetails != null && inProcessLocation.InProcessLocationDetails.Count > 0)
            {
                OrderHead orderHead =  inProcessLocation.InProcessLocationDetails[0].OrderLocationTransaction.OrderDetail.OrderHead;
                resolver.AntiResolveHu = orderHead.AntiResolveHu;
                resolver.FulfillUnitCount = orderHead.FulfillUnitCount;
                resolver.AllowExceed = orderHead.AllowExceed;
            }
            resolver.PickBy = inProcessLocation.IsDetailContainHu ? BusinessConstants.CODE_MASTER_PICKBY_HU : BusinessConstants.CODE_MASTER_PICKBY_ITEM;
            //resolver.PickBy = inProcessLocation.IsReceiptScanHu ? BusinessConstants.CODE_MASTER_PICKBY_HU : BusinessConstants.CODE_MASTER_PICKBY_ITEM;
            resolver.OrderType = inProcessLocation.OrderType;

            //resolver.AntiResolveHu = 
            #endregion
        }

        [Transaction(TransactionMode.Unspecified)]
        public void FillResolverByPickList(Resolver resolver)
        {
            PickList pickList = pickListMgr.CheckAndLoadPickList(resolver.Input);
            User user = userMgr.CheckAndLoadUser(resolver.UserCode);

            PickListHelper.CheckAuthrize(pickList, user);

            resolver.Code = pickList.PickListNo;
            resolver.Status = pickList.Status;
            resolver.PickBy = pickList.PickBy;
            if (pickList.PickListDetails != null && pickList.PickListDetails.Count > 0)
            {
                resolver.NeedPrintAsn = pickList.PickListDetails[0].OrderLocationTransaction.OrderDetail.OrderHead.NeedPrintAsn;
                resolver.IsPickFromBin = pickList.PickListDetails[0].OrderLocationTransaction.OrderDetail.OrderHead.IsPickFromBin;
            }
            //resolver.IsDetailContainHu = true;
            resolver.IsScanHu = true;//目前只有支持Hu才支持拣货
            resolver.OrderType = pickList.OrderType;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void FillResolverByBin(Resolver resolver)
        {
            User user = userMgr.CheckAndLoadUser(resolver.UserCode);
            StorageBin storageBin = storageBinMgr.CheckAndLoadStorageBin(resolver.Input);

            #region 校验
            if (!storageBin.IsActive)
            {
                throw new BusinessErrorException("Common.Business.Error.EntityInActive", storageBin.Code);
            }

            //if (!user.HasPermission(storageBin.Area.Location.Region.Code))
            //{
            //    throw new BusinessErrorException("Common.Business.Error.NoPermission");
            //}
            #endregion

            resolver.Description = storageBin.Description;
            resolver.BinCode = storageBin.Code;

            //库格一定为目的(操作)
            resolver.LocationCode = storageBin.Area.Location.Code;
            resolver.LocationToCode = storageBin.Area.Location.Code;
            resolver.Result = languageMgr.TranslateMessage("Warehouse.CurrentBinCode", resolver.UserCode, storageBin.Code);
        }


        /// <summary>
        ///  目前仅用于退库
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void FillResolverByLocation(Resolver resolver)
        {
            User user = userMgr.CheckAndLoadUser(resolver.UserCode);
            Location location = locationMgr.CheckAndLoadLocation(resolver.Input);

            #region 校验
            if (!location.IsActive)
            {
                throw new BusinessErrorException("Common.Business.Error.EntityInActive", location.Code);
            }

            //if (!user.HasPermission(location.Region.Code))
            //{
            //    throw new BusinessErrorException("Common.Business.Error.NoPermission");
            //}
            #endregion

            resolver.Description = location.Name;
            //库位一定为来源(操作)
            resolver.LocationCode = location.Code;
            resolver.LocationFormCode = location.Code;

            resolver.Result = languageMgr.TranslateMessage("Warehouse.LocationCode", resolver.UserCode, location.Name);
        }

        [Transaction(TransactionMode.Unspecified)]
        public void FillResolverByFlow(Resolver resolver)
        {
            Flow flow = flowMgr.CheckAndLoadFlow(resolver.Input);
            FillResolverByFlow(resolver, flow);
        }

        [Transaction(TransactionMode.Unspecified)]
        public void FillResolverByFlow(Resolver resolver, Flow flow)
        {
            User user = userMgr.CheckAndLoadUser(resolver.UserCode);
            //Flow flow = flowMgr.CheckAndLoadFlow(resolver.Input);

            #region 校验
            if (!flow.IsActive)
            {
                throw new BusinessErrorException("Common.Business.Error.EntityInActive", flow.Code);
            }

            //if (!user.HasPermission(flow.PartyFrom.Code)
            //|| !user.HasPermission(flow.PartyTo.Code))
            //{
            //    throw new BusinessErrorException("Common.Business.Error.NoPermission");
            //}
            #endregion

            resolver.Code = flow.Code;
            resolver.Description = flow.Description;
            resolver.IsScanHu = flow.IsShipScanHu || flow.IsReceiptScanHu;
            resolver.OrderType = flow.Type;
            resolver.AllowCreateDetail = flow.AllowCreateDetail;
            resolver.NeedPrintAsn = flow.NeedPrintAsn;
            resolver.NeedPrintReceipt = flow.NeedPrintReceipt;
            resolver.AutoPrintHu = flow.AutoPrintHu;
            resolver.AllowExceed = flow.AllowExceed;
            resolver.AntiResolveHu = flow.AntiResolveHu;
            resolver.IsOddCreateHu = flow.IsOddCreateHu;
            resolver.IsPickFromBin = flow.IsPickFromBin;
            resolver.CodePrefix = BusinessConstants.BARCODE_HEAD_FLOW;
            resolver.LocationFormCode = flow.LocationFrom == null ? string.Empty : flow.LocationFrom.Code;
            resolver.LocationToCode = flow.LocationTo == null ? string.Empty : flow.LocationTo.Code;
            resolver.FulfillUnitCount = flow.FulfillUnitCount;

            resolver.Result = languageMgr.TranslateMessage("Common.Business.Message.Flow", resolver.UserCode) + resolver.Description;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void FillDetailByFlow(Resolver resolver)
        {
            Flow flow = flowMgr.CheckAndLoadFlow(resolver.Code, true);
            FillDetailByFlow(resolver, flow);
        }

        [Transaction(TransactionMode.Unspecified)]
        public void FillDetailByFlow(Resolver resolver, Flow flow)
        {
            foreach (FlowDetail flowDetail in flow.FlowDetails)
            {
                flowDetail.LocationFrom = flowDetail.LocationFrom == null ? flow.LocationFrom : flowDetail.LocationFrom;
                flowDetail.LocationTo = flowDetail.LocationTo == null ? flow.LocationTo : flowDetail.LocationTo;
                //if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN
                //    || resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN)
                //{
                //    Location tempLocation = flowDetail.LocationFrom;
                //    flowDetail.LocationFrom = flowDetail.LocationTo;
                //    flowDetail.LocationTo = tempLocation;
                //}
            }
            resolver.Transformers = TransformerHelper.ConvertFlowDetailsToTransformers(flow.FlowDetails);
            //resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }

    }
}
