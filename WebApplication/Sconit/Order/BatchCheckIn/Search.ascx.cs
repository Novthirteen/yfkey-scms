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
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Utility;
using NHibernate.Expression;


public partial class Order_BatchCheckIn_Search : ModuleBase
{

    public event EventHandler SearchEvent;

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


    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblFlow.Text = FlowHelper.GetFlowLabel(this.ModuleType);
        this.tbFlow.ServiceMethod = FlowHelper.GetFlowServiceMethod(this.ModuleType);
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    protected void tbFlow_TextChanged(Object sender, EventArgs e)
    {
        if (this.tbFlow != null || this.tbFlow.Text.Trim() != string.Empty)
        {
            SearchEvent(this.tbFlow.Text.Trim(), null);
        }
    }
    protected void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        //todo
    }
   

}
