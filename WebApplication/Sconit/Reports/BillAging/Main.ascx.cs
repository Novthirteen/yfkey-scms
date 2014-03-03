using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;
using NHibernate.Expression;

public partial class Reports_BillAging_Main : MainModuleBase
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

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucSearch.ExportEvent += new EventHandler(ucSearch_ExportEvent);
        this.ucList.DetailEvent += new EventHandler(ucSearch_DetailEvent);

        if (!IsPostBack)
        {
            this.ModuleType = this.ModuleParameter["ModuleType"];
            this.ucSearch.ModuleType = this.ModuleType;
            this.ucList.ModuleType = this.ModuleType;
            this.ucDetail.ModuleType = this.ModuleType;

            if (this.Action == BusinessConstants.PAGE_LIST_ACTION)
            {
                ucSearch.QuickSearch(this.ActionParameter);
            }
        }
    }

    void ucSearch_ExportEvent(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1], (IDictionary<string, string>)((object[])sender)[2]);
        this.ucList.Visible = true;
        this.ucList.UpdateView(); 
        this.ucList.Export();
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1], (IDictionary<string, string>)((object[])sender)[2]);
        this.ucList.Visible = true;
        this.ucList.UpdateView();
    }

    void ucSearch_DetailEvent(object sender, EventArgs e)
    {
        this.ucDetail.Visible = true;
        this.ucDetail.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1], (IDictionary<string, string>)((object[])sender)[2]);
    }
}
