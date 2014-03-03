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
using NHibernate.Expression;

public partial class Reports_InvDetail_Main : MainModuleBase
{
    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucSearch.ExportEvent += new EventHandler(ucSearch_ExportEvent);

        if (!IsPostBack)
        {
            this.ModuleType = this.ModuleParameter["ModuleType"];
            this.ucSearch.ModuleType = this.ModuleType;
            this.ucList.ModuleType = this.ModuleType;

            if (this.Action == BusinessConstants.PAGE_LIST_ACTION)
            {
                ucSearch.QuickSearch(this.ActionParameter);
            }
        }
    }

    void ucSearch_ExportEvent(object sender, EventArgs e)
    {
        if (this.ModuleType == BusinessConstants.INVENTORY_REPORTS_HISINV)
        {
            this.ucHisInvList.InitPageParameter(sender);
            this.ucHisInvList.Visible = true;
            this.ucHisInvList.Export();
        }
        else
        {
            this.ucList.InitPageParameter(sender);
            this.ucList.Visible = true;
            this.ucList.Export();
        }
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        if (this.ModuleType == BusinessConstants.INVENTORY_REPORTS_HISINV)
        {
            this.ucHisInvList.Visible = true;
            this.ucHisInvList.InitPageParameter(sender);
            //this.ucHisInvList.UpdateView();
        }
        else
        {
            this.ucList.Visible = true;
            this.ucList.InitPageParameter(sender);
            //this.ucList.UpdateView();
        }
    }
}
