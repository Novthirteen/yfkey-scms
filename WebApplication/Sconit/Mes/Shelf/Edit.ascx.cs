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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Control;
using com.Sconit.Entity;
using com.Sconit.Service.Procurement;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Mes;

public partial class Mes_Shelf_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    private string Code
    {
        get
        {
            return (string)ViewState["Code"];
        }
        set
        {
            ViewState["Code"] = value;
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {

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
        this.Code = code;
        this.ODS_Shelf.SelectParameters["Code"].DefaultValue = code;
        this.ODS_Shelf.DeleteParameters["Code"].DefaultValue = code;

       
    }

    protected void FV_Shelf_DataBound(object sender, EventArgs e)
    {
        if (this.Code != null && this.Code != string.Empty)
        {
            Shelf shelf = TheShelfMgr.LoadShelf(this.Code);
            this.UpdateView(shelf);
        }
    }

    private void UpdateView(Shelf shelf)
    {
        Controls_TextBox tbProductLine = (Controls_TextBox)this.FV_Shelf.FindControl("tbProductLine");
        tbProductLine.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true";
        tbProductLine.DataBind();
        tbProductLine.Text = shelf.ProductLine.Code;
    }

    protected void ODS_Shelf_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Shelf oldShelef = TheShelfMgr.LoadShelf(this.Code);
        Shelf shelf = (Shelf)e.InputParameters[0];
        Controls_TextBox tbProductLine = (Controls_TextBox)this.FV_Shelf.FindControl("tbProductLine");
        shelf.ProductLine = TheFlowMgr.LoadFlow(tbProductLine.Text.Trim());
        shelf.Capacity = oldShelef.Capacity;
        shelf.CurrentCartons = oldShelef.CurrentCartons;
        shelf.OriginalCartonNo = oldShelef.OriginalCartonNo;
        shelf.Code = oldShelef.Code;

    }

    protected void ODS_Shelf_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("Mes.Shelf.Update.Successfully", Code);
    }

    protected void ODS_Shelf_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            if (BackEvent != null)
            {
                btnBack_Click(this, e);
            }
            ShowSuccessMessage("Mes.Shelf.Delete.Successfully", Code);
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.Shelf.Delete.Failed", Code);

        }
    }
}
