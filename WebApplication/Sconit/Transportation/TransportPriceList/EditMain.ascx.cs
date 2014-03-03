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
using com.Sconit.Entity;
using com.Sconit.Web;
using com.Sconit.Utility;
using com.Sconit.Entity.Transportation;

public partial class Transportation_TransportPriceList_EditMain : MainModuleBase
{
    public event EventHandler BackEvent;

    public string TransportPriceListCode
    {
        get
        {
            return (string)ViewState["TransportPriceListCode"];
        }
        set
        {
            ViewState["TransportPriceListCode"] = value;
        }
    }

    public string PartyType
    {
        get
        {
            return (string)ViewState["PartyType"];
        }
        set
        {
            ViewState["PartyType"] = value;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucEdit.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucWarehouseLease.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucOperation.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucTransportPriceListDetail.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucTabNavigator.lblTransportPriceListClickEvent += new System.EventHandler(this.TabTransportPriceListClick_Render);
        this.ucTabNavigator.lblWarehouseLeaseClickEvent += new System.EventHandler(this.TabWarehouseLeaseClick_Render);
        this.ucTabNavigator.lblOperationClickEvent += new System.EventHandler(this.TabOperationClick_Render);
        this.ucTabNavigator.lblTransportPriceListDetailClickEvent += new System.EventHandler(this.TabTransportPriceListDetailClick_Render);
    }

    public void InitPageParameter(string code)
    {
        this.TransportPriceListCode = code;
        this.ucTabNavigator.Visible = true;
        this.ucEdit.Visible = true;
        this.ucEdit.InitPageParameter(code);
        SetPartyType();
        ShowTabKit();
        this.ucTabNavigator.UpdateView();
    }

    protected void Back_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void TabTransportPriceListClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = true;
        this.ucWarehouseLease.Visible = false;
        this.ucOperation.Visible = false;
        this.ucTransportPriceListDetail.Visible = false;
    }

    protected void TabWarehouseLeaseClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucWarehouseLease.Visible = true;
        this.ucOperation.Visible = false;
        this.ucTransportPriceListDetail.Visible = false;

        this.ucWarehouseLease.TransportPriceListCode = this.TransportPriceListCode;
        this.ucWarehouseLease.InitPageParameter();
    }

    protected void TabOperationClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucWarehouseLease.Visible = false;
        this.ucOperation.Visible = true;
        this.ucTransportPriceListDetail.Visible = false;

        this.ucOperation.TransportPriceListCode = this.TransportPriceListCode;
        this.ucOperation.InitPageParameter();
    }

    protected void TabTransportPriceListDetailClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucWarehouseLease.Visible = false;
        this.ucOperation.Visible = false;
        this.ucTransportPriceListDetail.Visible = true;

        this.ucTransportPriceListDetail.TransportPriceListCode = this.TransportPriceListCode;
        this.ucTransportPriceListDetail.PartyType = this.PartyType;
        this.ucTransportPriceListDetail.InitPageParameter();
    }

    private void SetPartyType()
    {
        TransportPriceList transportPriceList = TheTransportPriceListMgr.LoadTransportPriceList(this.TransportPriceListCode);
        this.PartyType = transportPriceList.Party.Type;
    }

    private void ShowTabKit()
    {
        if (this.PartyType != BusinessConstants.PARTY_TYPE_CARRIER)
        {
            this.ucTabNavigator.ShowTab(true);
        }
        else
        {
            this.ucTabNavigator.ShowTab(false);
        }
    }
}
