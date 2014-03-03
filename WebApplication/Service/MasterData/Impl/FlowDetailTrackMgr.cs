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
    public class FlowDetailTrackMgr : FlowDetailTrackBaseMgr, IFlowDetailTrackMgr
    {
        public FlowDetailTrackMgr(IFlowDetailTrackDao entityDao)
            : base(entityDao)
        {
        }

        private string[] FlowDetail2FlowDetailTrackCloneFields = new string[] 
            {      
	                "ReferenceItemCode",
                    "TimeUnit",
                    "UnitCount",
                    "Sequence",
                    "IsAutoCreate",
                    "SafeStock",
                    "MaxStock",
                    "MinLotSize",
                    "OrderLotSize",
                    "GoodsReceiptLotSize",
                    "BatchSize",
                    "RoundUpOption",
                    "BillSettleTerm",
                    "HuLotSize",
                    "PackageVolumn",
                    "PackageType",
                    "ProjectDescription",
                    "Remark",
                    "NeedInspection",
                    "IdMark",
                    "BarCodeType",
                    "CustomerItemCode",
                    "StartDate",
                    "EndDate",
                    "OddShipOption",
                    "Version"
                 };

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public void CreateFlowDetailTrack(FlowDetail flowDetail,string eventCode)
        {
            FlowDetailTrack flowDetailTrack = new FlowDetailTrack();
            CloneHelper.CopyProperty(flowDetail, flowDetailTrack, FlowDetail2FlowDetailTrackCloneFields);
            flowDetailTrack.FlowDetailId = flowDetail.Id;
            flowDetailTrack.Flow = flowDetail.Flow.Code;
            flowDetailTrack.Item = flowDetail.Item.Code;
            flowDetailTrack.Customer = flowDetail.Customer != null ? flowDetail.Customer.Code : string.Empty;
            flowDetailTrack.Uom = flowDetail.Uom != null ? flowDetail.Uom.Code : string.Empty;
            flowDetailTrack.Bom = flowDetail.Bom != null ? flowDetail.Bom.Code : string.Empty;
            flowDetailTrack.BillFrom = flowDetail.BillFrom != null ? flowDetail.BillFrom.Code : string.Empty;
            flowDetailTrack.BillTo = flowDetail.BillTo != null ? flowDetail.BillTo.Code : string.Empty;
            flowDetailTrack.LocationFrom = flowDetail.LocationFrom != null ? flowDetail.LocationFrom.Code : string.Empty;
            flowDetailTrack.LocationTo = flowDetail.LocationTo != null ? flowDetail.LocationTo.Code : string.Empty;
            flowDetailTrack.LastModifyUser = flowDetail.LastModifyUser.Code;
            flowDetailTrack.LastModifyDate = DateTime.Now;
            flowDetailTrack.PriceListFrom = flowDetail.PriceListFrom != null ? flowDetail.PriceListFrom.Code : string.Empty;
            flowDetailTrack.PriceListTo = flowDetail.PriceListTo != null ? flowDetail.PriceListTo.Code : string.Empty;
            flowDetailTrack.EventCode = eventCode;

            base.CreateFlowDetailTrack(flowDetailTrack);
        }


        #endregion Customized Methods
    }
}