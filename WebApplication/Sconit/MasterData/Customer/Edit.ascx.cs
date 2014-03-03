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

public partial class MasterData_Customer_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected string CustomerCode
    {
        get
        {
            return (string)ViewState["CustomerCode"];
        }
        set
        {
            ViewState["CustomerCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string code)
    {
        this.CustomerCode = code;
        this.ODS_Customer.SelectParameters["code"].DefaultValue = this.CustomerCode;
        this.ODS_Customer.DeleteParameters["code"].DefaultValue = this.CustomerCode;
    }

    protected void FV_Customer_DataBound(object sender, EventArgs e)
    {
       
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }


    protected void ODS_Customer_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
       
    }

    protected void ODS_Customer_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.Customer.UpdateCustomer.Successfully", CustomerCode);
        
    }

    protected void ODS_Customer_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.Customer.DeleteCustomer.Successfully", CustomerCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.Customer.DeleteCustomer.Fail", CustomerCode);
            e.ExceptionHandled = true;
        }
    }
  
}
