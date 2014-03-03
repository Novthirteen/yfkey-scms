using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Procurement;
using com.Sconit.Entity;
using com.Sconit.Entity.Procurement;
using System.Collections.Generic;
using System.Drawing;
using com.Sconit.Utility;
using System.Text;

public partial class MRP_PlanSchedule_List : ListModuleBase
{
    public event EventHandler ListEvent;

    private const int _dynColCount = 6;
    private const int _firstDynColIndex = 4;
    private const int _firstDataTableDynColIndex = 8;
    private const int _pageSize = 20;
    private const int _limitSize = 360;

    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }
    public object[] criteria
    {
        get
        {
            return (object[])ViewState["Criteria"];
        }
        set
        {
            ViewState["Criteria"] = value;
        }
    }

    private DataTable dt
    {
        get
        {
            return (DataTable)Session["DataTable"];
        }
        set
        {
            Session["DataTable"] = value;
        }
    }
    private List<DateTime> dateList
    {
        get
        {
            return (List<DateTime>)Session["ScheduleTimeLine"];
        }
        set
        {
            Session["ScheduleTimeLine"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
        else
        {
            //重新加载动态控件
            this.GenDynControls();
        }
    }
    public override void UpdateView()
    {
    }

    public void ListView()
    {
        ListView(false);
    }
    public void ListView(bool autoPlan)
    {
        string party = ((object[])criteria)[0].ToString();
        string timePeriodType = ((object[])criteria)[1].ToString();
        DateTime startTime = (DateTime)(((object[])criteria)[2]);
        DateTime endTime = (DateTime)(((object[])criteria)[3]);
        string flowCode = ((object[])criteria)[4].ToString();
        string itemCode = ((object[])criteria)[5].ToString();

        //startTime = DateTimeHelper.GetStartTime(timePeriodType, startTime);
        startTime = DateTimeHelper.GetWeekStart(startTime);
        if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
        {
            endTime = startTime.AddDays(_dynColCount);
        }
        else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK)
        {
            endTime = startTime.AddDays(_dynColCount * 4);
        }
        else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_MONTH)
        {
            endTime = startTime.AddMonths(_dynColCount);
        }
        else
        { }
        //endTime = DateTimeHelper.GetEndStartTime(timePeriodType, endTime);

        this.dateList = new List<DateTime>();
        DateTime date = startTime;
        while (DateTime.Compare(date, endTime) <= 0)
        {
            dateList.Add(date);
            date = DateTimeHelper.GetNextStartTime(timePeriodType, date);
        }

        IList<ItemFlowPlan> preIFPList = TheItemFlowPlanMgr.GetPreItemFlowPlan(ModuleType, timePeriodType, startTime, endTime, party, flowCode, itemCode, this.CurrentUser.Code);

        //todo,page

        this.dt = TheItemFlowPlanMgr.FillDataTableByItemFlowPlan(dateList, preIFPList, ModuleType, timePeriodType, autoPlan);
        this.GenerateDynamicColumns(dateList, timePeriodType);

        this.GV_List.DataSource = dt;
        this.GV_List.DataBind();

        if (this.GV_List.Rows.Count > 0)
            ListEvent(new object[] { true }, null);
        else
            ListEvent(new object[] { false }, null);
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            string planViewType = drv["Type"].ToString();
            if (planViewType == string.Empty)
            {
                //Group line
                e.Row.BackColor = Color.Gray;
                e.Row.ForeColor = Color.Snow;
            }
            else if (planViewType == BusinessConstants.PLAN_VIEW_TYPE_DEMAND)
            {
                //Demand
                e.Row.BackColor = Color.Aquamarine;
                e.Row.Cells[3].Text = "${MRP.PlanViewType.Demand}";

                //ToolTip
                e.Row.Cells[2].ToolTip = "${MRP.PlanViewType.TotalDemand}";

                //demand source
                foreach (DateTime date in dateList)
                {
                    int dynColIndex = dateList.IndexOf(date);
                    if ((decimal)drv["DynCol_" + dynColIndex.ToString()] > 0)
                    {
                        int itemFlowPlanId = (int)drv["ItemFlowPlanId"];
                        string timePeriodType = (string)drv["TimePeriodType"];

                        ItemFlowPlanDetail ifpd = TheItemFlowPlanDetailMgr.GetItemFlowPlanDetail(itemFlowPlanId, timePeriodType, date);
                        IList<ItemFlowPlanTrack> ifptList = TheItemFlowPlanTrackMgr.GetItemFlowPlanTrackList(ifpd, null, null);
                        e.Row.Cells[dynColIndex + _firstDynColIndex].Attributes.Add("title", this.GetToolTip(ifptList));
                    }
                }
            }
            else if (planViewType == BusinessConstants.PLAN_VIEW_TYPE_PLAN)
            {
                //Plan
                if (ModuleType != BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_DMDSCHEDULE)
                {
                    e.Row.Cells[_firstDynColIndex - 1].Text = "${MRP.PlanViewType.Plan}";

                    //ToolTip
                    e.Row.Cells[1].ToolTip = "${Common.Business.MaxInv}";
                    e.Row.Cells[2].ToolTip = "${MRP.PlanViewType.TotalPlan}";
                }

                for (int j = 0; j < GV_List.Columns.Count - _firstDynColIndex; j++)
                {
                    //首次加载动态控件
                    TextBox _editControl = new TextBox();
                    _editControl.ID = this.GetDynControlID(j);
                    _editControl.Width = 50;
                    decimal planQty = (decimal)drv["DynCol_" + j.ToString()];
                    if (planQty > 0)
                        _editControl.Text = planQty.ToString("0.########");

                    _editControl.Attributes.Add("onclick", "this.select()");
                    if (!e.Row.Cells[j + _firstDynColIndex].Controls.Contains(_editControl))
                        e.Row.Cells[j + _firstDynColIndex].Controls.Add(_editControl);
                }
            }
            //else if (planViewType == BusinessConstants.PLAN_VIEW_TYPE_ORDER)
            //{
            //    e.Row.Cells[3].Text = "订单";
            //}
            else if (planViewType == BusinessConstants.PLAN_VIEW_TYPE_PAB)
            {
                //PAB
                e.Row.Cells[_firstDynColIndex - 1].Text = "${MRP.PlanViewType.PAB}";

                //ToolTip
                e.Row.Cells[1].ToolTip = "${Common.Business.SafeInv}";
                e.Row.Cells[2].ToolTip = "${Common.Business.StartPAB}";

                decimal safeStock = decimal.Parse(drv["StaCol_1"].ToString());
                for (int j = 0; j < GV_List.Columns.Count - _firstDynColIndex; j++)
                {
                    decimal PAB = decimal.Parse(drv["DynCol_" + j.ToString()].ToString());
                    if (PAB < 0)
                    {
                        e.Row.Cells[j + _firstDynColIndex].BackColor = Color.Red;
                    }
                    else if (PAB < safeStock)
                    {
                        e.Row.Cells[j + _firstDynColIndex].BackColor = Color.Yellow;
                    }
                    else
                    {
                        e.Row.Cells[j + _firstDynColIndex].BackColor = Color.LawnGreen;
                    }
                }
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "${Common.Business.ItemCode}";
            e.Row.Cells[1].Text = "${Common.Business.ItemDescription}";
            if (ModuleType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_DMDSCHEDULE)
            {
                e.Row.Cells[2].Text = "${Common.Business.ReferenceItemCode}";
                e.Row.Cells[3].Text = "${Common.Business.Uom}";
            }
            else
            {
                e.Row.Cells[2].Text = "${Common.Business.Total}";
                e.Row.Cells[3].Text = "${Common.Business.Date}";
            }
        }
    }

    public void Save()
    {
        if (GV_List.Rows.Count == 0 || dt.Rows.Count == 0)
            return;

        int rowIndex = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["Type"].ToString() == BusinessConstants.PLAN_VIEW_TYPE_PLAN)
            {
                string timePeriodType = dr["TimePeriodType"].ToString();
                for (int j = 0; j < GV_List.Columns.Count - _firstDynColIndex; j++)
                {
                    int itemFlowPlanId = (int)dr["ItemFlowPlanId"];
                    DateTime date = this.dateList[j];
                    ItemFlowPlan ifp = TheItemFlowPlanMgr.LoadItemFlowPlan(itemFlowPlanId);
                    try
                    {
                        string editControlID = this.GetDynControlID(j);
                        TextBox _editControl = (TextBox)GV_List.Rows[rowIndex].Cells[j + _firstDynColIndex].FindControl(editControlID);
                        string colName = "DynCol_" + j.ToString();
                        decimal newValue = 0;
                        if (_editControl.Text.Trim() != string.Empty)
                            newValue = decimal.Parse(_editControl.Text.Trim());

                        decimal oldValue = (decimal)dr[colName];

                        if (newValue != 0 || oldValue != 0)
                        {
                            ItemFlowPlanDetail ifpd = new ItemFlowPlanDetail();
                            ifpd.ItemFlowPlan = ifp;
                            ifpd.TimePeriodType = timePeriodType;
                            ifpd.ReqDate = date;
                            ifpd.PlanQty = newValue;
                            //ifpd.LastModifyDate = DateTime.Now;
                            //ifpd.LastModifyUser = this.Session["UserCode"].ToString();

                            if (ifpd.PlanQty < 0)
                            {
                                ShowErrorMessage("MRP.Error.Save", ifp.FlowDetail.Item.Code, GetHeaderText(timePeriodType, date));
                                return;
                            }

                            TheItemFlowPlanDetailMgr.SaveItemFlowPlanDetail(ifpd);
                            dr[colName] = newValue;
                            ShowSuccessMessage("Common.Business.Result.Save.Successfully");
                        }
                    }
                    catch (Exception)
                    {
                        ShowErrorMessage("MRP.Error.Save", ifp.FlowDetail.Item.Code, GetHeaderText(timePeriodType, date));
                        return;
                    }
                }
            }
            rowIndex++;
        }

        this.ListView();
    }

    public void Release()
    {
        if (GV_List.Rows.Count == 0 || dt.Rows.Count == 0)
            return;

        int rowIndex = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["Type"].ToString() == BusinessConstants.PLAN_VIEW_TYPE_PLAN)
            {
                string timePeriodType = dr["TimePeriodType"].ToString();
                for (int j = 0; j < dt.Columns.Count - _firstDataTableDynColIndex; j++)
                {
                    int itemFlowPlanId = (int)dr["ItemFlowPlanId"];
                    DateTime date = this.dateList[j];

                    string colName = "DynCol_" + j.ToString();
                    decimal planQty = (decimal)dr[colName];
                    if (planQty > 0)
                    {
                        try
                        {
                            TheItemFlowPlanMgr.ReleaseItemFlowPlanDetail(itemFlowPlanId, timePeriodType, date);
                        }
                        catch (Exception e)
                        {
                            ShowErrorMessage("MRP.Result.Fail.Release", e.Message);
                            return;
                        }
                    }
                }
            }
            rowIndex++;
        }

        ShowSuccessMessage("MRP.Result.Success.Release");
    }

    public void Run()
    {
        ListView(true);
        ShowSuccessMessage("MRP.Result.Success.AutoPlan");
    }

    private void GenerateDynamicColumns(List<DateTime> dateList, string timePeriodType)
    {
        //Clear Old
        while (GV_List.Columns.Count > _firstDynColIndex)
        {
            GV_List.Columns.RemoveAt(_firstDynColIndex);
        }

        //Generate new columns
        int i = 0;
        foreach (DateTime date in dateList)
        {
            string dynColName = "DynCol_" + i.ToString();
            BoundField bf = new BoundField();
            bf.DataField = dynColName;
            bf.HeaderText = this.GetHeaderText(timePeriodType, date);
            bf.HeaderStyle.Wrap = false;
            bf.ItemStyle.Wrap = false;
            bf.DataFormatString = "{0:0.########}";

            if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
            {
                //双休日特殊颜色标识
                if ((int)date.DayOfWeek == 0 || (int)date.DayOfWeek == 6)
                    bf.HeaderStyle.ForeColor = Color.Pink;
            }

            GV_List.Columns.Add(bf);
            i++;
        }
    }

    private string GetHeaderText(string timePeriodType, DateTime date)
    {
        string headerText = string.Empty;
        if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
        {
            headerText = date.ToString("yyyy-MM-dd");
        }
        else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK)
        {
            headerText = "${MRP.WeekNo," + date.Year.ToString() + "," + DateTimeHelper.GetWeekIndex(date).ToString() + "}";
        }
        else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_MONTH)
        {
            headerText = date.ToString("yyyy-MM");
        }
        else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_QUARTER)
        {
            headerText = "${MRP.QuarterNo," + date.Year.ToString() + "," + DateTimeHelper.GetQuarterIndex(date).ToString() + "}";
        }
        else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_YEAR)
        {
            headerText = date.ToString("yyyy");
        }
        else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_HOUR)
        {
            headerText = date.ToString("yyyy-MM-dd HH:mm");
        }

        return headerText;
    }

    private DateTime GetLimitEndTime(string timePeriodType, DateTime startTime, DateTime endTime)
    {
        DateTime limitEndTime = endTime;

        if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
            limitEndTime = startTime.AddDays(_limitSize);
        else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_HOUR)
            limitEndTime = startTime.AddHours(_limitSize);

        if (DateTime.Compare(limitEndTime, endTime) > 0)
            limitEndTime = endTime;

        return limitEndTime;
    }

    private void GenDynControls()
    {
        if (GV_List.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Type"].ToString() == BusinessConstants.PLAN_VIEW_TYPE_PLAN)
                {
                    for (int j = 0; j < GV_List.Columns.Count - _firstDynColIndex; j++)
                    {
                        TextBox _editControl = new TextBox();
                        _editControl.ID = this.GetDynControlID(j);
                        _editControl.Width = 50;
                        _editControl.Attributes.Add("onclick", "this.select()");

                        int i = dt.Rows.IndexOf(dr);
                        if (!GV_List.Rows[i].Cells[j + _firstDynColIndex].Controls.Contains(_editControl))
                            GV_List.Rows[i].Cells[j + _firstDynColIndex].Controls.Add(_editControl);
                    }
                }
            }
        }
    }

    private string GetDynControlID(int colIndex)
    {
        string controlID = "_editTextBox_" + colIndex.ToString();
        return controlID;
    }

    private string GetToolTip(IList<ItemFlowPlanTrack> ifptList)
    {
        StringBuilder detail = new StringBuilder("cssbody=[obbd] cssheader=[obhd] body=[<table width=100%>");
        detail.Append("<tr><td>" + "${Common.Business.Code}" + "</td><td>" + "${Common.Business.Description}" + "</td><td>" + "${Common.Business.ItemCode}" + "</td><td>" + "${Common.Business.ItemDescription}" + "</td><td>" + "${MRP.PlanViewType.Demand}" + "</td><td>" + "${MRP.Rate}" + "</td></tr>");
        foreach (ItemFlowPlanTrack ifpt in ifptList)
        {
            string party = ifpt.ReferencePlanDetail.ItemFlowPlan.Flow.Code;
            string flow = ifpt.ReferencePlanDetail.ItemFlowPlan.Flow.Description;
            string item = ifpt.ReferencePlanDetail.ItemFlowPlan.FlowDetail.Item.Code;
            string itemDesc = ifpt.ReferencePlanDetail.ItemFlowPlan.FlowDetail.Item.Description;
            string demand = ifpt.ReferencePlanDetail.PlanQty.ToString("0.########");
            string rate = ifpt.Rate.ToString("0.########");
            detail.Append("<tr><td>" + party + "</td><td>" + flow + "</td><td>" + item + "</td><td>" + itemDesc + "</td><td>" + demand + "</td><td>" + rate + "</td></tr>");
        }
        detail.Append("</table>]");
        return detail.ToString();
    }

}
