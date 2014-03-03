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
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class MasterData_Routing_Main : MainModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler UpdateViewEvent;
    public MasterData_Routing_Main()
    {
    }

    //public MasterData_Routing_Main(IDictionary<string, string> mpDic, string act, IDictionary<string, string> apDic)
    //{
    //    if (mpDic.ContainsKey("Type"))
    //    {
    //        this.ModuleType = mpDic["Type"];
    //    }
    //    this.Action = act;
    //    this.ActionParameter = apDic;
    //}
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
    public string getModuleType()
    {
        return this.ModuleType;
    }
    public string FlowCode
    {
        get
        {
            return (string)ViewState["FlowCode"];
        }
        set
        {
            ViewState["FlowCode"] = value;
        }
    }
    public void InitPageParameter(string flowCode)
    {
        this.FlowCode = flowCode;
        this.ucRouting.InitPageParameter(flowCode,false);
        this.lRouting.InnerText = FlowHelper.GetFlowRoutingLabel(this.ModuleType);
        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            this.ucReturnRouting.Visible = true;
            this.ucReturnRouting.InitPageParameter(flowCode, true);
        }
    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            fdReturn.Visible = true;
        }

        this.ucRouting.EditEvent += new System.EventHandler(this.Edit_Render);
        this.ucRouting.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucRouting.UpdateViewEvent += new System.EventHandler(this.UpdateView_Render);
        this.ucReturnRouting.EditEvent += new System.EventHandler(this.Edit_Render);
        this.ucReturnRouting.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucReturnRouting.UpdateViewEvent += new System.EventHandler(this.UpdateView_Render);

        if (!IsPostBack)
        {
            this.ucRouting.ModuleType = this.ModuleType;
            this.ucReturnRouting.ModuleType = this.ModuleType;
        }
    }

    //The event handler when user click button "Search" button
    void Edit_Render(object sender, EventArgs e)
    {
        if (!(bool)((object[])sender)[2])
        {
            this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
            this.ucRouting.Visible = true;
            this.ucList.Visible = true;
            this.ucList.UpdateView();
        }
        else
        {
            this.ucReturnRoutingList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
            this.ucReturnRouting.Visible = true;
            this.ucReturnRoutingList.Visible = true;
            this.ucReturnRoutingList.UpdateView();
        }
        
    }

    protected void Back_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void UpdateView_Render(object sender, EventArgs e)
    {
        UpdateViewEvent(sender, e);
    }
}
