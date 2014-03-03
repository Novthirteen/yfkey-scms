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
using System.Data;

public partial class MRP_ShiftPlan_Manual_List : ModuleBase
{
    private const int _firstDynColIndex = 6;

    public List<string> shiftIdList
    {
        get { return (List<string>)ViewState["ShiftIdList"]; }
        set { ViewState["ShiftIdList"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            int i = 0;
            foreach (string code in shiftIdList)
            {
                MRP_ShiftPlan_Manual_Edit _editControl = (MRP_ShiftPlan_Manual_Edit)e.Row.FindControl(this.GetDynControlID(i));
                _editControl.FlowDetailId = (int)drv["FlowDetailId"];
                _editControl.ReqDate = (DateTime)drv["ReqDate"];
                _editControl.ShiftCode = code;
                _editControl.ItemFlowPlanDetId = drv["ItemFlowPlanDetId"] != DBNull.Value ? (int)drv["ItemFlowPlanDetId"] : 0;

                string colName = "DynCol_" + i.ToString();
                if (drv[colName] != DBNull.Value)
                {
                    _editControl.ShiftPlanScheduleId = (int)drv[colName];
                    _editControl.Control_DataBind();
                }

                i++;
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            if (shiftIdList != null && shiftIdList.Count > 0)
            {
                int i = 0;
                foreach (string code in shiftIdList)
                {
                    Shift shift = TheShiftMgr.LoadShift(code);
                    int colIndex = i + _firstDynColIndex;
                    e.Row.Cells[colIndex].Text = shift.ShiftName;
                    i++;
                }
            }
        }
    }

    public void ListView(object sender)
    {
        string region = (string)(((object[])sender)[0]);
        string flowCode = (string)(((object[])sender)[1]);
        DateTime date = (DateTime)(((object[])sender)[2]);
        string code = (string)(((object[])sender)[3]);
        string itemCode = (string)(((object[])sender)[4]);
        IList<Shift> shiftList = (IList<Shift>)(((object[])sender)[5]);
        if (shiftList != null && shiftList.Count > 0)
        {
            shiftIdList = new List<string>();
            foreach (Shift shift in shiftList)
            {
                shiftIdList.Add(shift.Code);
            }
        }

        IList<ShiftPlanSchedule> spsList = TheShiftPlanScheduleMgr.GetShiftPlanScheduleList(region, flowCode, date, code, itemCode, this.CurrentUser.Code);
        DataTable dt = TheShiftPlanScheduleMgr.ConvertShiftPlanScheduleToDataTable(spsList, shiftList);

        GV_List.DataSource = dt;
        GV_List.DataBind();

        this.HideColumns(shiftList);
    }

    public void Save(object sender)
    {
        string region = (string)(((object[])sender)[0]);
        string flowCode = (string)(((object[])sender)[1]);
        DateTime date = (DateTime)(((object[])sender)[2]);
        string shiftCode = (string)(((object[])sender)[3]);
        string itemCode = (string)(((object[])sender)[4]);
        IList<Shift> shiftList = (IList<Shift>)(((object[])sender)[5]);

        if (GV_List.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in GV_List.Rows)
            {
                int i = 0;
                foreach (Shift shift in shiftList)
                {
                    MRP_ShiftPlan_Manual_Edit _editControl = (MRP_ShiftPlan_Manual_Edit)gvr.FindControl(this.GetDynControlID(i));
                    _editControl.Save();
                    i++;
                }
            }
        }

        ShowSuccessMessage("Common.Business.Result.Save.Successfully");
    }

    public void GenOrders(object sender)
    {
        string region = (string)(((object[])sender)[0]);
        string flowCode = (string)(((object[])sender)[1]);
        DateTime date = (DateTime)(((object[])sender)[2]);
        string shiftCode = (string)(((object[])sender)[3]);
        string itemCode = (string)(((object[])sender)[4]);
        IList<Shift> shiftList = (IList<Shift>)(((object[])sender)[5]);

        if (GV_List.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in GV_List.Rows)
            {
                int i = 0;
                foreach (Shift shift in shiftList)
                {
                    if (shift.Code == shiftCode)
                    {
                        MRP_ShiftPlan_Manual_Edit _editControl = (MRP_ShiftPlan_Manual_Edit)gvr.FindControl(this.GetDynControlID(i));
                        _editControl.GenOrders();
                        i++;
                    }
                }
            }
        }

        ShowSuccessMessage("Common.Business.Result.Insert.Successfully");
    }

    private string GetDynControlID(int colIndex)
    {
        return "ucEdit_" + colIndex.ToString();
    }

    private void HideColumns(IList<Shift> shiftList)
    {
        if (GV_List.Rows.Count > 0)
        {
            if (shiftList != null && shiftList.Count > 0)
            {
                for (int i = _firstDynColIndex + shiftList.Count; i < GV_List.Columns.Count; i++)
                {
                    GV_List.Columns[i].Visible = false;
                }
            }
        }
    }
}
