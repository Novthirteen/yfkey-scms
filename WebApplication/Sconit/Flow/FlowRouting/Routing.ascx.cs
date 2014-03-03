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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Utility;
using NHibernate.Expression;

public partial class MasterData_Flow_Routing : EditModuleBase
{
    public event EventHandler EditEvent;
    public event EventHandler BackEvent;
    public event EventHandler UpdateViewEvent;
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
    protected string FlowCode
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

    public bool IsReturn
    {
        get
        {
            return (bool)ViewState["IsReturn"];
        }
        set
        {
            ViewState["IsReturn"] = value;
        }
    }

    private string[] EditFields = new string[]
    {
        
    };


    public void InitPageParameter(string flowCode, bool isRetrun)
    {
        this.FlowCode = flowCode;
        this.IsReturn = isRetrun;
        this.ODS_Routing.SelectParameters["code"].DefaultValue = FlowCode;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected void FV_Routing_DataBound(object sender, EventArgs e)
    {
        if (FlowCode != null && FlowCode != string.Empty)
        {
            Flow flow = TheFlowMgr.LoadFlow(FlowCode);

            Controls_TextBox tbRouting = (Controls_TextBox)(this.FV_Routing.FindControl("tbRouting"));
            Literal lblRouting = (Literal)this.FV_Routing.FindControl("lblRouting");
            Literal lDescription = (Literal)(this.FV_Routing.FindControl("lDescription"));
            lblRouting.Text = FlowHelper.GetFlowRoutingLabel(this.ModuleType) + ":";
            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                if (this.IsReturn)
                {
                    tbRouting.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_ROUTING_TYPE_VALUE_REWORK;
                }
                else
                {
                    tbRouting.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_ROUTING_TYPE_VALUE_PRODUCTION;
                }
                tbRouting.DataBind();
            }
            else
            {
                tbRouting.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_ROUTING_TYPE_VALUE_BINARY;
                tbRouting.DataBind();
            }
            if (!IsReturn)
            {
                if (flow.Routing != null)
                {
                    tbRouting.Text = flow.Routing.Code;
                    lDescription.Text = flow.Routing.Description;
                    EditEvent(DoSearch(flow.Routing.Code, IsReturn), null);
                }
            }
            else
            {
                if (flow.ReturnRouting != null)
                {
                    lblRouting.Text = "${MasterData.Flow.ReturnRouting}";
                    tbRouting.Text = flow.ReturnRouting.Code;
                    lDescription.Text = flow.ReturnRouting.Description;
                    EditEvent(DoSearch(flow.ReturnRouting.Code, IsReturn), null);
                }
            }
        }
    }

    protected void ODS_Routing_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Flow flow = (Flow)e.InputParameters[0];
        Flow oldFlow = TheFlowMgr.LoadFlow(FlowCode);
        CloneHelper.CopyProperty(oldFlow, flow, EditFields, true);

        Controls_TextBox tbRouting = (Controls_TextBox)(this.FV_Routing.FindControl("tbRouting"));

        if (!IsReturn)
        {
            flow.Routing = TheRoutingMgr.LoadRouting(tbRouting.Text);
        }
        else
        {
            flow.ReturnRouting = TheRoutingMgr.LoadRouting(tbRouting.Text);
        }
        flow.LastModifyUser = this.CurrentUser;
        flow.LastModifyDate = DateTime.Now;
        flow.Version += 1;
    }

    protected void ODS_Routing_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {

        if (!IsReturn)
        {
            this.Parent.FindControl("ucList").Visible = false;
        }
        else
        {
            this.Parent.FindControl("ucReturnRoutingList").Visible = false;
        }
        UpdateViewEvent(this.FlowCode, e);
        ShowSuccessMessage("MasterData.Flow.Routing.AddRouting.Successfully", this.FlowCode);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            FlowCode = null;
            BackEvent(this, e);
        }
    }

    private object[] DoSearch(string routingCode, bool isReturn)
    {


        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(RoutingDetail));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(RoutingDetail))
            .SetProjection(Projections.Count("Id"));


        selectCriteria.Add(Expression.Eq("Routing.Code", routingCode));
        selectCountCriteria.Add(Expression.Eq("Routing.Code", routingCode));


        return new object[] { selectCriteria, selectCountCriteria, IsReturn };


    }
}
