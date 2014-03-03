using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity;

public partial class Order_OrderIssue_EditMain : MainModuleBase
{
    public event EventHandler CreatePickListEvent;
    public event EventHandler BackEvent;
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
    public string ModuleSubType
    {
        get
        {
            return (string)ViewState["ModuleSubType"];
        }
        set
        {
            ViewState["ModuleSubType"] = value;
        }
    }
    public bool IsSupplier
    {
        get { return ViewState["IsSupplier"] != null ? (bool)ViewState["IsSupplier"] : false; }
        set { ViewState["IsSupplier"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ucFlowInfo.ModuleType = this.ModuleType;
            this.ucList.ModuleType = this.ModuleType;
            this.ucSearch.ModuleType = this.ModuleType;

            this.ucList.ModuleSubType = this.ModuleSubType;
            this.ucSearch.ModuleSubType = this.ModuleSubType;

            this.ucSearch.IsSupplier = this.IsSupplier;

            if (this.Session["Temp_Session_OrderNo"] != null && this.Session["Temp_Session_OrderNo"] != string.Empty)
            {
                List<string> orderNoList = new List<string>();
                orderNoList.Add((string)this.Session["Temp_Session_OrderNo"]);
                ListEdit_Render(new object[] { orderNoList }, null);
                Session.Contents.Remove("Temp_Session_OrderNo");
            }
        }

        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucList.EditEvent += new System.EventHandler(this.ListEdit_Render);
        this.ucSupplierList.EditEvent += new System.EventHandler(this.ListEdit_Render);
        this.ucList.CreatePickListEvent += new System.EventHandler(this.ListCreatePickList_Render);
        this.ucDetailMain.ShipEvent += new System.EventHandler(this.SaveDetail_Render);
        this.ucDetailMain.ShipBackEvent += new System.EventHandler(this.DetailBack_Render);
        this.ucDetailMain.BindInfoEvent += new EventHandler(this.DetailBindInfo_Render);
        this.ucDetailMain.CreatePickListEvent += new System.EventHandler(this.CreatePickList_Render);
        this.ucViewShipList.BackEvent += new EventHandler(this.ViewBack_Render);
        this.ucPickListInfo.BackEvent += new EventHandler(this.PickListInfoBack_Render);
        this.ucPickListInfo.DeleteEvent += new EventHandler(this.PickListInfoBack_Render);
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        //if (this.ModuleType == BusinessConstants.ORDER_MODULETYPE_VALUE_SUPPLIERDISTRIBUTION)
        if (this.IsSupplier)
        {
            this.ucList.Visible = false;
            this.ucSupplierList.Visible = true;
            this.ucSupplierList.InitPageParameter((string)((object[])sender)[0], (string)((object[])sender)[1]);
        }
        else
        {
            this.ucSupplierList.Visible = false;
            this.ucList.Visible = true;
            this.ucList.InitPageParameter((string)((object[])sender)[0], (string)((object[])sender)[1]);
        }
    }


    //The event handler when user click link "Edit" link of ucList
    void ListEdit_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucFlowInfo.Visible = true;
        this.ucList.Visible = false;
        this.ucSupplierList.Visible = false;
        this.ucDetailMain.Visible = true;
        this.ucViewShipList.Visible = false;
        this.ucFlowInfo.InitPageParameter(((List<string>)((object[])sender)[0])[0]);
        this.ucDetailMain.InitPageParameter((List<string>)((object[])sender)[0]);
    }

    void SaveDetail_Render(object sender, EventArgs e)
    {
        this.ucViewShipList.Visible = true;
        this.ucViewShipList.InitPageParameter((string)((object[])sender)[0], (bool)((object[])sender)[1]);
    }

    void DetailBack_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucFlowInfo.Visible = false;
        //if (this.ModuleType == BusinessConstants.ORDER_MODULETYPE_VALUE_SUPPLIERDISTRIBUTION)
        if (this.IsSupplier)
        {
            this.ucSupplierList.Visible = true; 
        }
        else
        {
            this.ucList.Visible = true;
        }
        this.ucSearch.QuickSearch(new Dictionary<string, string>());
      
    }

    void DetailBindInfo_Render(object sender, EventArgs e)
    {
        this.ucFlowInfo.Visible = true;
        this.ucFlowInfo.InitPageParameter((string)((object[])sender)[0]);
    }

    void ViewBack_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucSearch.QuickSearch(this.ActionParameter);
        this.ucFlowInfo.Visible = false;
        this.ucDetailMain.Visible = false;
        this.ucViewShipList.Visible = false;
    }

    void CreatePickList_Render(object sender, EventArgs e)
    {
        this.ucFlowInfo.Visible = false;
        this.ucDetailMain.Visible = false;
        this.ucPickListInfo.Visible = true;
        this.ucPickListInfo.InitPageParameter((string)sender);
        if (CreatePickListEvent != null)
        {
            CreatePickListEvent(sender, e);
        }
    }

    void ListCreatePickList_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucSupplierList.Visible = false;
        this.ucFlowInfo.Visible = true;
        this.ucDetailMain.Visible = true;
        this.ucViewShipList.Visible = false;
        this.ucFlowInfo.InitPageParameter(((List<string>)((object[])sender)[0])[0]);
        this.ucDetailMain.InitPageParameter((List<string>)((object[])sender)[0], true);
    }

    void PickListInfoBack_Render(object sender, EventArgs e)
    {
        this.ucPickListInfo.Visible = false;
        this.ucSearch.Visible = true;
        this.ucSearch.QuickSearch(this.ActionParameter);
        if (BackEvent != null)
        {
            BackEvent(sender, e);
        }

    }
}
