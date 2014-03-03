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
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Utility;
using com.Sconit.Entity.Distribution;

public partial class Distribution_OrderIssue_FlowInfo : EditModuleBase
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
    public void InitPageParameter(InProcessLocation ip)
    {
        this.BindData(ip);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    private void BindData(InProcessLocation ip)
    {
        //PartyFrom
        this.tbPartyFrom.ReadonlyTextBox_DataBind(ip.PartyFrom.Code, ip.PartyFrom.Name);

        //PartyTo
        this.tbPartyTo.ReadonlyTextBox_DataBind(ip.PartyTo.Code, ip.PartyTo.Name);

        //ShipFrom
        if (ip.ShipFrom != null)
            this.tbShipFrom.ReadonlyTextBox_DataBind(ip.ShipFrom.Code, ip.ShipFrom.Address);

        //ShipTo
        if (ip.ShipTo != null)
            this.tbShipTo.ReadonlyTextBox_DataBind(ip.ShipTo.Code, ip.ShipTo.Address);

        //Carrier
        //todo

        //DockDescription
        this.tbDockDescription.ReadonlyTextBox_DataBind(null, ip.DockDescription);
    }

    public void InitPageParameter(string orderNo)
    {
        OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(orderNo);
        this.tbPartyFrom.ReadonlyTextBox_DataBind(orderHead.PartyFrom.Code, orderHead.PartyFrom.Name);
        this.tbPartyTo.ReadonlyTextBox_DataBind(orderHead.PartyTo.Code, orderHead.PartyTo.Name);
        if (orderHead.ShipFrom != null)
        {
            this.tbShipFrom.ReadonlyTextBox_DataBind(orderHead.ShipFrom.Code, orderHead.ShipFrom.Address);
        }
        if (orderHead.ShipTo != null)
        {
            this.tbShipTo.ReadonlyTextBox_DataBind(orderHead.ShipTo.Code, orderHead.ShipTo.Address);
        }
        if (orderHead.Carrier != null)
        {
            this.tbCarrier.ReadonlyTextBox_DataBind(orderHead.Carrier.Code, orderHead.Carrier.Name);
        }
        this.tbDockDescription.ReadonlyTextBox_DataBind(null, orderHead.DockDescription);
    }
}
