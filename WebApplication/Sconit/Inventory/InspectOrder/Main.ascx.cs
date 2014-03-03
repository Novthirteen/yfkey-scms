using System;
using System.Collections;
using System.Collections.Generic;
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
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Distribution;
using com.Sconit.Service.Distribution;

public partial class Inventory_InspectOrder_Main : MainModuleBase
{

    public Inventory_InspectOrder_Main()
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }

        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucList.EditEvent += new System.EventHandler(this.ListEdit_Render);
        this.ucInspectOrderInfo.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucSearch.NewEvent += new System.EventHandler(this.New_Render);
        this.ucNew.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucNew.CreateEvent += new System.EventHandler(this.ListEdit_Render);
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucList.Visible = true;
        this.ucList.isGroup = (bool)((object[])sender)[3];
        this.ucList.isExport = (bool)((object[])sender)[2];
        this.ucList.UpdateView();

    }

    //The event handler when user click link "Edit" link of ucList
    void ListEdit_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucNew.Visible = false;
        this.ucInspectOrderInfo.Visible = true;
        this.ucInspectOrderInfo.InitPageParameter((string)sender);
    }



    //The event handler when user click link "View" link of ucList
    void ListView_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucNew.Visible = false;
        this.ucInspectOrderInfo.Visible = true;
        this.ucInspectOrderInfo.InitPageParameter((string)sender);
    }

    //The event handler when user click link "Back" button of unRepackInfo
    void Back_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucInspectOrderInfo.Visible = false;
        this.ucNew.Visible = false;
        this.ucList.UpdateView();

    }

    //The event handler when user click link "New" button of unRepackInfo
    void New_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucInspectOrderInfo.Visible = false;
        this.ucNew.Visible = true;
        this.ucNew.UpdateView();
    }

}
