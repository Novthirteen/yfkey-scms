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

public partial class MasterData_Bom_BomView_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);

        if (!IsPostBack)
        {
            if (this.Action == BusinessConstants.PAGE_LIST_ACTION)
            {
                Search_Render(sender, null);
            }
        }
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        string viewType = ((object[])sender)[2].ToString().ToLower();
        if (viewType == "tree")
        {
            this.ucList.Visible = false;
            this.ucTreeView.Visible = true;
            this.ucTreeView.ShowTreeView(sender);
        }
        else
        {
            this.ucList.Visible = true;
            this.ucList.ListView(sender);
            this.ucTreeView.Visible = false;
        }
    }
}
