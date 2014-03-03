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
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

public partial class Order_GoodsReceipt_OrderReceipt_Main : MainModuleBase
{
    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucList.EditEvent += new System.EventHandler(this.ListEdit_Render);
        this.ucProductionReceiveMain.RefreshListEvent += new System.EventHandler(this.Receive_Render);
        this.ucProductionReceiveMain.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucReceiveMain.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucReceiveMain.ReceiveEvent += new EventHandler(this.Receive_Render);
        this.ucReceiptView.BackEvent += new EventHandler(this.ReceiptViewBack_Render);
        if (!IsPostBack)
        {
            this.ucSearch.ModuleType = this.ModuleType;
            this.ucList.ModuleType = this.ModuleType;
            this.ucProductionReceiveMain.ModuleType = this.ModuleType;
            this.ucReceiptView.ModuleType = this.ModuleType;
            
            if (this.Session["Temp_Session_OrderNo"] != null)
            {
                ListEdit_Render(this.Session["Temp_Session_OrderNo"], null);
                Session.Contents.Remove("Temp_Session_OrderNo");
            }
        }
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucList.Visible = true;
        this.ucList.UpdateView();
    }

    //The event handler when user click link "Edit" link of ucList
    void ListEdit_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        if (this.ModuleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            this.ucReceiveMain.Visible = true;
            this.ucReceiveMain.InitPageParameter((string)sender);
        }
        else
        {
            this.ucProductionReceiveMain.Visible = true;
            this.ucProductionReceiveMain.InitPageParameter((string)sender);
        }

    }

    void RefreshList_Render(object sender, EventArgs e)
    {
        ShowSuccessMessage("MasterData.Order.OrderHead.Receive.Successfully", (string)sender);
        this.ucReceiveMain.Visible = false;
        this.ucList.Visible = true;
        this.ucSearch.Visible = true;
        this.ucProductionReceiveMain.Visible = false;
        this.ucList.UpdateView();
    }

    void Back_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucReceiveMain.Visible = false;
        this.ucProductionReceiveMain.Visible = false;
    }

    void Receive_Render(object sender, EventArgs e)
    {
        this.ucReceiptView.Visible = true;
        this.ucReceiveMain.Visible = false;
        this.ucList.Visible = true;
        this.ucSearch.Visible = true;
        this.ucProductionReceiveMain.Visible = false;
        this.ucList.UpdateView();
        this.ucReceiptView.InitPageParameter((string)((object[])sender)[0], (bool)((object[])sender)[1]);
    }

    void ReceiptViewBack_Render(object sender, EventArgs e)
    {
        this.ucReceiveMain.Visible = false;
        this.ucProductionReceiveMain.Visible = false;
        this.ucSearch.Visible = true;
        this.ucSearch.QuickSearch(this.ActionParameter);
    }

}
