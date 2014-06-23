using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.MRP;
using com.Sconit.Web;
using com.Sconit.Utility;
using System.Data.SqlClient;
using System.Data;

public partial class NewMrp_TransferPlan_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<Flow> flowList = new List<Flow>();
            var dateset = this.TheGenericMgr.GetDatasetBySql(
                @"select t.Code,t.Code + '['+t.Desc1+']' as Description from FlowMstr p,FlowMstr t
                where p.Type = 'Production' and t.Type='Transfer' 
                and p.IsMRP=1 and t.IsMRP =1
                and p.IsActive = 1 and t.IsActive = 1
                and p.LocFrom = t.LocTo 
                and t.FlowStrategy ='MRP'", null);
            foreach (DataRow dr in dateset.Tables[0].Rows)
            {
                var flow = new Flow();
                flow.Code = (string)dr[0];
                flow.Description = (string)dr[1];
                flowList.Add(flow);
            }
            this.ddlFlow.DataSource = flowList;
            this.ddlFlow.DataBind();

            List<Shift> shiftList = new List<Shift>();

            IList<Shift> newShiftList = TheWorkCalendarMgr.GetShiftByDate(DateTime.Now, null, null);
            if (newShiftList != null || newShiftList.Count > 0)
            {
                shiftList.AddRange(newShiftList.OrderBy(s => s.Code).ToList());
            }

            this.ddlShift.DataSource = shiftList;
            this.ddlShift.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DateTime planDate = DateTime.Today;
        if (this.tbPlanDateDate.Text.Trim() != string.Empty)
        {
            DateTime.TryParse(this.tbPlanDateDate.Text.Trim(), out planDate);
        }
        else
        {
            this.ShowErrorMessage("计划时间不能为空");
            return;
        }
        if (this.ddlShift.SelectedValue == string.Empty)
        {
            this.ShowErrorMessage("班次不能为空");
            return;
        }
        string shift = this.ddlShift.SelectedValue;
        var sqlParams = new SqlParameter[3];
        sqlParams[0] = new SqlParameter("@FlowCode", this.ddlFlow.SelectedValue);
        sqlParams[1] = new SqlParameter("@PlanDate", planDate);
        sqlParams[2] = new SqlParameter("@Shift", this.ddlShift.SelectedValue);
        var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetTransferPlan", sqlParams);
        var firmPlanList = com.Sconit.Utility.IListHelper.DataTableToList<FirmPlan>(ds.Tables[0]);
        foreach (var firmPlan in firmPlanList)
        {
            //firmPlan.Qty = firmPlan.OutQty + firmPlan.SafeStock - firmPlan.InQty - firmPlan.InvQty;
            firmPlan.Qty = firmPlan.OutQty;
            firmPlan.Qty = firmPlan.Qty < 0 ? 0 : firmPlan.Qty;
        }
        var list = firmPlanList.Where(p => p.Qty > 0).ToList();
        list.AddRange(firmPlanList.Where(p => p.Qty == 0));

        this.GV_Order.DataSource = list;
        this.GV_Order.DataBind();
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            var firmPlanList = new List<FirmPlan>();
            foreach (GridViewRow row in this.GV_Order.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    var firmPlan = new FirmPlan();
                    firmPlan.FlowCode = row.Cells[1].Text;
                    firmPlan.ItemCode = row.Cells[2].Text;
                    double qty = 0;
                    string tbQty = ((TextBox)row.Cells[11].FindControl("tbQty")).Text.Trim();
                    tbQty = tbQty == string.Empty ? "0" : tbQty;
                    if (!double.TryParse(tbQty, out qty))
                    {
                        this.ShowErrorMessage("${MasterData.MiscOrder.WarningMessage.InputQtyFormat.Error}");
                    }
                    firmPlan.Qty = qty;

                    if (firmPlan.Qty > 0)
                    {
                        firmPlanList.Add(firmPlan);
                    }
                }
            }

            var orderHeadList = TheMrpMgr.CreateTransferOrder(firmPlanList, this.CurrentUser);
            if (orderHeadList.Count > 0)
            {
                this.ShowSuccessMessage("MasterData.Order.OrderHead.AddOrder.Successfully", orderHeadList.First().OrderNo);
                //跳转到相应的订单查询一面
                string url = "Main.aspx?mid=Order.OrderHead.Procurement__mp--ModuleType-Procurement_ModuleSubType-Nml_StatusGroupId-4__act--ListAction";
                Page.ClientScript.RegisterStartupScript(GetType(), "method",
                    " <script language='javascript' type='text/javascript'>timedMsg('" + url + "'); </script>");
            }
            else
            {
                this.ShowErrorMessage("未能创建任何订单");
            }
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
        catch (Exception ex)
        {
            this.ShowErrorMessage(ex.Message);
        }
    }

    protected void GV_Order_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FirmPlan firmPlan = (FirmPlan)(e.Row.DataItem);
            var qty = (Math.Ceiling(firmPlan.Qty / firmPlan.Uc) * firmPlan.Uc).ToString();
            ((TextBox)e.Row.Cells[11].FindControl("tbQty")).Text = qty;
        }
    }
}