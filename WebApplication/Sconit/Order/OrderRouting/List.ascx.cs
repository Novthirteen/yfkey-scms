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
using com.Sconit.Utility;

public partial class Order_OrderRouting_List : ListModuleBase
{
    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    public string ModuleSubType
    {
        get
        {
            return (string)ViewState["ModuleSubType"];
        }
        set
        {
            ViewState["ModuleSubType"] = value;
        }
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
        ArrayList dataList = (ArrayList)this.GV_List.DataSource;
        if (dataList == null || dataList.Count == 0)
        {
            this.Visible = false;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.lTitle.InnerText = FlowHelper.GetFlowRoutingLabel(this.ModuleType);
    }
}
