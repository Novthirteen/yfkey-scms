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

public partial class MasterData_Supplier_EditMain : System.Web.UI.UserControl
{
    public event EventHandler BackEvent;

    public string SupplierCode
    {
        get
        {
            return (string)ViewState["SupplierCode"];
        }
        set
        {
            ViewState["SupplierCode"] = value;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        this.ucEdit.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucWorkCenter.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucBillAddress.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucShipAddress.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucTabNavigator.lblSupplierClickEvent += new System.EventHandler(this.TabSupplierClick_Render);
        this.ucTabNavigator.lblWorkCenterClickEvent += new System.EventHandler(this.TabWorkCenterClick_Render);
        this.ucTabNavigator.lblBillAddressClickEvent += new System.EventHandler(this.TabBillAddressClick_Render);
        this.ucTabNavigator.lblShipAddressClickEvent += new System.EventHandler(this.TabShipAddressClick_Render);
    }

    public void InitPageParameter(string code)
    {
        this.SupplierCode = code;
        this.ucTabNavigator.Visible = true;
        this.ucEdit.Visible = true;
        this.ucEdit.InitPageParameter(code);
        this.ucWorkCenter.Visible = false;
        this.ucTabNavigator.UpdateView();
    }

    protected void Back_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void TabSupplierClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = true;
        this.ucWorkCenter.Visible = false;
        this.ucBillAddress.Visible = false;
        this.ucShipAddress.Visible = false;
    }

    protected void TabWorkCenterClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucWorkCenter.Visible = true;
        this.ucBillAddress.Visible = false;
        this.ucShipAddress.Visible = false;
        this.ucWorkCenter.ParentCode = SupplierCode;
        this.ucWorkCenter.InitPageParameter();
    }

    protected void TabBillAddressClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucWorkCenter.Visible = false;
        this.ucBillAddress.Visible = true;
        this.ucShipAddress.Visible = false;
        this.ucBillAddress.PartyCode = this.SupplierCode;
        this.ucBillAddress.AddrType = BusinessConstants.PARTY_ADDRESS_TYPE_BILL_ADDRESS;
        this.ucBillAddress.InitPageParameter();
    }

    protected void TabShipAddressClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucWorkCenter.Visible = false;
        this.ucBillAddress.Visible = false;
        this.ucShipAddress.Visible = true;
        this.ucShipAddress.PartyCode = this.SupplierCode;
        this.ucShipAddress.AddrType = BusinessConstants.PARTY_ADDRESS_TYPE_SHIP_ADDRESS;
        this.ucShipAddress.InitPageParameter();
    }
}
