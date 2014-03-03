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
using com.Sconit.Entity.Transportation;

public partial class Transportation_Expense_Edit : EditModuleBase
{
    private Expense expense;
    public event EventHandler BackEvent;

    protected string ExpenseCode
    {
        get
        {
            return (string)ViewState["ExpenseCode"];
        }
        set
        {
            ViewState["ExpenseCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string code)
    {
        this.ExpenseCode = code;
        this.ODS_Expense.SelectParameters["code"].DefaultValue = this.ExpenseCode;
        this.ODS_Expense.DeleteParameters["code"].DefaultValue = this.ExpenseCode;
    }

    protected void FV_Expense_DataBound(object sender, EventArgs e)
    {
        if (ExpenseCode != null && ExpenseCode != string.Empty)
        {
            Expense expense = (Expense)((FormView)sender).DataItem;

            Button buttonDelete = (Button)this.FV_Expense.FindControl("Button2");

            if (expense.IsReferenced)
            {
                buttonDelete.Visible = false;
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_Expense_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        expense = (Expense)e.InputParameters[0];

        expense.Code = expense.Code.Trim();
        expense.Remark = expense.Remark.Trim();
        expense.LastModifyUser = this.CurrentUser;
    }

    protected void ODS_Expense_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("Transportation.Expense.UpdateExpense.Successfully", ExpenseCode);
        
    }

    protected void ODS_Expense_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Transportation.Expense.DeleteExpense.Successfully", ExpenseCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Transportation.Expense.DeleteExpense.Fail", ExpenseCode);
            e.ExceptionHandled = true;
        }
    }
}
