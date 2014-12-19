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

    //报废
    public bool IsScrap
    {
        get
        {
            return (bool)ViewState["IsScrap"];
        }
        set
        {
            ViewState["IsScrap"] = value;
        }
    }

    //原材料回用
    public bool IsReuse
    {
        get
        {
            return (bool)ViewState["IsReuse"];
        }
        set
        {
            ViewState["IsReuse"] = value;
        }
    }



    public Order_OrderHead_Main()
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucSearch.NewEvent += new System.EventHandler(this.New_Render);
        this.ucList.EditEvent += new System.EventHandler(this.ListEdit_Render);
        this.ucNew.QuickCreateEvent += new System.EventHandler(this.QuickCreateBack_Render);
        this.ucScrapNew.CreateEvent += new System.EventHandler(this.ListEdit_Render);


        if (this.Action != BusinessConstants.PAGE_NEW_ACTION)
        {
            this.ucNew.BackEvent += new System.EventHandler(this.NewBack_Render);
        }
        this.ucNew.CreateEvent += new System.EventHandler(this.CreateBack_Render);
        if (this.Action != BusinessConstants.PAGE_NEW_ACTION)
        {
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
            if (this.ModuleParameter.ContainsKey("NewItem"))
            {
                this.NewItem = bool.Parse(this.ModuleParameter["NewItem"]);
            }
            else
            {
                this.NewItem = false;
            }
            if (this.ModuleParameter.ContainsKey("IsScrap"))
            {
                this.IsScrap = bool.Parse(this.ModuleParameter["IsScrap"]);
            }
            else
            {
                this.IsScrap = false;
            }
            if (this.ModuleParameter.ContainsKey("IsReuse"))
            {
                this.IsReuse = bool.Parse(this.ModuleParameter["IsReuse"]);
            }
            else
            {
                this.IsReuse = false;
            }
            this.ucSearch.ModuleType = this.ModuleType;
            this.ucSearch.ModuleSubType = this.ModuleSubType;
            this.ucNew.ModuleType = this.ModuleType;
            this.ucNew.ModuleSubType = this.ModuleSubType;
            this.ucNew.IsQuick = this.IsQuick;
            this.ucEdit.ModuleType = this.ModuleType;
            this.ucEdit.ModuleSubType = this.ModuleSubType;
            this.ucScrapNew.ModuleType = this.ModuleType;
            this.ucScrapNew.ModuleSubType = this.ModuleSubType;
            this.ucViewReceipt.ModuleType = this.ModuleType;
            this.ucNew.NewItem = this.NewItem;
            this.ucEdit.NewItem = this.NewItem;
            this.ucSearch.NewItem = this.NewItem;
            this.ucScrapNew.NewItem = this.NewItem;
            this.ucEdit.IsScrap = this.IsScrap;
            this.ucEdit.IsReuse = this.IsReuse;
            this.ucScrapNew.IsReuse = this.IsReuse;
            this.ucImport.ModuleType = this.ModuleType;

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
            if (this.Action == BusinessConstants.PAGE_IMPORT_ACTION)
            {
                Import_Render(this, null);
            }
            //首页显示时隐藏search
            //if (this.Session["Temp_Session_ACT"] != null)
            //{
            //    this.ucSearch.Visible = false;
            //    Session.Contents.Remove("Temp_Session_ACT");
            //}
        }
    }

    void Import_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucScrapNew.Visible = false;
        this.ucNew.Visible = false;
        this.ucEdit.Visible = false;
        this.ucImport.Visible = false;
        this.ucImportHuId.Visible = true;
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1], (IDictionary<string, string>)((object[])sender)[2]);
        this.ucList.Visible = true;
        this.ucList.isGroup = (bool)((object[])sender)[3];
        this.ucList.isExport = (bool)((object[])sender)[4];
        this.ucList.UpdateView();
    }

    //The event handler when user click button "New" button
    void New_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        if (this.IsScrap)
        {
            this.ucScrapNew.Visible = true;
            this.ucNew.Visible = false;
        }
        else
        {
            this.ucNew.Visible = true;
        }
        this.ucEdit.Visible = false;
        this.ucNew.PageCleanup();
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
        this.ucEdit.InitPageParameter(orderNo);
    }

    //The event handler when user click button "Confirm" button of ucNew and isQuick
    void QuickCreateBack_Render(object sender, EventArgs e)
    {
        Receipt receipt = (Receipt)((object[])sender)[0];
        bool needPrintReceipt = (bool)((object[])sender)[1];
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
        this.ucNew.Visible = false;
        this.ucScrapNew.Visible = false;
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

    protected void ucSearch_BtnImportClick(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucNew.Visible = false;
        this.ucEdit.Visible = false;
        this.ucImport.Visible = true;
    }

    protected void ucImport_BtnBackClick(object sender, EventArgs e)
    {
        this.ucImport.Visible = false;
        this.ucSearch.Visible = true;
    }
}
