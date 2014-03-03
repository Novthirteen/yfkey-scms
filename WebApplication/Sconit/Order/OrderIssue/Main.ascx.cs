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
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Distribution;
using com.Sconit.Service.Distribution;

public partial class Distribution_OrderIssue_Main : MainModuleBase
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
    public bool IsSupplier
    {
        get { return ViewState["IsSupplier"] != null ? (bool)ViewState["IsSupplier"] : false; }
        set { ViewState["IsSupplier"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucTabNavigator.lbOrderIssueClickEvent += new System.EventHandler(this.TabOrderIssueClick_Render);
        this.ucTabNavigator.lbPickListIssueClickEvent += new System.EventHandler(this.TabPickListIssueClick_Render);
        this.ucOrderIssue.CreatePickListEvent += new System.EventHandler(this.CreatePickList_Render);
        this.ucOrderIssue.BackEvent += new System.EventHandler(this.Back_Render);

        if (!IsPostBack)
        {
            if (this.ModuleParameter.ContainsKey("ModuleType"))
            {
                this.ModuleType = this.ModuleParameter["ModuleType"];
                this.ucOrderIssue.ModuleType = this.ModuleType;
            }
            if (this.ModuleParameter.ContainsKey("ModuleSubType"))
            {
                this.ModuleSubType = this.ModuleParameter["ModuleSubType"];
                this.ucOrderIssue.ModuleSubType = this.ModuleSubType;
            }
            if (this.ModuleParameter != null && this.ModuleParameter.ContainsKey("IsSupplier"))
            {
                this.IsSupplier = bool.Parse(this.ModuleParameter["IsSupplier"]);
                this.ucOrderIssue.IsSupplier = this.IsSupplier;
            }
            else
            {
                this.ucOrderIssue.IsSupplier = false;
            }

            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                this.ucTabNavigator.Visible = true;
            }

            if (this.IsSupplier)
            {
                this.ucTabNavigator.Visible = false;
                this.ucPickListIssue.Visible = false;
            }
        }
    }

    //The event handler when user click link button to "HuSearch" tab
    void TabOrderIssueClick_Render(object sender, EventArgs e)
    {
        this.ucOrderIssue.Visible = true;
        this.ucPickListIssue.Visible = false;
       
    }

    //The event handler when user click link button to "Traceability" tab
    void TabPickListIssueClick_Render(object sender, EventArgs e)
    {
        this.ucOrderIssue.Visible = false;
        this.ucPickListIssue.Visible = true;
    }

    void CreatePickList_Render(object sender, EventArgs e)
    {
        this.ucTabNavigator.UpdateView();
    }

    void Back_Render(object sender, EventArgs e)
    {
        this.ucTabNavigator.lblOrderIssue_Click(sender,e);
    }

}
