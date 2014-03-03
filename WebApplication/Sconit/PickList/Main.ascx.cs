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

public partial class Distribution_PickList_Main : MainModuleBase
{

    public Distribution_PickList_Main()
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }

        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucList.EditEvent += new System.EventHandler(this.ListEdit_Render);
        this.ucPickList.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucPickList.DeleteEvent += new System.EventHandler(this.Delete_Render);
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[1], (DetachedCriteria)((object[])sender)[2]);
        this.ucList.Visible = true;
        this.ucList.ListType = (int)(((object[])sender)[0]);
        this.ucList.IsExport = (bool)(((object[])sender)[3]);
        this.ucList.UpdateView();
    }

    //The event handler when user click link "Edit" link of ucList
    void ListEdit_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucPickList.Visible = true;
        this.ucPickList.InitPageParameter((string)sender);
    }


    void Back_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucPickList.Visible = false;
        this.ucList.UpdateView();
       // this.ucList.Visible = false;
    }

    void Delete_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucList.UpdateView();
        this.ucPickList.Visible = false;
        //this.ucList.Visible = false;
    }

}
