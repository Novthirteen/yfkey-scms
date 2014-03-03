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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using com.Sconit.Entity;

public partial class MasterData_WorkCenter_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected string WorkCenterCode
    {
        get
        {
            return (string)ViewState["WorkCenterCode"];
        }
        set
        {
            ViewState["WorkCenterCode"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string code)
    {
        this.WorkCenterCode = code;
        this.ODS_WorkCenter.SelectParameters["code"].DefaultValue = code;
        this.ODS_WorkCenter.DeleteParameters["code"].DefaultValue = code;
       
    }

    protected void FV_WorkCenter_DataBound(object sender, EventArgs e)
    {
        WorkCenter wc = TheWorkCenterMgr.LoadWorkCenter(this.WorkCenterCode);
        ((Literal)(this.FV_WorkCenter.FindControl("lbCurrentParty"))).Text = wc.Party.Code;
        Controls_TextBox tbType = (Controls_TextBox)(this.FV_WorkCenter.FindControl("tbType"));
        tbType.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_WORKCENTER_TYPE;
        tbType.DataBind();
        tbType.Text = wc.Type;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }


    protected void ODS_WorkCenter_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        WorkCenter workCenter = (WorkCenter)e.InputParameters[0];
        WorkCenter oldWC = TheWorkCenterMgr.LoadWorkCenter(WorkCenterCode);
        workCenter.Party = oldWC.Party;
        workCenter.Type = ((Controls_TextBox)(this.FV_WorkCenter.FindControl("tbType"))).Text;
    }

    protected void ODS_WorkCenter_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.WorkCenter.UpdateWorkCenter.Successfully", WorkCenterCode);
        btnBack_Click(this, e);
    }

    protected void ODS_WorkCenter_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.WorkCenter.DeleteWorkCenter.Successfully", WorkCenterCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.WorkCenter.DeleteWorkCenter.Fail", WorkCenterCode);
            e.ExceptionHandled = true;
        }
    }
  
}
