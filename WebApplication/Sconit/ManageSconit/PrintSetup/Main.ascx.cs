using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

public partial class ManageSconit_PrintSetup_Main : MainModuleBase
{
    private List<Permission> AutoPrintMonitorList
    {
        get { return (List<Permission>)ViewState["AutoPrintMonitorList"]; }
        set { ViewState["AutoPrintMonitorList"] = value; }
    }
    private HttpCookie autoPrintCookie;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.autoPrintCookie = new HttpCookie("autoPrintCookie");
        Response.Cookies["autoPrintCookie"].Expires = DateTime.Now.AddDays(7) ;   
        if (!Page.IsPostBack)
        {
            this.AutoPrintMonitorList = new List<Permission>();

            foreach (Permission permission in this.CurrentUser.PagePermission)
            {
                if (permission.Category.Code == BusinessConstants.PERMISSION_PAGE_VALUE_AUTOPRINT)
                {
                    if (Request.Cookies["autoPrintCookie"] != null && Request.Cookies["autoPrintCookie"][permission.Code] != null)
                    {
                        string status = Request.Cookies["autoPrintCookie"][permission.Code].ToString();
                        bool isStart = true;
                        if (status.ToLower() == "false")
                        {
                            isStart = false;
                        }
                        permission.Status = isStart;
                    }
                    this.AutoPrintMonitorList.Add(permission);
                }
            }

            this.GV_List.DataSource = AutoPrintMonitorList;
            this.GV_List.DataBind();
        }
    }

    protected void lbnControl_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        string Code = linkButton.CommandArgument;
        foreach (Permission autoPrintMonitor in this.AutoPrintMonitorList)
        {
            if (autoPrintMonitor.Code == Code)
            {
                autoPrintMonitor.Status = !autoPrintMonitor.Status;
                break;
            }
        }
        this.GV_List.DataSource = AutoPrintMonitorList;
        this.GV_List.DataBind();
    }

    protected void GV_List_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal ltlCurrentStatus = (Literal)e.Row.Cells[0].FindControl("ltlCurrentStatus");
            Literal ltlStatus = (Literal)e.Row.Cells[0].FindControl("ltlStatus");
            HiddenField hfCode = (HiddenField)e.Row.Cells[1].FindControl("hfCode");
            LinkButton lbtnStart = (LinkButton)e.Row.Cells[2].FindControl("lbtnStart");
            LinkButton lbtnStop = (LinkButton)e.Row.Cells[2].FindControl("lbtnStop");

            autoPrintCookie.Values[hfCode.Value] = ltlCurrentStatus.Text.ToLower();
            Response.Cookies.Add(autoPrintCookie);

            if (ltlCurrentStatus.Text.ToLower() == "false")
            {
                lbtnStart.Visible = true;
                lbtnStop.Visible = false;
                ltlStatus.Text = "${Print.Setup.CurrentStatus}:" + "${Print.Setup.Status.NoRun}";
            }
            else
            {
                lbtnStart.Visible = false;
                lbtnStop.Visible = true;
                ltlStatus.Text = "${Print.Setup.CurrentStatus}:" + "${Print.Setup.Status.Run}";
            }
        }
    }

}
