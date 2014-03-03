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
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity;

public partial class MasterData_FlowDetail_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;
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
    protected void Page_Load(object sender, EventArgs e)
    {


        if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT)
        {
            this.tbFlow.ServiceMethod = "GetProcurementFlow";

        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
        {
            this.tbFlow.ServiceMethod = "GetDistributionFlow";
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
        {
            this.tbFlow.ServiceMethod = "GetTransferFlow";
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
        {
            this.tbFlow.ServiceMethod = "GetProductionFlow";
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS)
        {
            this.tbFlow.ServiceMethod = "GetCustomerGoodsFlow";
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
        {
            this.tbFlow.ServiceMethod = "GetSubconctractingFlow";
        }

        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
        this.tbFlow.DataBind();


    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {

        if (actionParameter.ContainsKey("FlowCode"))
        {
            this.FlowCode = actionParameter["FlowCode"];
        }
        if (actionParameter.ContainsKey("ItemCode"))
        {
            this.tbItemCode.Text = actionParameter["ItemCode"];
        }

        if (this.FlowCode != null && this.FlowCode != string.Empty)
        {
            this.lblFlow.Visible = false;
            this.tbFlow.Visible = false;
            this.rfvFlow.Enabled = false;
            this.btnBack.Visible = true;

        }
        else
        {
            this.lblFlow.Visible = true;
            this.tbFlow.Visible = true;
            this.rfvFlow.Enabled = true;
            this.btnBack.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (this.rfvFlow.IsValid)
        {
            DoSearch();
        }
    }
    protected override void DoSearch()
    {
        if (!this.rfvFlow.Visible || SearchEvent != null)
        {

            string code = this.tbItemCode.Text != string.Empty ? this.tbItemCode.Text.Trim() : string.Empty;

            #region DetachedCriteria查询

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(FlowDetail));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(FlowDetail))
                .SetProjection(Projections.Count("Id"));
            selectCriteria.AddOrder(Order.Asc("Sequence"));

            if (this.tbFlow.Text.Trim() != string.Empty && this.tbFlow.Text.Trim() != null)
            {
                this.FlowCode = this.tbFlow.Text.Trim();
            }

            if (FlowCode != null && FlowCode != string.Empty)
            {
                Flow flow = TheFlowMgr.LoadFlow(FlowCode);
                if (flow.ReferenceFlow != null && flow.ReferenceFlow.Trim() != string.Empty)
                {
                    //添加零件组引用明细
                    selectCriteria.Add(Expression.Or(Expression.Eq("Flow.Code", FlowCode), Expression.Eq("Flow.Code", flow.ReferenceFlow)));
                    selectCountCriteria.Add(Expression.Or(Expression.Eq("Flow.Code", FlowCode), Expression.Eq("Flow.Code", flow.ReferenceFlow)));
                }
                else
                {
                    selectCriteria.Add(Expression.Eq("Flow.Code", FlowCode));
                    selectCountCriteria.Add(Expression.Eq("Flow.Code", FlowCode));
                }
            }
            if (code != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Item.Code", code));
                selectCountCriteria.Add(Expression.Eq("Item.Code", code));
            }

            SearchEvent((new object[] { selectCriteria, selectCountCriteria, FlowCode }), null);
            #endregion
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {

        if (!this.rfvFlow.Visible || this.rfvFlow.IsValid)
        {
            if (NewEvent != null)
            {
                NewEvent(this.tbFlow.Text.Trim(), e);
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

}
