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
using System.Collections.Generic;

public partial class Visualization_Traceability_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);

        if (!IsPostBack)
        {
            if (this.Action == BusinessConstants.PAGE_LIST_ACTION)
            {
                ucSearch.QuickSearch(this.ActionParameter);
            }
        }
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = true;
        this.ucEdit.InitPageParameter((string)sender);
        this.ucList.Visible = true;
        this.ucList.InitPageParameter((string)sender);
        //this.ucInvList.Visible = true;
        //this.ucInvList.InitPageParameter((string)sender);
    }

    public void QuickSearch(object sender, EventArgs e)
    {
        this.ActionParameter = new Dictionary<string, string>();
        this.ActionParameter.Add("HuId", (string)((object[])sender)[0]);

        this.ucSearch.QuickSearch(this.ActionParameter);
    }
}
