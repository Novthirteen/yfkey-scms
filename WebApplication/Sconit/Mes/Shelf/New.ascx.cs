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
using com.Sconit.Entity;
using com.Sconit.Entity.Mes;

public partial class Mes_Shelf_New : NewModuleBase
{
    private Shelf shelf;

    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;



    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void PageCleanup()
    {
        ((TextBox)(this.FV_Shelf.FindControl("tbCode"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_Shelf.FindControl("tbProductLine"))).Text = string.Empty;
        ((TextBox)(this.FV_Shelf.FindControl("tbTagNo"))).Text = string.Empty;
        ((TextBox)(this.FV_Shelf.FindControl("tbCapacity"))).Text = string.Empty;
        ((CheckBox)(this.FV_Shelf.FindControl("cbIsActive"))).Checked = true;
      
        Controls_TextBox tbProductLine = (Controls_TextBox)this.FV_Shelf.FindControl("tbProductLine");
        tbProductLine.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true";
        tbProductLine.DataBind();
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        TextBox tbCode = (TextBox)this.FV_Shelf.FindControl("tbCode");
        Shelf shelf = TheShelfMgr.LoadShelf(tbCode.Text.Trim());
        if (shelf != null)
        {
            args.IsValid = false;
        }
    }

    protected void ODS_Shelf_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Controls_TextBox tbProductLine = (Controls_TextBox)this.FV_Shelf.FindControl("tbProductLine");
        TextBox tbCapacity = (TextBox)this.FV_Shelf.FindControl("tbCapacity");

        shelf = (Shelf)e.InputParameters[0];
        shelf.Code = shelf.Code.Trim();
        shelf.ProductLine = TheFlowMgr.LoadFlow(tbProductLine.Text.Trim());
        shelf.TagNo = shelf.TagNo.Trim();
    }

    protected void ODS_Shelf_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(shelf.Code, e);
            ShowSuccessMessage("Mes.Shelf.Insert.Successfully", shelf.Code);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
}
