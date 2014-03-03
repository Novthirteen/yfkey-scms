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
using NHibernate.Expression;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

public partial class Order_GoodsReceipt_AsnReceipt_Main : MainModuleBase
{
    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }
    public string Action
    {
        get { return (string)ViewState["Action"]; }
        set { ViewState["Action"] = value; }
    }

    public string AsnType
    {
        get { return (string)ViewState["AsnType"]; }
        set { ViewState["AsnType"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucList.EditEvent += new System.EventHandler(this.ListEdit_Render);
        this.ucList.ViewEvent += new System.EventHandler(this.ListView_Render);
        //this.ucList.CloseEvent += new System.EventHandler(this.ListClose_Render);
        this.ucEditMain.RefreshListEvent += new System.EventHandler(this.RefreshList_Render);
        this.ucEditMain.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucViewMain.CloseEvent += new System.EventHandler(this.Back_Render);

        if (!IsPostBack)
        {
            if (this.ModuleType == null)
            {
                this.ModuleType = this.ModuleParameter["ModuleType"];
                this.Action = this.ModuleParameter["Action"];                
            }
            if (this.ModuleParameter != null && this.ModuleParameter.ContainsKey("IsSupplier"))
            {
                this.ucSearch.IsSupplier = bool.Parse(this.ModuleParameter["IsSupplier"]);
                this.ucList.IsSupplier = this.ucSearch.IsSupplier;
            }
            if (this.ModuleParameter != null && this.ModuleParameter.ContainsKey("AsnType"))
            {
                this.AsnType = this.ModuleParameter["AsnType"];
            }
            else
            {
                this.AsnType = BusinessConstants.CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_NORMAL;
            }
            this.ucSearch.ModuleType = this.ModuleType;
            this.ucSearch.Action = this.Action;
            this.ucList.ModuleType = this.ModuleType;
            this.ucList.Action = this.Action;
            this.ucEditMain.ModuleType = this.ModuleType;
            this.ucViewMain.ModuleType = this.ModuleType;
            this.ucSearch.AsnType = this.AsnType;
            this.ucList.AsnType = this.AsnType;
            this.ucViewMain.AsnType = this.AsnType;
        }
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucList.Visible = true;
        this.ucList.IsExport = (bool)((object[])sender)[2];
        this.ucList.IsGroup = (bool)((object[])sender)[3];
        this.ucList.UpdateView();
    }

    //The event handler when user click link "Edit" link of ucList
    void ListEdit_Render(object sender, EventArgs e)
    {
        this.ucEditMain.Visible = true;
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucEditMain.InitPageParameter((string)sender, "Receive");
    }

    void ListView_Render(object sender, EventArgs e)
    {
        this.ucViewMain.Visible = true;
        this.ucViewMain.InitPageParameter((string)sender);
    }

    void ListClose_Render(object sender, EventArgs e)
    {
        this.ucViewMain.Visible = true;
        this.ucViewMain.InitPageParameter((string)sender,"Edit");
    }


    void RefreshList_Render(object sender, EventArgs e)
    {
        this.ucEditMain.Visible = false;
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucList.UpdateView();
    }

    void Back_Render(object sender, EventArgs e)
    {
        this.ucEditMain.Visible = false;
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucSearch.QuickSearch(this.ActionParameter);
    }
}
