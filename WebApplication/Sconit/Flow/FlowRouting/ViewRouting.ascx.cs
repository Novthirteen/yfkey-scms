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

public partial class MasterData_Flow_ViewRouting : ModuleBase
{
    public event EventHandler EditEvent;
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

    public void InitPageParameter(string flowCode,bool isRetrun)
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
            Literal lblRouting = (Literal)this.FV_Routing.FindControl("lblRouting");
            Literal lRouting = (Literal)(this.FV_Routing.FindControl("lRouting"));
            Literal lDescription = (Literal)(this.FV_Routing.FindControl("lDescription"));
            lblRouting.Text = FlowHelper.GetFlowRoutingLabel(this.ModuleType);
            if (!IsReturn)
            {
                if (flow.Routing != null)
                {
                    lRouting.Text = flow.Routing.Code;
                    lDescription.Text = flow.Routing.Description;
                    EditEvent(DoSearch(flow.Routing.Code,IsReturn), null);
                }
            }
            else
            {
                if (flow.ReturnRouting != null)
                {
                    lblRouting.Text = "${MasterData.Flow.ReturnRouting}";
                    lRouting.Text = flow.ReturnRouting.Code;
                    lDescription.Text = flow.ReturnRouting.Description;
                    EditEvent(DoSearch(flow.ReturnRouting.Code,IsReturn), null);
                }
            }
        }
    }


    private object[] DoSearch(string routingCode,bool isReturn)
    {


        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(RoutingDetail));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(RoutingDetail))
            .SetProjection(Projections.Count("Id"));

        
            selectCriteria.Add(Expression.Eq("Routing.Code", routingCode));
            selectCountCriteria.Add(Expression.Eq("Routing.Code", routingCode));
       

        return new object[] { selectCriteria, selectCountCriteria,IsReturn };


    }
}
