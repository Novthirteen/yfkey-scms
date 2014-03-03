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
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using com.Sconit.Utility;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

public partial class MasterData_WorkCalendar_WorkCalendarView_List : ListModuleBase
{
    public EventHandler EditEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public override void UpdateView()
    {
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            #region text format
            string week = e.Row.Cells[1].Text;
            switch (week)
            {
                case "Monday":
                    e.Row.Cells[1].Text = "${Common.Week.Monday}";
                    break;
                case "Tuesday":
                    e.Row.Cells[1].Text = "${Common.Week.Tuesday}";
                    break;
                case "Wednesday":
                    e.Row.Cells[1].Text = "${Common.Week.Wednesday}";
                    break;
                case "Thursday":
                    e.Row.Cells[1].Text = "${Common.Week.Thursday}";
                    break;
                case "Friday":
                    e.Row.Cells[1].Text = "${Common.Week.Friday}";
                    break;
                case "Saturday":
                    e.Row.Cells[1].Text = "${Common.Week.Saturday}";
                    break;
                case "Sunday":
                    e.Row.Cells[1].Text = "${Common.Week.Sunday}";
                    break;
                default:
                    break;
            }
            CodeMaster type = TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_WORKCALENDAR_TYPE, e.Row.Cells[5].Text.Trim());
            if (type != null)
            {
                e.Row.Cells[5].Text = type.Description;
            }
            #endregion

            //#region border
            //e.Row.Cells[0].Attributes.Add("style", "border:1px   solid   blue");
            //e.Row.Cells[1].Attributes.Add("style", "border:1px   solid   blue");
            //e.Row.Cells[2].Attributes.Add("style", "border:1px   solid   blue");
            //e.Row.Cells[3].Attributes.Add("style", "border:1px   solid   blue");
            //e.Row.Cells[4].Attributes.Add("style", "border:1px   solid   blue");
            //e.Row.Cells[5].Attributes.Add("style", "border:1px   solid   blue");
            //#endregion

            #region add class 休息日和工作日以不同的背景色区分
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((String)(DataBinder.Eval(e.Row.DataItem, "Type")) == BusinessConstants.CODE_MASTER_WORKCALENDAR_TYPE_VALUE_WORK)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Attributes.Add("class", "GVRow");
                    }
                }
                else
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Attributes.Add("class", "GVAlternatingRow");
                    }
                }
            }
            #endregion
        }
    }

    public void ListView(object sender)
    {
        string region = ((object[])sender)[0].ToString();
        string workcenter = ((object[])sender)[1].ToString();
        string para_starttime = ((object[])sender)[2].ToString();
        string para_endtime = ((object[])sender)[3].ToString();
        DateTime starttime = DateTime.Now;
        DateTime endtime = DateTime.Now;

        if (region != "")
        {
            Region r = TheRegionMgr.LoadRegion(region);
            if (r == null)
            {
                ShowWarningMessage("MasterData.WorkCalendar.WarningMessage.RegionInvalid", region);
                return;
            }
        }
        if (workcenter != "")
        {
            WorkCenter r = TheWorkCenterMgr.LoadWorkCenter(workcenter);
            if (r == null)
            {
                ShowWarningMessage("MasterData.WorkCalendar.WarningMessage.WorkCenterInvalid", workcenter);
                return;
            }
        }
        try
        {
            starttime = Convert.ToDateTime(para_starttime);
        }
        catch (Exception)
        {
            ShowWarningMessage("MasterData.WorkCalendar.WarningMessage.StartTimeInvalid");
            return;
        }
        try
        {
            endtime = Convert.ToDateTime(para_endtime);
        }
        catch (Exception)
        {
            ShowWarningMessage("MasterData.WorkCalendar.WarningMessage.EndTimeInvalid");
            return;
        }
        if (DateTime.Compare(starttime, endtime) > 0)
        {
            ShowWarningMessage("MasterData.WorkCalendar.WarningMessage.TimeCompare");
            return;
        }

        List<WorkCalendar> workCalendars = TheWorkCalendarMgr.GetWorkCalendar(starttime, endtime, region, workcenter);
        this.GV_List.DataSource = workCalendars;
        this.GV_List.DataBind();

        GridViewHelper.GV_MergeTableCell(GV_List, new int[] { 0, 1 });
    }

}
