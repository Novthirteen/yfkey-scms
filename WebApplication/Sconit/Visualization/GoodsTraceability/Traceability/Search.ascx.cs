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
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;

public partial class Visualization_Traceability_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {
            string HuId = this.tbHuId.Text.Trim() != string.Empty ? this.tbHuId.Text.Trim() : string.Empty;
            if (HuId == string.Empty)
                return;

            SearchEvent(HuId, null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("HuId"))
        {
            this.tbHuId.Text = actionParameter["HuId"];
        }
    }
}
