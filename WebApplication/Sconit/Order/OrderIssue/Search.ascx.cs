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
using NHibernate.Expression;


public partial class Distribution_OrderIssue_Search : SearchModuleBase
{

    public event EventHandler SearchEvent;

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

    private string CurrentFlowCode
    {
        get
        {
            return (string)ViewState["CurrentFlowCode"];
        }
        set
        {
            ViewState["CurrentFlowCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblFlow.Text = FlowHelper.GetFlowLabel(this.ModuleType) + ":";

        if (IsSupplier)
        {
            this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:false,bool:true,bool:false,bool:true,bool:true,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM;
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:false,bool:true,bool:false,bool:true,bool:true,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:true,bool:true,bool:false,bool:false,bool:false,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM;
        }
        //else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
        //{
        //    this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:false,bool:true,bool:false";
        //}
        //else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        //{
        //    this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:false,bool:false,bool:true";
        //}
        //else if (this.ModuleType == BusinessConstants.ORDER_MODULETYPE_VALUE_SUPPLIERDISTRIBUTION)
        //{
        //    this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:false,bool:false,bool:false";

        //    if (this.tbFlow.Text.Trim() == string.Empty)
        //    {
        //        IList<Flow> flowList = TheFlowMgr.GetFlowList(this.CurrentUser.Code, true, false, false, false, true, true, "from");

        //        if (flowList != null && flowList.Count > 0)
        //        {
        //            this.tbFlow.Text = flowList[0].Code;
        //            DoSearch();
        //        }
        //    }
        //}
    }

    protected void tbFlow_TextChanged(Object sender, EventArgs e)
    {
       
        DoSearch();
    }

    protected override void DoSearch()
    {
        if (this.tbFlow != null && this.tbFlow.Text.Trim() != string.Empty)
        {
            SearchEvent((new object[] { this.tbFlow.Text.Trim(), BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML }), null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        //todo
    }


}
