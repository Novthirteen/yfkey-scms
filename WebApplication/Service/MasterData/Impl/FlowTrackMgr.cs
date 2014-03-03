using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class FlowTrackMgr : FlowTrackBaseMgr, IFlowTrackMgr
    {
        public FlowTrackMgr(IFlowTrackDao entityDao)
            : base(entityDao)
        {
        }


        private string[] FlowHead2FlowTrackCloneFields = new string[] 
            {      
	                "Code",
	                "Description",
	                "IsActive",
                    "ReferenceFlow",
	                "Type",
                    "DockDescription",
	                "FlowStrategy",
	                "LotGroup",
	                "LeadTime",
	                "EmTime",
	                "MaxCirTime",
	                "WinTime1",
	                "WinTime2",
	                "WinTime3",
	                "WinTime4",
	                "WinTime5",
	                "WinTime6",
	                "WinTime7",
	                "NextOrderTime",
	                "NextWinTime",
	                "WeekInterval",
	                "IsAutoCreate",
	                "IsAutoRelease",
	                "IsAutoStart",
	                "IsAutoShip",
	                "IsAutoReceive",
	                "IsAutoBill",
	                "IsListDetail",
	                "IsShowPrice",
	                "CheckDetailOption",
	                "StartLatency",
	                "CompleteLatency",
	                "NeedPrintOrder",
	                "NeedPrintAsn",
	                "NeedPrintReceipt",
	                "GoodsReceiptGapTo",
	                "AllowExceed",
	                "FulfillUnitCount",
	                "ReceiptTemplate",
	                "OrderTemplate",
	                "AsnTemplate",
	                "CreateDate",
	                "AllowCreateDetail",
	                "BillSettleTerm",
	                "IsShipScanHu",
	                "IsReceiptScanHu",
	                "CreateHuOption",
	                "AutoPrintHu",
	                "IsOddCreateHu",
	                "IsAutoCreatePickList",
	                "NeedInspection",
	                "IsGoodsReceiveFIFO",
	                "AntiResolveHu",
	                "MaxOnlineQty",
	                "HuTemplate",
	                "AllowRepeatlyExceed",
	                "IsPickFromBin",
	                "IsShipByOrder",
	                "IsAsnUniqueReceipt",
	                "Version"
                 };


        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public void CreateFlowTrack(Flow flow,string eventCode)
        {
            FlowTrack flowTrack = new FlowTrack();
            CloneHelper.CopyProperty(flow, flowTrack, FlowHead2FlowTrackCloneFields);
            flowTrack.BillFrom = flow.BillFrom != null ? flow.BillFrom.Code : string.Empty;
            flowTrack.BillTo = flow.BillTo != null ? flow.BillTo.Code : string.Empty;
            flowTrack.Carrier = flow.Carrier != null ? flow.Carrier.Code : string.Empty;
            flowTrack.CarrierBillAddress = flow.CarrierBillAddress != null ? flow.CarrierBillAddress.Code : string.Empty;
            flowTrack.CreateUser = flow.CreateUser != null ? flow.CreateUser.Code : string.Empty;
            flowTrack.LocationFrom = flow.LocationFrom != null ? flow.LocationFrom.Code : string.Empty;
            flowTrack.LocationTo = flow.LocationTo != null ? flow.LocationTo.Code : string.Empty;
            flowTrack.Currency = flow.Currency != null ? flow.Currency.Code : string.Empty;
            flowTrack.LastModifyUser = flow.LastModifyUser.Code;
            flowTrack.LastModifyDate = DateTime.Now;
            flowTrack.PartyFrom = flow.PartyFrom != null ? flow.PartyFrom.Code : string.Empty;
            flowTrack.PartyTo = flow.PartyTo != null ? flow.PartyTo.Code : string.Empty;
            flowTrack.PriceListFrom = flow.PriceListFrom != null ? flow.PriceListFrom.Code : string.Empty;
            flowTrack.PriceListTo = flow.PriceListTo != null ? flow.PriceListTo.Code : string.Empty;
            flowTrack.ReturnRouting = flow.ReturnRouting != null ? flow.ReturnRouting.Code : string.Empty;
            flowTrack.Routing = flow.Routing != null ? flow.Routing.Code : string.Empty;
            flowTrack.ShipFrom = flow.ShipFrom != null ? flow.ShipFrom.Code : string.Empty;
            flowTrack.ShipTo = flow.ShipTo != null ? flow.ShipTo.Code : string.Empty;
            flowTrack.EventCode = eventCode;

            this.CreateFlowTrack(flowTrack);
        }


        #endregion Customized Methods
    }
}