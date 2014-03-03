using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;

public partial class Order_ReceiptNotes_Main : MainModuleBase
{
    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    public string ModuleSubType
    {
        get { return (string)ViewState["ModuleSubType"]; }
        set { ViewState["ModuleSubType"] = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucList.ViewEvent += new System.EventHandler(this.ListView_Render);
        this.ucList.AdjustEvent += new System.EventHandler(this.ListAdjust_Render);
        this.ucAdjustMain.AdjustEvent += new System.EventHandler(this.ListView_Render);
        this.ucSearch.ExportEvent += new EventHandler(ucSearch_ExportEvent);

        if (!IsPostBack)
        {
            if (this.ModuleType == null)
            {
                this.ModuleType = this.ModuleParameter["ModuleType"];                
            }
            if (this.ModuleSubType == null && this.ModuleParameter.ContainsKey("ModuleSubType"))
            {
                this.ModuleSubType = this.ModuleParameter["ModuleSubType"];
            }
            if (this.ModuleParameter != null && this.ModuleParameter.ContainsKey("IsSupplier"))
            {
                this.ucSearch.IsSupplier = bool.Parse(this.ModuleParameter["IsSupplier"]);
                this.ucList.IsSupplier = this.ucSearch.IsSupplier;
            }
            this.ucSearch.ModuleType = this.ModuleType;
            this.ucList.ModuleType = this.ModuleType;
            this.ucViewMain.ModuleType = this.ModuleType;

            this.ucSearch.ModuleSubType = this.ModuleSubType;
            this.ucList.ModuleSubType = this.ModuleSubType;
            //this.ucViewMain.ModuleSubType = this.ModuleSubType;
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

    void ucSearch_ExportEvent(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucList.Visible = true;
        this.ucList.UpdateView();
        this.ucList.Export();
    }

    void ListView_Render(object sender, EventArgs e)
    {
        this.ucViewMain.Visible = true;
        this.ucViewMain.InitPageParameter((string)sender, false);
    }

    void ListAdjust_Render(object sender, EventArgs e)
    {
        this.ucAdjustMain.Visible = true;
        this.ucAdjustMain.InitPageParameter((string)sender);
    }
}
