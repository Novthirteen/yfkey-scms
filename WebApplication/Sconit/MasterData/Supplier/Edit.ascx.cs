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

public partial class MasterData_Supplier_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected string SupplierCode
    {
        get
        {
            return (string)ViewState["SupplierCode"];
        }
        set
        {
            ViewState["SupplierCode"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string code)
    {
        this.SupplierCode = code;
        this.ODS_Supplier.SelectParameters["code"].DefaultValue = this.SupplierCode;
        this.ODS_Supplier.DeleteParameters["code"].DefaultValue = this.SupplierCode;
    }

    protected void FV_Supplier_DataBound(object sender, EventArgs e)
    {
       
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }


    protected void ODS_Supplier_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
       
    }

    protected void ODS_Supplier_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.Supplier.UpdateSupplier.Successfully", SupplierCode);
        
    }

    protected void ODS_Supplier_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.Supplier.DeleteSupplier.Successfully", SupplierCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.Supplier.DeleteSupplier.Fail", SupplierCode);
            e.ExceptionHandled = true;
        }
    }
  
}
