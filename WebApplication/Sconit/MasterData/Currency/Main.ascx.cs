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

public partial class MasterData_Currency_Main : MainModuleBase
{

    private string currencyCode = string.Empty;

    protected void GV_Currency_OnDataBind(object sender, EventArgs e)
    {
        this.fldGV.Visible = true;
        if (((GridView)(sender)).Rows.Count == 0)
        {
            this.lblResult.Visible = true;
        }
        else
        {
            this.lblResult.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.GV_Currency.DataBind();
        this.fldSearch.Visible = true;
        this.fldInsert.Visible = false;
        this.fldGV.Visible = true;
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        this.fldSearch.Visible = false;
        this.fldGV.Visible = false;
        this.fldInsert.Visible = true;
        ((TextBox)(this.FV_Currency.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_Currency.FindControl("tbName"))).Text = string.Empty;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.fldSearch.Visible = true;
        this.fldGV.Visible = true;
        this.fldInsert.Visible = false;
        //this.GV_Currency.DataBind();
    }

    protected void ODS_GV_Currency_OnUpdating(object source, ObjectDataSourceMethodEventArgs e)
    {
        Currency currency = (Currency)e.InputParameters[0];
        currency.Name = currency.Name.Trim();
    }
    protected void ODS_Currency_Inserting(object source, ObjectDataSourceMethodEventArgs e)
    {
        string code = ((TextBox)(this.FV_Currency.FindControl("tbCode"))).Text;
        string name = ((TextBox)(this.FV_Currency.FindControl("tbName"))).Text;
        if (code == null || code.Trim() == string.Empty) 
        {
            ShowWarningMessage("MasterData.Currency.Code.Empty", "");
            e.Cancel = true;
            return;
        }
        if (name == null || name.Trim() == string.Empty)
        {
            ShowWarningMessage("MasterData.Currency.Name.Empty", "");
            e.Cancel = true;
            return;
        }
        if (TheCurrencyMgr.LoadCurrency(code) == null)
        {
            ShowSuccessMessage("MasterData.Currency.AddCurrency.Successfully", code);
        }
        else 
        {
            e.Cancel = true;
            ShowErrorMessage("MasterData.Currency.CodeExist", code);
            return;
        }
    }

    protected void ODS_Currency_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        btnSearch_Click(this, null);
    }

    protected void ODS_GV_Currency_OnDeleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Currency currency = (Currency)e.InputParameters[0];
        currencyCode = currency.Code;
    }

    protected void ODS_GV_Currency_OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            ShowSuccessMessage("MasterData.Currency.DeleteCurrency.Successfully", currencyCode);
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.Currency.DeleteCurrency.Fail", currencyCode);
        }
    }

}
