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
      
        if (this.Action != BusinessConstants.PAGE_NEW_ACTION)
        {
            this.ucNew.BackEvent += new System.EventHandler(this.NewBack_Render);
        }
        this.ucNew.CreateEvent += new System.EventHandler(this.CreateBack_Render);
        if (this.Action != BusinessConstants.PAGE_NEW_ACTION)
        {
            this.ucViewReceipt.BackEvent += new System.EventHandler(this.EditBack_Render);
        }
        else
        {
            this.ucViewReceipt.BackEvent += new System.EventHandler(this.New_Render);
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
          
            this.ucNew.ModuleType = this.ModuleType;
            this.ucNew.ModuleSubType = this.ModuleSubType;
            this.ucNew.IsQuick = this.IsQuick;
            this.ucNew.IsReject = this.IsReject;
            this.ucViewReceipt.ModuleType = this.ModuleType;
            
            if (this.Action == BusinessConstants.PAGE_LIST_ACTION)
            {
               
            }
            if (this.Action == BusinessConstants.PAGE_NEW_ACTION)
            {
                New_Render(this, null);
            }
            if (this.Action == BusinessConstants.PAGE_EDIT_ACTION)
            {
                
            }
        }
    }

   
    //The event handler when user click button "New" button
    void New_Render(object sender, EventArgs e)
    {
        this.ucNew.Visible = true;
        this.ucViewReceipt.Visible = false;
        this.ucNew.PageCleanup();
    }

    //The event handler when user click button "Back" button of ucNew
    void NewBack_Render(object sender, EventArgs e)
    {
        this.ucNew.Visible = false;
    }

    //The event handler when user click button "Confirm" button of ucNew
    void CreateBack_Render(object sender, EventArgs e)
    {
        Receipt receipt = (Receipt)((object[])sender)[0];
        bool needPrintReceipt = (bool)((object[])sender)[1];
        this.ucNew.Visible = false;
        this.ucViewReceipt.Visible = true;
        this.ucViewReceipt.InitPageParameter(receipt, needPrintReceipt);
    }


    //The event handler when user click button "Back" button of ucViewReceipt
    void EditBack_Render(object sender, EventArgs e)
    {
        this.ucViewReceipt.Visible = false;
    }
}
