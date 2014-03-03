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

public partial class Reports_CycCntDiff_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
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
            object criteriaParam = CollectParam();
            SearchEvent(criteriaParam, null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Region"))
        {
            this.tbRegion.Text = actionParameter["Region"];
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
        if (actionParameter.ContainsKey("StorageBin"))
        {
            this.tbStorageBin.Text = actionParameter["StorageBin"];
        }
        if (actionParameter.ContainsKey("Item"))
        {
            this.tbItem.Text = actionParameter["Item"];
        }
        if (actionParameter.ContainsKey("OrderNo"))
        {
            this.tbOrderNo.Text = actionParameter["OrderNo"];
        }
    }

    private CriteriaParam CollectParam()
    {
        CriteriaParam criteriaParam = new CriteriaParam();
        criteriaParam.Party = this.tbRegion.Text.Trim() != string.Empty ? new string[] { this.tbRegion.Text.Trim() } : null;
        criteriaParam.Location = this.tbLocation.Text.Trim() != string.Empty ? new string[] { this.tbLocation.Text.Trim() } : null;
        if (this.tbStartDate.Text.Trim() != string.Empty)
            criteriaParam.StartDate = DateTime.Parse(this.tbStartDate.Text);
        if (this.tbEndDate.Text.Trim() != string.Empty)
            criteriaParam.EndDate = DateTime.Parse(this.tbEndDate.Text);
        criteriaParam.BinCode = this.tbStorageBin.Text.Trim() != string.Empty ? this.tbStorageBin.Text.Trim() : null;
        criteriaParam.Item = this.tbItem.Text.Trim() != string.Empty ? this.tbItem.Text.Trim() : null;
        criteriaParam.OrderNo = this.tbOrderNo.Text.Trim() != string.Empty ? this.tbOrderNo.Text.Trim() : string.Empty;

        criteriaParam.ClassifiedOrderNo = this.cbClassifiedOrderNo.Checked;
        criteriaParam.ClassifiedLocation = this.cbClassifiedLocation.Checked;
        criteriaParam.ClassifiedBin = this.cbClassifiedBin.Checked;
        criteriaParam.ClassifiedHuId = this.cbClassifiedHuId.Checked;
        return criteriaParam;
    }
}
