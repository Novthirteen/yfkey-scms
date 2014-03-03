using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.View;
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class Reports_LocTrans_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbTransType.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE;
        this.tbLocation.ServiceParameter = "string:" + this.CurrentUser.Code;
        this.tbRefLoc.ServiceParameter = "string:" + this.CurrentUser.Code;
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (this.rfvItemCode.IsValid)
        {
            DateTime startDate = DateTime.Parse(tbStartDate.Text.Trim());
            DateTime endDate = DateTime.Parse(tbEndDate.Text.Trim());
            if (startDate.AddMonths(1) < endDate)
            {
                ShowWarningMessage("MasterData.LocTrans.DatePeriodMustBeInaMonth");
                return;
            }
            this.DoSearch();
        }
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {
            object criteriaParam = CollectParam();
            if (criteriaParam != null)
            {
                SearchEvent(criteriaParam, null);
            }
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ExportEvent != null)
        {
            if (this.rfvItemCode.IsValid)
            {

                DateTime startDate = DateTime.Parse(tbStartDate.Text.Trim());
                DateTime endDate = DateTime.Parse(tbEndDate.Text.Trim());
                if (startDate.AddMonths(1) < endDate)
                {
                    ShowWarningMessage("MasterData.LocTrans.DatePeriodMustBeInaMonth");
                    return;
                }
                object param = this.CollectParam();
                if (param != null)
                {
                    ExportEvent(param, null);
                }
            }
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("TransType"))
        {
            this.tbTransType.Text = actionParameter["TransType"];
        }
        if (actionParameter.ContainsKey("Location"))
        {
            this.tbLocation.Text = actionParameter["Location"];
        }
        if (actionParameter.ContainsKey("StartDate"))
        {
            this.tbStartDate.Text = actionParameter["StartDate"];
        }
        if (actionParameter.ContainsKey("EndDate"))
        {
            this.tbEndDate.Text = actionParameter["EndDate"];
        }
        if (actionParameter.ContainsKey("Item"))
        {
            this.tbItem.Text = actionParameter["Item"];
        }
    }


    private CriteriaParam CollectParam()
    {
        CriteriaParam criteriaParam = new CriteriaParam();
        criteriaParam.TransactionType = this.tbTransType.Text.Trim() != string.Empty ? this.tbTransType.Text.Trim() : string.Empty;
        criteriaParam.Location = this.tbLocation.Text.Trim() != string.Empty ? new string[] { this.tbLocation.Text.Trim() } : null;
        if (this.tbStartDate.Text.Trim() != string.Empty)
            criteriaParam.StartDate = DateTime.Parse(this.tbStartDate.Text);
        if (this.tbEndDate.Text.Trim() != string.Empty)
            criteriaParam.EndDate = DateTime.Parse(this.tbEndDate.Text);
        criteriaParam.Item = this.tbItem.Text.Trim() != string.Empty ? this.tbItem.Text.Trim() : null;
        //djin 20120802 目标库存
        if (this.tbRefLoc.Visible == true && tbRefLoc.Text.Trim() != string.Empty)
            criteriaParam.RefLoc = new string[] { tbRefLoc.Text.Trim() };
        criteriaParam.ClassifiedDate = this.cbSumDate.Checked;
        //criteriaParam.ClassifiedTransType = this.cbSumType.Checked;
        //criteriaParam.ClassifiedLocation = this.cbSumLocation.Checked;
        criteriaParam.ClassifiedUser = this.cbSumUser.Checked;

        return criteriaParam;

    }
}
