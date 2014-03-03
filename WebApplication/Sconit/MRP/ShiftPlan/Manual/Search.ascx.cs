using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;

public partial class MRP_ShiftPlan_Manual_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler SaveEvent;
    public event EventHandler GenOrdersEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.ucShift.Date = DateTime.Today;
        }

        this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {
            SearchEvent(this.GetParam(), null);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveEvent != null)
        {
            SaveEvent(this.GetParam(), null);
        }
    }

    protected void btnGenOrders_Click(object sender, EventArgs e)
    {
        if (GenOrdersEvent != null)
        {
            GenOrdersEvent(this.GetParam(), null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
    }

    protected void tbRegion_TextChanged(object sender, EventArgs e)
    {
        this.BindShift();
    }

    protected void tbDate_TextChanged(object sender, EventArgs e)
    {
        this.BindShift();
    }

    private void BindShift()
    {
        this.ucShift.BindList(DateTime.Parse(this.tbDate.Text), this.tbRegion.Text.Trim());
    }

    private object GetParam()
    {
        string region = this.tbRegion.Text.Trim() != string.Empty ? this.tbRegion.Text.Trim() : string.Empty;
        string flowCode = this.tbFlow.Text.Trim() != string.Empty ? this.tbFlow.Text.Trim() : string.Empty;
        DateTime date = DateTime.Parse(this.tbDate.Text);
        string shiftCode = this.ucShift.ShiftCode;
        string itemCode = this.tbItemCode.Text.Trim() != string.Empty ? this.tbItemCode.Text.Trim() : string.Empty;

        return new object[] { region, flowCode, date, shiftCode, itemCode, new List<Shift>() };
    }
}
