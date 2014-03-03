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
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

public partial class MasterData_WorkCalendar_Workday_List : ListModuleBase
{
    public EventHandler EditEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string week = e.Row.Cells[3].Text;
            switch (week)
            {
                case "Monday":
                    e.Row.Cells[3].Text = "${Common.Week.Monday}";
                    break;
                case "Tuesday":
                    e.Row.Cells[3].Text = "${Common.Week.Tuesday}";
                    break;
                case "Wednesday":
                    e.Row.Cells[3].Text = "${Common.Week.Wednesday}";
                    break;
                case "Thursday":
                    e.Row.Cells[3].Text = "${Common.Week.Thursday}";
                    break;
                case "Friday":
                    e.Row.Cells[3].Text = "${Common.Week.Friday}";
                    break;
                case "Saturday":
                    e.Row.Cells[3].Text = "${Common.Week.Saturday}";
                    break;
                case "Sunday":
                    e.Row.Cells[3].Text = "${Common.Week.Sunday}";
                    break;
                default:
                    break;
            }
            CodeMaster type = TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_WORKCALENDAR_TYPE, e.Row.Cells[4].Text.Trim());
            if (type != null)
            {
                e.Row.Cells[4].Text = type.Description;
            }
        }
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string code = ((LinkButton)sender).CommandArgument;
            EditEvent(code, e);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string code = ((LinkButton)sender).CommandArgument;
        try
        {
            TheWorkdayMgr.DeleteWorkday(Convert.ToInt32(code), true);
            ShowSuccessMessage("MasterData.WorkCalendar.Delete.Successfully");
            UpdateView();
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.WorkCalendar.Delete.Failed");
        }
    }
}
