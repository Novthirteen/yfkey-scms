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

public partial class Inventory_Repack_Main : MainModuleBase
{
    public string RepackType
    {
        get
        {
            return (string)ViewState["RepackType"];
        }
        set
        {
            ViewState["RepackType"] = value;
        }
    }

    public Inventory_Repack_Main()
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.ModuleParameter.ContainsKey("Type"))
            {
                this.RepackType = this.ModuleParameter["Type"];

            }

            this.ucSearch.RepackType = this.RepackType;
            this.ucList.RepackType = this.RepackType;
            this.ucRepackDetailList.RepackType = this.RepackType;
            this.ucRepackInfo.RepackType = this.RepackType;
            this.ucViewRepackDetailList.RepackType = this.RepackType;
            this.ucRepackDetailList.IsQty = false;
        }


        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucSearch.NewEvent += new System.EventHandler(this.New_Render);
        this.ucSearch.RepackHuEvent += new System.EventHandler(this.RepackHu_Render);
        this.ucRepackInfo.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucList.ViewEvent += new System.EventHandler(this.ListView_Render);
        this.ucRepackDetailList.RepackEvent += new System.EventHandler(this.ListView_Render);
        this.ucRepackDetailList.BackEvent += new System.EventHandler(this.Back_Render);

    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucList.Visible = true;
        this.ucViewRepackDetailList.Visible = false;
        this.ucRepackDetailList.Visible = false;
        this.ucList.UpdateView();
    }

    //The event handler when user click button "Search" button
    void New_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucRepackInfo.Visible = false;
        this.ucRepackDetailList.Visible = true;
        this.ucRepackDetailList.InitPageParameter();
        this.ucViewRepackDetailList.Visible = false;
    }

    //The event handler when user click button "Search" button
    void RepackHu_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucRepackInfo.Visible = false;
        this.ucRepackDetailList.Visible = true;
        this.ucRepackDetailList.InitPageParameter(true);
        this.ucViewRepackDetailList.Visible = false;
    }


    //The event handler when user click link "View" link of ucList
    void ListView_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucRepackDetailList.Visible = false;
        this.ucRepackInfo.Visible = true;
        this.ucRepackInfo.InitPageParameter((string)sender);
        this.ucViewRepackDetailList.Visible = true;
        this.ucViewRepackDetailList.InitPageParameter((string)sender);
    }

    //The event handler when user click link "Back" button of unRepackInfo
    void Back_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucRepackInfo.Visible = false;
        this.ucRepackDetailList.Visible = false;
        this.ucViewRepackDetailList.Visible = false;
    }

}
