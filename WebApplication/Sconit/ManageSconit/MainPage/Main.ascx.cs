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
using com.Sconit.Entity;


public partial class MainPage_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Session["ACT"] = BusinessConstants.PAGE_LIST_ACTION;
        //this.Session["Temp_Session_ACT"] = BusinessConstants.PAGE_SEARCH_ACTION;
        //this.indexFrame.Visible = false;
    }
    protected void lbPOMonitoring_Click(object sender, EventArgs e)
    {
        this.tab_POMonitoring.Attributes["class"] = "ajax__tab_active";
        this.tab_DOMonitoring.Attributes["class"] = "ajax__tab_inactive";
        this.tab_WOMonitoring.Attributes["class"] = "ajax__tab_inactive";
        this.indexFrame.Attributes.Add("src", "Main.aspx?mid=Order.OrderHead.Procurement__mp--ModuleType-Procurement_ModuleSubType-Nml_StatusGroupId-7__act--ListAction__smp--none");
        }

    protected void lbWOMonitoring_Click(object sender, EventArgs e)
    {
        this.tab_POMonitoring.Attributes["class"] = "ajax__tab_inactive";
        this.tab_DOMonitoring.Attributes["class"] = "ajax__tab_inactive";
        this.tab_WOMonitoring.Attributes["class"] = "ajax__tab_active";
        this.indexFrame.Attributes.Add("src", "Main.aspx?mid=Order.OrderHead.Production__mp--ModuleType-Production_ModuleSubType-Nml_StatusGroupId-7__act--ListAction__smp--none");
    }

    protected void lbDOMonitoring_Click(object sender, EventArgs e)
    {
        this.tab_POMonitoring.Attributes["class"] = "ajax__tab_inactive";
        this.tab_DOMonitoring.Attributes["class"] = "ajax__tab_active";
        this.tab_WOMonitoring.Attributes["class"] = "ajax__tab_inactive";
        this.indexFrame.Attributes.Add("src", "Main.aspx?mid=Order.OrderHead.Distribution__mp--ModuleType-Distribution_ModuleSubType-Nml_StatusGroupId-7__act--ListAction__smp--none");
    }
}
