using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;

public partial class MRP_ShiftPlan_Manual_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucSearch.SaveEvent += new EventHandler(Save_Render);
        this.ucSearch.GenOrdersEvent += new EventHandler(ucSearch_GenOrdersEvent);

        if (!IsPostBack)
        {
        }
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.Visible = true;
        this.ucList.ListView(sender);
    }

    void Save_Render(object sender, EventArgs e)
    {
        this.ucList.Save(sender);
    }

    void ucSearch_GenOrdersEvent(object sender, EventArgs e)
    {
        this.ucList.GenOrders(sender);
    }
}
