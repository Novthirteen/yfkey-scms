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
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Entity.View;

public partial class Reports_InvDetail_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;

    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbEffDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        this.InitialUI();
    }

    private void InitialUI()
    {
        this.tbLocation.ServiceParameter = "string:" + this.CurrentUser.Code;
          
        if (ModuleType == BusinessConstants.INVENTORY_REPORTS_INVDET)
        {
            this.lblEffDate.Visible = false;
            this.tbEffDate.Visible = false;
        }
        else if (ModuleType == BusinessConstants.INVENTORY_REPORTS_HISINV)
        {
            this.trLocation.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ModuleType == BusinessConstants.INVENTORY_REPORTS_INVDET)
        {
            this.rfvItemCode.Enabled = false;
        }
        if (!this.rfvItemCode.Enabled || this.rfvItemCode.IsValid)
        {
            this.DoSearch();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ExportEvent != null)
        {
            if (ModuleType == BusinessConstants.INVENTORY_REPORTS_INVDET)
            {
                this.rfvItemCode.Enabled = false;
            }
            if (!this.rfvItemCode.Enabled || this.rfvItemCode.IsValid)
            {
                ExportEvent(this.CollectParam(), null);
            }
        }
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {
            SearchEvent(this.CollectParam(), null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Location"))
        {
            this.tbLocation.Text = actionParameter["Location"];
        }
        if (actionParameter.ContainsKey("Item"))
        {
            this.tbItem.Text = actionParameter["Item"];
        }
        if (actionParameter.ContainsKey("EffDate"))
        {
            this.tbEffDate.Text = actionParameter["EffDate"];
        }
        if (actionParameter.ContainsKey("LocationType"))
        {
            this.tbType.Text = actionParameter["LocationType"];
        }
    }

    private object CollectParam()
    {
        CriteriaParam criteriaParam = new CriteriaParam();
        criteriaParam.LocationType = this.tbType.Text.Trim() != string.Empty?this.tbType.Text.Trim():null;
        criteriaParam.Location = this.tbLocation.Text.Trim() != string.Empty ? new string[] { this.tbLocation.Text.Trim() } : null;
        criteriaParam.Item = this.tbItem.Text.Trim() != string.Empty ? this.tbItem.Text.Trim() : null;
        if (this.tbEffDate.Text.Trim() != string.Empty && this.ModuleType == BusinessConstants.INVENTORY_REPORTS_HISINV)
            criteriaParam.EndDate = DateTime.Parse(this.tbEffDate.Text);
        bool cbPlan = this.cbPlan.Checked;
        //return criteriaParam;
        object obj = new object[] { criteriaParam, cbPlan };
        return obj;
    }
}
