using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Service.Procurement;

public partial class MRP_ShiftPlan_Manual_Edit : ModuleBase
{
    public int ShiftPlanScheduleId
    {
        get { return ViewState["ShiftPlanScheduleId"] != null ? (int)ViewState["ShiftPlanScheduleId"] : 0; }
        set { ViewState["ShiftPlanScheduleId"] = value; }
    }
    public int FlowDetailId
    {
        get { return (int)ViewState["FlowDetailId"]; }
        set { ViewState["FlowDetailId"] = value; }
    }
    public DateTime ReqDate
    {
        get { return (DateTime)ViewState["ReqDate"]; }
        set { ViewState["ReqDate"] = value; }
    }
    public string ShiftCode
    {
        get { return (string)ViewState["ShiftCode"]; }
        set { ViewState["ShiftCode"] = value; }
    }
    public int ItemFlowPlanDetId
    {
        get { return ViewState["ItemFlowPlanDetId"] != null ? (int)ViewState["ItemFlowPlanDetId"] : 0; }
        set { ViewState["ItemFlowPlanDetId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    public void Control_DataBind()
    {
        if (ShiftPlanScheduleId > 0)
        {
            ShiftPlanSchedule sps = TheShiftPlanScheduleMgr.LoadShiftPlanSchedule(ShiftPlanScheduleId);
            this.tbPlanQty.Text = sps.PlanQty.ToString("0.########");
        }
    }

    public void Save()
    {
        Shift shift = TheShiftMgr.LoadShift(ShiftCode);
        decimal planQty = this.tbPlanQty.Text.Trim() != string.Empty ? decimal.Parse(this.tbPlanQty.Text) : 0;

        ShiftPlanSchedule sps = new ShiftPlanSchedule();
        if (ShiftPlanScheduleId > 0)
        {
            //Update
            sps = TheShiftPlanScheduleMgr.LoadShiftPlanSchedule(ShiftPlanScheduleId);
            if (sps.Shift == null)
                sps.Shift = shift;
            sps.PlanQty = planQty;
            TheShiftPlanScheduleMgr.UpdateShiftPlanSchedule(sps);
        }
        else
        {
            if (planQty > 0)
            {
                //Create
                sps.FlowDetail = TheFlowDetailMgr.LoadFlowDetail(FlowDetailId);
                sps.ReqDate = ReqDate;
                sps.Shift = shift;
                sps.Sequence = 0;//todo
                sps.PlanQty = planQty;
                sps.LastModifyDate = DateTime.Now;
                sps.LastModifyUser = this.CurrentUser;
                TheShiftPlanScheduleMgr.CreateShiftPlanSchedule(sps);
            }
        }
    }

    public void GenOrders()
    {
        if (ShiftPlanScheduleId > 0)
        {
            TheShiftPlanScheduleMgr.GenOrdersByShiftPlanScheduleId(ShiftPlanScheduleId, this.CurrentUser.Code);
        }
    }
}
