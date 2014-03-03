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
using com.Sconit.Service.Transportation;

public partial class Transportation_TransportPriceList_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lblTransportPriceListClickEvent;
    public event EventHandler lblWarehouseLeaseClickEvent;
    public event EventHandler lblOperationClickEvent;
    public event EventHandler lblTransportPriceListDetailClickEvent;

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

    public void UpdateView()
    {
        lbTransportPriceList_Click(this, null);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void ShowTab(bool isShow)
    {
        if (isShow)
        {
            this.tab_WarehouseLease.Visible = true;
            this.tab_Operation.Visible = true;
            this.lbTransportPriceListDetail.Text = "${Transportation.TransportPriceList.ShortCallCharges}";
        }
        else
        {
            this.tab_WarehouseLease.Visible = false;
            this.tab_Operation.Visible = false;
            this.lbTransportPriceListDetail.Text = "${Transportation.TransportPriceList.TransportPriceListDetail}";
        }
    }

    protected void lbTransportPriceList_Click(object sender, EventArgs e)
    {
        if (lblTransportPriceListClickEvent != null)
        {
            lblTransportPriceListClickEvent(this, e);

            this.tab_TransportPriceList.Attributes["class"] = "ajax__tab_active";
            this.tab_WarehouseLease.Attributes["class"] = "ajax__tab_inactive";
            this.tab_Operation.Attributes["class"] = "ajax__tab_inactive";
            this.tab_TransportPriceListDetail.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbWarehouseLease_Click(object sender, EventArgs e)
    {
        if (lblWarehouseLeaseClickEvent != null)
        {
            lblWarehouseLeaseClickEvent(this, e);

            this.tab_TransportPriceList.Attributes["class"] = "ajax__tab_inactive";
            this.tab_WarehouseLease.Attributes["class"] = "ajax__tab_active";
            this.tab_Operation.Attributes["class"] = "ajax__tab_inactive";
            this.tab_TransportPriceListDetail.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbOperation_Click(object sender, EventArgs e)
    {
        if (lblOperationClickEvent != null)
        {
            lblOperationClickEvent(this, e);

            this.tab_TransportPriceList.Attributes["class"] = "ajax__tab_inactive";
            this.tab_WarehouseLease.Attributes["class"] = "ajax__tab_inactive";
            this.tab_Operation.Attributes["class"] = "ajax__tab_active";
            this.tab_TransportPriceListDetail.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbTransportPriceListDetail_Click(object sender, EventArgs e)
    {
        if (lblTransportPriceListDetailClickEvent != null)
        {
            lblTransportPriceListDetailClickEvent(this, e);

            this.tab_TransportPriceList.Attributes["class"] = "ajax__tab_inactive";
            this.tab_WarehouseLease.Attributes["class"] = "ajax__tab_inactive";
            this.tab_Operation.Attributes["class"] = "ajax__tab_inactive";
            this.tab_TransportPriceListDetail.Attributes["class"] = "ajax__tab_active";
        }
    }
}
