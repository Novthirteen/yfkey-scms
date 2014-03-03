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

public partial class MasterData_WorkCalendar_Shift_Edit : EditModuleBase
{
    private Shift shift;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    protected string code
    {
        get { return (string)ViewState["code"]; }
        set { ViewState["code"] = value; }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(code, e);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    public void InitPageParameter(string code)
    {
        this.code = code;
        this.ODS_Shift.SelectParameters["code"].DefaultValue = code;
        this.ODS_Shift.DeleteParameters["code"].DefaultValue = code;
        this.FV_Shift.DataBind();
    }

    protected void FV_Shift_DataBound(object sender, EventArgs e)
    {
    }

    protected void ODS_Shift_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Shift shift = (Shift)e.InputParameters[0];
    }

    protected void ODS_Shift_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.WorkCalendar.Update.Successfully");
    }

    protected void ODS_Shift_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.WorkCalendar.Delete.Successfully");
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.WorkCalendar.Delete.Failed");
            e.ExceptionHandled = true;
        }
    }
}
