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

public partial class MasterData_Client_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucTabNavigator.lbOnlineClickEvent += new System.EventHandler(this.TabOnlineClick_Render);
        this.ucTabNavigator.lbOfflineClickEvent += new System.EventHandler(this.TabOfflineClick_Render);
        this.ucOnlineSearch.SearchEvent += new System.EventHandler(this.OnlineSearch_Render);
        this.ucOfflineSearch.SearchEvent += new System.EventHandler(this.OfflineSearch_Render);
        this.ucOfflineList.ViewEvent += new System.EventHandler(this.OfflineListView_Render);
        this.ucOfflineView.BackEvent += new System.EventHandler(this.OfflineViewBack_Render);

        if (!IsPostBack)
        {
            if (this.Action == BusinessConstants.PAGE_LIST_ACTION)
            {
                ucOnlineSearch.QuickSearch(this.ActionParameter);
            }
        }
    }

    //The event handler when user click button "Search" button
    void OnlineSearch_Render(object sender, EventArgs e)
    {
        this.ucOnlineList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucOnlineList.Visible = true;
        this.ucOnlineList.UpdateView();
        this.CleanMessage();
    }

    //The event handler when user click button "Search" button
    void OfflineSearch_Render(object sender, EventArgs e)
    {
        this.ucOfflineList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucOfflineList.Visible = true;
        this.ucOfflineList.UpdateView();
        this.CleanMessage();
    }

    private void TabOnlineClick_Render(object sender, EventArgs e)
    {
        this.ucOnlineList.Visible = false;
        this.ucOnlineSearch.Visible = true;
        this.ucOfflineSearch.Visible = false;
        this.ucOfflineList.Visible = false;
        this.ucOfflineView.Visible = false;
    }

    private void TabOfflineClick_Render(object sender, EventArgs e)
    {
        this.ucOnlineList.Visible = false;
        this.ucOnlineSearch.Visible = false;
        this.ucOfflineSearch.Visible = true;
        this.ucOfflineList.Visible = false;
        this.ucOfflineView.Visible = false;
    }


    void OfflineListView_Render(object sender, EventArgs e)
    {
        //this.ucOnlineList.Visible = false;
        //this.ucOnlineSearch.Visible = false;
        //this.ucOfflineSearch.Visible = false;
        //this.ucOfflineList.Visible = false;

        this.ucOfflineView.InitPageParameter((string)sender);
        this.ucOfflineView.Visible = true;
    }

    void OfflineViewBack_Render(object sender, EventArgs e)
    {
        this.ucOfflineView.Visible = false;
        this.ucOfflineSearch.Visible = true;
        this.ucOfflineList.Visible = true;
    
    }
}
