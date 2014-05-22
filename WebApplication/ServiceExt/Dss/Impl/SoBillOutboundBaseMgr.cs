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
using com.Sconit.Persistence;
using System.Data.SqlClient;
using System.Data;

namespace com.Sconit.Service.Dss.Impl
{
    public abstract class SoBillOutboundBaseMgr : AbstractOutboundMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IBillMgr billMgr;
        private ICommonOutboundMgr commonOutboundMgr;
        private ISqlHelperDao sqlHelperDao;

        public SoBillOutboundBaseMgr(INumberControlMgr numberControlMgr,
            IDssExportHistoryMgr dssExportHistoryMgr,
            ICriteriaMgr criteriaMgr,
            IDssOutboundControlMgr dssOutboundControlMgr,
            IDssObjectMappingMgr dssObjectMappingMgr,
            ICommonOutboundMgr commonOutboundMgr,
            ILocationMgr locationMgr,
            IBillMgr billMgr,
            ISqlHelperDao sqlHelperDao
            )
            : base(numberControlMgr, dssExportHistoryMgr, criteriaMgr, dssOutboundControlMgr, dssObjectMappingMgr, locationMgr)
        {
            this.criteriaMgr = criteriaMgr;
            this.commonOutboundMgr = commonOutboundMgr;
            this.billMgr = billMgr;
            this.sqlHelperDao = sqlHelperDao;
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override IList<DssExportHistory> ExtractOutboundData(DssOutboundControl dssOutboundControl)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(BillDetail))
                .CreateAlias("Bill", "b")
                .Add(Expression.Gt("Id", dssOutboundControl.Mark))
                .Add(Expression.Eq("b.TransactionType", BusinessConstants.BILL_TRANS_TYPE_SO));
            //.Add(Expression.In("b.Status", new string[] { BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE, BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT }));

            IList<BillDetail> result = criteriaMgr.FindAll<BillDetail>(criteria);
            IList<DssExportHistory> resultList = this.ConvertList(result, dssOutboundControl);

            IList<Bill> billList = result.Select(r => r.Bill).Distinct().ToList();
            foreach (Bill bill in billList)
            {
                bill.IsExport = true;
                this.billMgr.UpdateBill(bill);
            }

            return resultList;
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

        //protected override object Serialize(object obj)
        //{
        //    throw new NotImplementedException();
        //}

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
                    dssExportHistory.Location = dssExportHistory.DssOutboundControl.UndefinedString1;//客户库位
                    dssExportHistory.Qty = billDetail.BilledQty;
                    dssExportHistory.EffectiveDate = billDetail.Bill.CreateDate.Date;
                    dssExportHistory.PartyTo = billDetail.Bill.BillAddress.Party.Code;//客户

                    dssExportHistory.DefinedString1 = billDetail.Bill.BillNo;//开票通知单号

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
                commonOutboundMgr.GetOrderLocationTransactionInfo(dssExportHistory.OrderNo, dssExportHistory.Item, BusinessConstants.IO_TYPE_OUT);

            if (orderLocationTransaction != null)
            {
                //dssExportHistory.Location = orderLocationTransaction.Location != null ? orderLocationTransaction.Location.Code : null;//发出库位
                dssExportHistory.PartyFrom = orderLocationTransaction.OrderDetail.OrderHead.PartyFrom.Code;//发出区域
            }
            else
            {
                ////归档库查找
                //DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderLocationTransaction));
                //criteria.CreateAlias("OrderDetail", "od");
                //criteria.Add(Expression.Eq("od.OrderHead.OrderNo", dssExportHistory.OrderNo));
                //criteria.Add(Expression.Eq("Item.Code", dssExportHistory.Item));
                //criteria.Add(Expression.Eq("IOType", BusinessConstants.IO_TYPE_OUT));

                //IList<OrderLocationTransaction> result = archCriteriaMgr.FindAll<OrderLocationTransaction>(criteria);
                //if (result != null && result.Count > 0)
                //{
                //   orderLocationTransaction= result[0];
                //   dssExportHistory.PartyFrom = orderLocationTransaction.OrderDetail.OrderHead.PartyFrom.Code;//发出区域
                //}
                //else
                //{
                   
                //}
                try
                {
                    SqlParameter[] sqlParameter = new SqlParameter[3];
                    sqlParameter[0] = new SqlParameter("@Item", SqlDbType.VarChar, 50);
                    sqlParameter[0].Value = dssExportHistory.Item;

                    sqlParameter[1] = new SqlParameter("@OrderNo", SqlDbType.VarChar, 50);
                    sqlParameter[1].Value = dssExportHistory.OrderNo;

                    sqlParameter[2] = new SqlParameter("@PartyFrom", SqlDbType.VarChar, 50);
                    sqlParameter[2].Direction = ParameterDirection.InputOutput;

                    sqlHelperDao.ExecuteStoredProcedure("GetPartyFrom", sqlParameter);

                    dssExportHistory.PartyFrom = sqlParameter[2].Value.ToString();
                }
                catch (Exception)
                {
                    dssExportHistory.PartyFrom = string.Empty;
                }
            }
        }
        #endregion
    }
}
