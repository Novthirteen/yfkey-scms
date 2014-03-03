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
using com.Sconit.Entity.Transportation;
using com.Sconit.Service.Transportation;

public partial class Transportation_Expense_New : NewModuleBase
{
    private Expense expense;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        Controls_TextBox tbCarrier = (Controls_TextBox)(this.FV_Expense.FindControl("tbCarrier"));
        tbCarrier.ServiceParameter = "string:" + this.CurrentUser.Code;
    }
    
    public void PageCleanup()
    {
        ((TextBox)(this.FV_Expense.FindControl("tbCode"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_Expense.FindControl("tbCarrier"))).Text = string.Empty;
        ((TextBox)(this.FV_Expense.FindControl("tbAmount"))).Text = "0";
        ((Controls_TextBox)(this.FV_Expense.FindControl("tbCurrency"))).Text = string.Empty;
        ((TextBox)(this.FV_Expense.FindControl("tbTaxCode"))).Text = string.Empty;
        ((CheckBox)(this.FV_Expense.FindControl("cbIsIncludeTax"))).Checked = false;
        ((TextBox)(this.FV_Expense.FindControl("tbRemark"))).Text = string.Empty;
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        switch (cv.ID)
        {
            case "cvCode":
                if (TheExpenseMgr.LoadExpense(args.Value) != null)
                {
                    ShowWarningMessage("Transportation.Expense.Code.Exists", args.Value);
                    args.IsValid = false;
                }
                break;
            case "cvCarrier":
                if (TheCarrierMgr.LoadCarrier(args.Value) == null)
                {
                    ShowWarningMessage("Transportation.Carrier.Code.NotExist", args.Value);
                    args.IsValid = false;
                }
                break;
            case "cvAmount":
                try
                {
                    Convert.ToDecimal(args.Value);
                }
                catch (Exception)
                {
                    ShowWarningMessage("Transportation.Expense.Amount.Error");
                    args.IsValid = false;
                }
                break;
            case "cvCurrency":
                if (TheCurrencyMgr.LoadCurrency(args.Value) == null)
                {
                    ShowWarningMessage("MasterData.Currency.Code.NotExist", args.Value);
                    args.IsValid = false;
                }
                break;
            default:
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_Expense_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Controls_TextBox tbCarrier = ((Controls_TextBox)(this.FV_Expense.FindControl("tbCarrier")));
        Controls_TextBox tbCurrency = ((Controls_TextBox)(this.FV_Expense.FindControl("tbCurrency")));

        expense = (Expense)e.InputParameters[0];
        expense.Code = expense.Code.Trim();
        expense.Carrier = TheCarrierMgr.LoadCarrier(tbCarrier.Text.Trim());
        expense.CreateDate = DateTime.Now;
        expense.CreateUser = this.CurrentUser;
        expense.TaxCode = expense.TaxCode.Trim();
        expense.Currency = TheCurrencyMgr.LoadCurrency(tbCurrency.Text.Trim());
        expense.Remark = expense.Remark.Trim();
    }

    protected void ODS_Expense_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(expense.Code, e);
            ShowSuccessMessage("Transportation.Expense.AddExpense.Successfully", expense.Code);
        }
    }
}
