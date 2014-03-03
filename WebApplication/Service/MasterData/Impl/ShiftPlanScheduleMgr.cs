using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Procurement;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ShiftPlanScheduleMgr : ShiftPlanScheduleBaseMgr, IShiftPlanScheduleMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IOrderMgr OrderMgr;
        private IShiftMgr ShiftMgr;
        private ILeanEngineMgr LeanEngineMgr;
        public ShiftPlanScheduleMgr(IShiftPlanScheduleDao entityDao,
            ICriteriaMgr criteriaMgr,
            IOrderMgr OrderMgr,
            IShiftMgr ShiftMgr,
            ILeanEngineMgr LeanEngineMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.OrderMgr = OrderMgr;
            this.ShiftMgr = ShiftMgr;
            this.LeanEngineMgr = LeanEngineMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public ShiftPlanSchedule GetShiftPlanScheduleByItemFlowPlanDetId(int ItemFlowPlanDetId)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ShiftPlanSchedule));
            criteria.Add(Expression.Eq("ItemFlowPlanDetail.Id", ItemFlowPlanDetId));
            IList<ShiftPlanSchedule> spsList = criteriaMgr.FindAll<ShiftPlanSchedule>(criteria);

            if (spsList != null && spsList.Count > 0)
            {
                return spsList[0];
            }
            else
            {
                return null;
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public ShiftPlanSchedule GetShiftPlanSchedule(int flowDetailId, DateTime reqDate, string code, int seq)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ShiftPlanSchedule));
            criteria.Add(Expression.Eq("FlowDetail.Id", flowDetailId));
            criteria.Add(Expression.Eq("ReqDate", reqDate));
            criteria.Add(Expression.Eq("Shift.Code", code));
            criteria.Add(Expression.Eq("Sequence", seq));
            IList<ShiftPlanSchedule> spsList = criteriaMgr.FindAll<ShiftPlanSchedule>(criteria);

            if (spsList != null && spsList.Count > 0)
            {
                return spsList[0];
            }
            else
            {
                return null;
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ShiftPlanSchedule> GetShiftPlanScheduleList(string region, string flow, DateTime date, string code, string itemCode, string userCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ShiftPlanSchedule));
            criteria.CreateAlias("FlowDetail", "fd");
            criteria.CreateAlias("fd.Flow", "f");
            criteria.Add(Expression.Eq("ReqDate", date.Date));
            if (region != null && region.Trim() != string.Empty)
                criteria.Add(Expression.Eq("f.PartyTo.Code", region));
            if (flow != null && flow.Trim() != string.Empty)
                criteria.Add(Expression.Eq("f.Code", flow));
            if (itemCode != null && itemCode.Trim() != string.Empty)
                criteria.Add(Expression.Eq("fd.Item.Code", itemCode));

            return criteriaMgr.FindAll<ShiftPlanSchedule>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DataTable ConvertShiftPlanScheduleToDataTable(IList<ShiftPlanSchedule> spsList, IList<Shift> shiftList)
        {
            if (spsList == null || spsList.Count == 0)
                return null;
            if (shiftList == null || shiftList.Count == 0)
                return null;

            DataTable dt = new DataTable();
            dt.Columns.Add("FlowDetailId", typeof(int));
            dt.Columns.Add("ReqDate", typeof(DateTime));
            dt.Columns.Add("FlowCode", typeof(string));
            dt.Columns.Add("FlowDesc", typeof(string));
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("ItemDesc", typeof(string));
            dt.Columns.Add("Uom", typeof(string));
            dt.Columns.Add("TotalPlanQty", typeof(decimal));
            dt.Columns.Add("ItemFlowPlanDetId", typeof(int));

            //Dynamic columns
            string colName = "DynCol_";
            int i = 0;
            foreach (Shift shift in shiftList)
            {
                dt.Columns.Add(colName + i.ToString(), typeof(int));
                i++;
            }

            int j = 0;
            foreach (ShiftPlanSchedule sps in spsList)
            {
                bool isExist = false;
                foreach (DataRow dr in dt.Rows)
                {
                    if ((int)dr["FlowDetailId"] == sps.FlowDetail.Id && (DateTime)dr["ReqDate"] == sps.ReqDate)
                    {
                        dr[colName + j.ToString()] = sps.Id;
                        isExist = true;
                        break;
                    }
                }

                if (!isExist)
                {
                    DataRow dr = dt.NewRow();
                    dr["FlowDetailId"] = sps.FlowDetail.Id;
                    dr["ReqDate"] = sps.ReqDate;
                    dr["FlowCode"] = sps.FlowDetail.Flow.Code;
                    dr["FlowDesc"] = sps.FlowDetail.Flow.Description;
                    dr["ItemCode"] = sps.FlowDetail.Item.Code;
                    dr["ItemDesc"] = sps.FlowDetail.Item.Description;
                    dr["Uom"] = sps.FlowDetail.Uom.Code;
                    //if (sps.ItemFlowPlanDetail != null)
                    //    dr["TotalPlanQty"] = sps.ItemFlowPlanDetail.PlanQty;
                    //自动更新临时记录
                    if (sps.Shift != null || j == 0)
                        dr[colName + j.ToString()] = sps.Id;
                    //if (sps.ItemFlowPlanDetail != null)
                    //    dr["ItemFlowPlanDetId"] = sps.ItemFlowPlanDetail.Id;
                    dt.Rows.Add(dr);
                }
                j++;
            }

            return dt;
        }

        [Transaction(TransactionMode.Requires)]
        public void GenOrdersByShiftPlanScheduleId(int ShiftPlanScheduleId, string userCode)
        {
            ShiftPlanSchedule sps = this.LoadShiftPlanSchedule(ShiftPlanScheduleId);
            if (sps == null || sps.Shift == null)
                return;

            OrderHead oh = OrderMgr.TransferFlow2Order(sps.FlowDetail.Flow);
            oh.StartTime = ShiftMgr.GetShiftStartTime(sps.ReqDate, sps.Shift);
            oh.WindowTime = ShiftMgr.GetShiftEndTime(sps.ReqDate, sps.Shift);
            oh.Priority = BusinessConstants.CODE_MASTER_ORDER_PRIORITY_VALUE_NORMAL;
            if (oh.OrderDetails != null && oh.OrderDetails.Count > 0)
            {
                foreach (OrderDetail od in oh.OrderDetails)
                {
                    if (od.FlowDetail.Equals(sps.FlowDetail))
                    {
                        od.RequiredQty = sps.PlanQty;
                        od.OrderedQty = sps.PlanQty;
                    }
                }
            }

            LeanEngineMgr.CreateOrder(oh, userCode);
        }

        [Transaction(TransactionMode.Requires)]
        public void SaveShiftPlanSchedule(IList<ShiftPlanSchedule> shiftPlanScheduleList, User user)
        {
            if (shiftPlanScheduleList != null && shiftPlanScheduleList.Count > 0)
            {
                foreach (ShiftPlanSchedule shiftPlanSchedule in shiftPlanScheduleList)
                {
                    this.SaveShiftPlanSchedule(shiftPlanSchedule, user);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void SaveShiftPlanSchedule(ShiftPlanSchedule shiftPlanSchedule, User user)
        {
            //this.ClearOldShiftPlanSchedule(shiftPlanSchedule);

            //shiftPlanSchedule.LastModifyDate = DateTime.Now;
            //shiftPlanSchedule.LastModifyUser = user;
            //this.CreateShiftPlanSchedule(shiftPlanSchedule);

            ShiftPlanSchedule sps = this.GetShiftPlanSchedule(shiftPlanSchedule.FlowDetail.Id, shiftPlanSchedule.ReqDate, shiftPlanSchedule.Shift.Code, shiftPlanSchedule.Sequence);
            if (sps == null)
            {
                shiftPlanSchedule.LastModifyDate = DateTime.Now;
                shiftPlanSchedule.LastModifyUser = user;
                this.CreateShiftPlanSchedule(shiftPlanSchedule);
            }
            else
            {
                sps.PlanQty = shiftPlanSchedule.PlanQty;
                sps.LastModifyUser = shiftPlanSchedule.LastModifyUser;
                sps.LastModifyDate = DateTime.Now;
                this.UpdateShiftPlanSchedule(sps);
            }
        }

        [Transaction(TransactionMode.RequiresNew)]
        public void ClearOldShiftPlanSchedule(ShiftPlanSchedule shiftPlanSchedule)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ShiftPlanSchedule));
            criteria.Add(Expression.Eq("FlowDetail.Id", shiftPlanSchedule.FlowDetail.Id));
            criteria.Add(Expression.Eq("ReqDate", shiftPlanSchedule.ReqDate));
            criteria.Add(Expression.Eq("Shift.Code", shiftPlanSchedule.Shift.Code));
            IList<ShiftPlanSchedule> spsList = criteriaMgr.FindAll<ShiftPlanSchedule>(criteria);
            if (spsList != null && spsList.Count > 0)
            {
                foreach (ShiftPlanSchedule sps in spsList)
                {
                    this.DeleteShiftPlanSchedule(sps);
                }
            }
        }
        #endregion Customized Methods

        #region Private Method

        #endregion
    }
}