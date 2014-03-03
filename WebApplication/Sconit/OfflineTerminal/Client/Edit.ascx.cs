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
using com.Sconit.Control;

public partial class MasterData_Client_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected string ClientId
    {
        get
        {
            return (string)ViewState["ClientId"];
        }
        set
        {
            ViewState["ClientId"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        
    }

    protected void FV_Client_DataBound(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(string ClientId)
    {
        this.ClientId = ClientId;
        this.ODS_Client.SelectParameters["ClientId"].DefaultValue = this.ClientId;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_Client_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.Client.UpdateClient.Successfully", ClientId);
        btnBack_Click(this, e);
    }

    protected void ODS_Client_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {

    }

    protected void ODS_Client_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.Client.DeleteClient.Successfully", ClientId);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.Client.DeleteClient.Fail", ClientId);
            e.ExceptionHandled = true;
        }
    }
}
