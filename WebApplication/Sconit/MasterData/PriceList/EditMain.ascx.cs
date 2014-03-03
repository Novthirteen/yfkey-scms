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

public partial class MasterData_PriceList_PriceList_EditMain : MainModuleBase
{
    public event EventHandler BackEvent;

    protected string code
    {
        get
        {
            return (string)ViewState["code"];
        }
        set
        {
            ViewState["code"] = value;
        }
    }

    public string PriceListType;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucPriceListMain.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucPriceListDetailMain.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucTabNavigator.lbPriceListClickEvent += new System.EventHandler(this.TabPriceListClick_Render);
        this.ucTabNavigator.lbPriceListDetailClickEvent += new System.EventHandler(this.TabPriceListDetailClick_Render);

        if (!IsPostBack)
        {
            this.ucPriceListMain.Visible = true;
            this.ucPriceListDetailMain.Visible = false;
        }
        this.ucPriceListMain.PriceListType = this.PriceListType;
    }

    //The event handler when user click link button to "PriceList" tab
    void TabPriceListClick_Render(object sender, EventArgs e)
    {
        this.ucPriceListMain.Visible = true;
        this.ucPriceListDetailMain.Visible = false;
    }

    //The event handler when user click link button to "PriceListDetail" tab
    void TabPriceListDetailClick_Render(object sender, EventArgs e)
    {
        this.ucPriceListMain.Visible = false;
        this.ucPriceListDetailMain.Visible = true;
        //if (this.ucPriceListMain.PriceListCode != null)
        //{
        //    this.ucPriceListDetailMain.PriceListCode = this.ucPriceListMain.PriceListCode;
        this.ucPriceListDetailMain.InitPageParameter(code);
        //}
    }

    void Back_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    public void InitPageParameter(string code)
    {
        this.code = code;
        this.ucPriceListMain.InitPageParameter(code);
        this.ucTabNavigator.UpdateView();
        //this.ODS_PriceList.SelectParameters["Code"].DefaultValue = code;
        //this.ODS_PriceList.DeleteParameters["Code"].DefaultValue = code;
        //this.UpdateView();
    }
}
