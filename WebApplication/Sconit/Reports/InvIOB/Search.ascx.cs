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
using System.Collections.Generic;
using com.Sconit.Service.MasterData;
using Geekees.Common.Controls;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity.View;

public partial class Reports_InvIOB_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        this.tbLocation.ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ExportEvent != null)
        {
            object param = this.CollectParam();
            if (param != null)
                ExportEvent(param, null);
        }
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {
            object param = this.CollectParam();
            if (param != null)
            {
                SearchEvent(param, null);
            }
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Location"))
        {
            this.tbItem.Text = actionParameter["Location"];
        }
        if (actionParameter.ContainsKey("Item"))
        {
            this.tbItem.Text = actionParameter["Item"];
        }
        if (actionParameter.ContainsKey("StartDate"))
        {
            this.tbStartDate.Text = actionParameter["StartDate"];
        }
        if (actionParameter.ContainsKey("EndDate"))
        {
            this.tbEndDate.Text = actionParameter["EndDate"];
        }
    }

    private CriteriaParam CollectParam()
    {
        CriteriaParam criteriaParam = new CriteriaParam();
        criteriaParam.Location = this.tbLocation.Text.Trim() != string.Empty ? new string[] { this.tbLocation.Text.Trim() } : null;
        criteriaParam.Item = this.tbItem.Text.Trim() != string.Empty ? this.tbItem.Text.Trim() : null;

        if (this.tbStartDate.Text.Trim() != string.Empty)
            criteriaParam.StartDate = DateTime.Parse(this.tbStartDate.Text);
        if (this.tbEndDate.Text.Trim() != string.Empty)
            criteriaParam.EndDate = DateTime.Parse(this.tbEndDate.Text).AddDays(1);

        try
        {
            if (DateTime.Compare(Convert.ToDateTime(this.tbStartDate.Text.Trim()), Convert.ToDateTime(this.tbEndDate.Text.Trim())) > 0)
            {
                ShowErrorMessage("Common.StarDate.EndDate.Compare");
                return null;
            }
        }
        catch (Exception)
        {
            ShowErrorMessage("Common.Business.Error.DateInvalid");
            return null;
        }
        return criteriaParam;
    }

}
