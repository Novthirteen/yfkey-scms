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

public partial class MRP_PlanSchedule_Main : MainModuleBase
{
    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucSearch.ReleaseEvent += new System.EventHandler(this.Release_Render);
        this.ucSearch.RunEvent += new System.EventHandler(this.Run_Render);
        this.ucSearch.SaveEvent += new EventHandler(this.Save_Render);
        this.ucList.ListEvent += new EventHandler(this.List_Render);

        if (!IsPostBack)
        {
            this.ucSearch.ModuleType = this.ModuleType;
            this.ucList.ModuleType = this.ModuleType;
        }
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.Visible = true;
        this.ucList.criteria = (object[])sender;
        this.ucList.ListView();
    }

    //The event handler when user click button "Save" button
    void Save_Render(object sender, EventArgs e)
    {
        this.ucList.Save();
    }

    //The event handler when user click button "Release" button
    void Release_Render(object sender, EventArgs e)
    {
        this.ucList.Release();
    }

    //The event handler when user click button "Run" button
    void Run_Render(object sender, EventArgs e)
    {
        this.ucList.Run();
    }

    void List_Render(object sender, EventArgs e)
    {
        bool show = (bool)((object[])sender)[0];
        this.ucSearch.ShowHideActionButton(show);
    }
}
