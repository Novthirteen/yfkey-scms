using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using System.Collections;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity.Dss;
using com.Sconit.Entity.Exception;

namespace com.Sconit.Service.Dss.Impl
{
    public class PoBillOutboundMgr : AbstractOutboundMgr
    {
        private ICriteriaMgr criteriaMgr;
        private ICommonOutboundMgr commonOutboundMgr;
        private ILocationMgr locMgr;
        public PoBillOutboundMgr(INumberControlMgr numberControlMgr,
            IDssExportHistoryMgr dssExportHistoryMgr,
            ICriteriaMgr criteriaMgr,
            IDssOutboundControlMgr dssOutboundControlMgr,
            IDssObjectMappingMgr dssObjectMappingMgr,
            ICommonOutboundMgr commonOutboundMgr,
            ILocationMgr locationMgr)
            : base(numberControlMgr, dssExportHistoryMgr, criteriaMgr, dssOutboundControlMgr, dssObjectMappingMgr, locationMgr)
        {
            this.criteriaMgr = criteriaMgr;
            this.commonOutboundMgr = commonOutboundMgr;
            this.locMgr = locationMgr;
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override IList<DssExportHistory> ExtractOutboundData(DssOutboundControl dssOutboundControl)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(BillDetail))
                .CreateAlias("Bill", "b")
                .Add(Expression.Gt("Id", dssOutboundControl.Mark))
                .Add(Expression.Eq("b.TransactionType", BusinessConstants.BILL_TRANS_TYPE_PO))
                .Add(Expression.In("b.Status", new string[] { BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE, BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT }));

            IList<BillDetail> result = criteriaMgr.FindAll<BillDetail>(criteria);
            return this.ConvertList(result, dssOutboundControl);
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override object GetOutboundData(DssExportHistory dssExportHistory)
        {
            if (!commonOutboundMgr.CheckBillStatusValid(dssExportHistory.DefinedString1))
            {
                throw new BusinessErrorException("状态不合法");
            }

            return (object)dssExportHistory;
        }

        protected override object Serialize(object obj)
        {
            DssExportHistory dssExportHistory = (DssExportHistory)obj;
            DateTime effDate = dssExportHistory.EffectiveDate.HasValue ? dssExportHistory.EffectiveDate.Value : DateTime.Today;

            string[] line1 = new string[] 
            { 
                DssHelper.GetEventValue(dssExportHistory.EventCode),
                dssExportHistory.DefinedString1,//开票通知单号
                dssExportHistory.ReceiptNo,//收货单号
                null,//Line
                dssExportHistory.Item,//零件号
                dssExportHistory.PartyFrom,//供应商
                dssExportHistory.Qty.ToString("0.########"),//数量
                dssExportHistory.DefinedString2,//币种
                dssExportHistory.DefinedString3,//发票单价
                dssExportHistory.Uom,//单位
                dssExportHistory.DefinedString4,//采购单价
                dssExportHistory.DefinedString5,//金额
                dssExportHistory.OrderNo,//订单号
                DssHelper.FormatDate(effDate,dssExportHistory.DssOutboundControl.ExternalSystem.Code),//收货日期
                dssExportHistory.Location,//库位
                null//排程单号
            };

            string[][] data = new string[][] { line1 };

            return new object[] { effDate, data };
        }

        #region Private Method
        private IList<DssExportHistory> ConvertList(IList<BillDetail> list, DssOutboundControl dssOutboundControl)
        {
            IList<DssExportHistory> result = new List<DssExportHistory>();
            if (list != null && list.Count > 0)
            {
                foreach (BillDetail billDetail in list)
                {
                    DssExportHistory dssExportHistory = new DssExportHistory();

                    dssExportHistory.DssOutboundControl = dssOutboundControl;
                    dssExportHistory.EventCode = BusinessConstants.DSS_EVENT_CODE_CREATE;
                    dssExportHistory.IsActive = true;
                    dssExportHistory.CreateDate = DateTime.Now;

                    dssExportHistory.OriginalId = billDetail.Id;
                    dssExportHistory.OrderNo = billDetail.ActingBill.OrderNo;
                    dssExportHistory.ReceiptNo = billDetail.ActingBill.ReceiptNo;
                    dssExportHistory.Item = billDetail.ActingBill.Item.Code;
                    dssExportHistory.Uom = billDetail.ActingBill.Uom.Code;
                    dssExportHistory.Qty = billDetail.BilledQty;
                    dssExportHistory.EffectiveDate = billDetail.ActingBill.EffectiveDate;
                    dssExportHistory.PartyFrom = billDetail.Bill.BillAddress.Party.Code;//供应商

                    dssExportHistory.DefinedString1 = billDetail.Bill.BillNo;//开票通知单号
                    dssExportHistory.DefinedString2 = billDetail.Currency.Code;//币种
                    dssExportHistory.DefinedString3 = billDetail.UnitPrice.ToString("0.########");//发票单价
                    dssExportHistory.DefinedString4 = billDetail.UnitPrice.ToString("0.########");//采购单价
                    dssExportHistory.DefinedString5 = billDetail.OrderAmount.ToString("0.########");//金额

                    dssExportHistory.KeyCode = DssHelper.GetBillKeyCode(dssExportHistory, billDetail.Bill.BillNo);

                    this.GetLoctransInfo(dssExportHistory);
                    result.Add(dssExportHistory);
                }
            }

            return result;
        }

        /// <summary>
        /// Location,Region
        /// </summary>
        /// <param name="dssExportHistory"></param>
        [Transaction(TransactionMode.Unspecified)]
        private void GetLoctransInfo(DssExportHistory dssExportHistory)
        {
            OrderLocationTransaction orderLocationTransaction =
                commonOutboundMgr.GetOrderLocationTransactionInfo(dssExportHistory.OrderNo, dssExportHistory.Item, BusinessConstants.IO_TYPE_IN);

            if (orderLocationTransaction != null)
            {
                dssExportHistory.Location = orderLocationTransaction.Location != null ? orderLocationTransaction.Location.Code : null;//收货库位
                //if(orderLocationTransaction.Location.Code=="Reject")
                //    dssExportHistory.PartyTo=orderLocationTransaction.
                dssExportHistory.PartyTo = orderLocationTransaction.OrderDetail.OrderHead.PartyTo.Code;//收货区域
            }
        }
        #endregion
    }
}
