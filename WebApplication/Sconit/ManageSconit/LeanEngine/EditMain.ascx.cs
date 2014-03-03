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
using com.Sconit.Entity;
using com.Sconit.Web;
using NHibernate.Expression;

public partial class ManageSconit_LeanEngine_EditMain : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucTabNavigator.lbViewClickEvent += new EventHandler(ucTabNavigator_lbViewClickEvent);
        this.ucTabNavigator.lbSetupClickEvent += new EventHandler(ucTabNavigator_lbSetupClickEvent);

        if (this.Action == BusinessConstants.PAGE_LIST_ACTION)
        {
            ucSearch.QuickSearch(this.ActionParameter);
        }
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucList.Visible = true;
        this.ucList.UpdateView();
    }

    //The event handler when user click "View" button
    void ucTabNavigator_lbViewClickEvent(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucEdit.Visible = false;
    }

    //The event handler when user click "Setup" button
    void ucTabNavigator_lbSetupClickEvent(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucEdit.Visible = true;
    }

}
