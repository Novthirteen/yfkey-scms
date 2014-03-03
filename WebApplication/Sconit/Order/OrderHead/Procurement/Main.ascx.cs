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
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;

public partial class Order_OrderHead_Main : MainModuleBase
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

    public bool IsQuick
    {
        get
        {
            return (bool)ViewState["IsQuick"];
        }
        set
        {
            ViewState["IsQuick"] = value;
        }
    }

    //新品
    public bool NewItem
    {
        get
        {
            return (bool)ViewState["NewItem"];
        }
        set
        {
            ViewState["NewItem"] = value;
        }
    }

    //不合格品退货
    public bool IsReject
    {
        get
        {
            return (bool)ViewState["IsReject"];
        }
        set
        {
            ViewState["IsReject"] = value;
        }
    }

    public Order_OrderHead_Main()
    {
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new EventHandler(this.Search_Render);
        this.ucSearch.NewEvent += new EventHandler(this.New_Render);
        this.ucSearch.ExportEvent += new EventHandler(this.Search_ExportRender);
        this.ucList.EditEvent += new EventHandler(this.ListEdit_Render);
        this.ucNew.CreateEvent += new EventHandler(this.CreateBack_Render);
        this.ucNew.QuickCreateEvent += new EventHandler(this.QuickCreateBack_Render);
        this.ucQuickNew.QuickCreateEvent += new EventHandler(this.QuickCreateBack_Render);

        if (this.Action != BusinessConstants.PAGE_NEW_ACTION)
        {
            this.ucNew.BackEvent += new System.EventHandler(this.NewBack_Render);
            this.ucEdit.BackEvent += new System.EventHandler(this.EditBack_Render);
        }
        else
        {
            this.ucEdit.BackEvent += new System.EventHandler(this.New_Render);
        }

        if (!IsPostBack)
        {
            if (this.ModuleParameter.ContainsKey("ModuleType"))
            {
                this.ModuleType = this.ModuleParameter["ModuleType"];
            }
            if (this.ModuleParameter.ContainsKey("ModuleSubType"))
            {
                this.ModuleSubType = this.ModuleParameter["ModuleSubType"];
            }
            this.ucSearch.StatusGroupId = int.Parse(this.ModuleParameter["StatusGroupId"]);
            if (this.ModuleParameter.ContainsKey("IsQuick"))
            {
                this.IsQuick = bool.Parse(this.ModuleParameter["IsQuick"]);
            }
            else
            {
                this.IsQuick = false;
            }
            if (this.ModuleParameter.ContainsKey("IsReject"))
            {
                this.IsReject = bool.Parse(this.ModuleParameter["IsReject"]);
            }
            else
            {
                this.IsReject = false;
            }

            if (this.ModuleParameter.ContainsKey("NewItem"))
            {
                this.NewItem = bool.Parse(this.ModuleParameter["NewItem"]);
            }
            else
            {
                this.NewItem = false;
            }

            if (this.ModuleParameter != null && this.ModuleParameter.ContainsKey("IsSupplier"))
            {
                this.ucSearch.IsSupplier = bool.Parse(this.ModuleParameter["IsSupplier"]);
            }

            this.ucSearch.ModuleType = this.ModuleType;
            this.ucSearch.ModuleSubType = this.ModuleSubType;
            this.ucNew.ModuleType = this.ModuleType;
            this.ucNew.ModuleSubType = this.ModuleSubType;
            this.ucNew.IsQuick = this.IsQuick;
            this.ucNew.IsReject = this.IsReject;
            this.ucNew.NewItem = this.NewItem;
            this.ucEdit.ModuleType = this.ModuleType;
            this.ucEdit.ModuleSubType = this.ModuleSubType;
            this.ucViewReceipt.ModuleType = this.ModuleType;
            this.ucEdit.NewItem = this.NewItem;
            this.ucEdit.IsReject = this.IsReject;
            this.ucSearch.NewItem = this.NewItem;
            this.ucQuickNew.NewItem = this.NewItem;
            this.ucQuickNew.IsReject = this.IsReject;
            this.ucQuickNew.IsQuick = this.IsQuick;
            this.ucQuickNew.ModuleType = this.ModuleType;
            this.ucQuickNew.ModuleSubType = this.ModuleSubType;
   
            if (this.Action == BusinessConstants.PAGE_LIST_ACTION)
            {
                ucSearch.QuickSearch(this.ActionParameter);
            }
            if (this.Action == BusinessConstants.PAGE_NEW_ACTION)
            {
                New_Render(this, null);
            }
            if (this.Action == BusinessConstants.PAGE_EDIT_ACTION)
            {
                ListEdit_Render(this.ActionParameter["Code"], null);
            }

        }
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1], (IDictionary<string, string>)((object[])sender)[2]);
        this.ucList.Visible = true;
        this.ucList.isGroup = (bool)((object[])sender)[3];
        this.ucList.UpdateView();
    }

    void Search_ExportRender(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1], (IDictionary<string, string>)((object[])sender)[2]);
        this.ucList.Visible = true;
        this.ucList.isGroup = (bool)((object[])sender)[3];
        //this.ucList.UpdateView();
        this.ucList.Export();
    }

    //The event handler when user click button "New" button
    void New_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
         this.ucEdit.Visible = false;
         if (this.IsQuick)
         {
             this.ucQuickNew.Visible = true;
             this.ucNew.Visible = false;
             this.ucQuickNew.PageCleanup();
         }
         else
         {
             this.ucQuickNew.Visible = false;
             this.ucNew.Visible = true;
             this.ucNew.PageCleanup();
         }       
    }

    //The event handler when user click button "Back" button of ucNew
    void NewBack_Render(object sender, EventArgs e)
    {
        this.ucNew.Visible = false;
        this.ucSearch.Visible = true;
    }

    //The event handler when user click button "Confirm" button of ucNew
    void CreateBack_Render(object sender, EventArgs e)
    {
        string orderNo = (string)sender;
        this.ucNew.Visible = false;
        this.ucEdit.Visible = true;
        this.ucViewReceipt.Visible = false;
        this.ucEdit.InitPageParameter(orderNo);
    }

    //The event handler when user click button "Confirm" button of ucNew and isQuick
    void QuickCreateBack_Render(object sender, EventArgs e)
    {
        Receipt receipt = (Receipt)((object[])sender)[0];
        bool needPrintReceipt = (bool)((object[])sender)[1];
        this.ucQuickNew.PageCleanup();
        this.ucViewReceipt.Visible = true;
        this.ucViewReceipt.InitPageParameter(receipt, needPrintReceipt);
    }

    //The event handler when user click link "Edit" link of ucList
    void ListEdit_Render(object sender, EventArgs e)
    {
        string orderNo = (string)sender;
        this.ucEdit.Visible = true;
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucEdit.InitPageParameter(orderNo);
        OrderHead oH = TheOrderHeadMgr.LoadOrderHead(orderNo, false);
        this.ModuleSubType = oH.SubType;
        this.ModuleType = oH.Type;
    }

    //The event handler when user click button "Back" button of ucEdit
    void EditBack_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucList.UpdateView();
    }
}
