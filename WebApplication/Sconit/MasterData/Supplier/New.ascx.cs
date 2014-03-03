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
using com.Sconit.Utility;
using com.Sconit.Web;
using com.Sconit.Entity;

public partial class MasterData_Supplier_New : NewModuleBase
{
    private Supplier supplier;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public void PageCleanup()
    {
        ((TextBox)(this.FV_Supplier.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_Supplier.FindControl("tbName"))).Text = string.Empty;
        ((CheckBox)(this.FV_Supplier.FindControl("cbIsActive"))).Checked = true;
    }

    protected void ODS_Supplier_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        supplier = (Supplier)e.InputParameters[0];

    }

    protected void ODS_Supplier_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(supplier.Code, e);
            ShowSuccessMessage("MasterData.Supplier.AddSupplier.Successfully", supplier.Code);
        }
    }

    protected void checkSupplierExists(object source, ServerValidateEventArgs args)
    {
        string code = ((TextBox)(this.FV_Supplier.FindControl("tbCode"))).Text;

        if (TheSupplierMgr.LoadSupplier(code) != null)
        {
            args.IsValid = false;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        CustomValidator cvInsert = ((CustomValidator)(this.FV_Supplier.FindControl("cvInsert")));
        if (cvInsert.IsValid)
        {
            TextBox tbCode = (TextBox)(this.FV_Supplier.FindControl("tbCode"));
            TextBox tbName = (TextBox)(this.FV_Supplier.FindControl("tbName"));
            CheckBox cbIsActive = (CheckBox)(this.FV_Supplier.FindControl("cbIsActive"));
            Supplier supplier = new Supplier();
            supplier.Code = tbCode.Text.Trim();
            supplier.Name = tbName.Text.Trim();
            supplier.IsActive = cbIsActive.Checked;
            TheSupplierMgr.CreateSupplier(supplier, this.CurrentUser);
            if (CreateEvent != null)
            {
                CreateEvent(supplier.Code, e);
                ShowSuccessMessage("MasterData.Supplier.AddSupplier.Successfully", supplier.Code);
            }
        }
    }

}
