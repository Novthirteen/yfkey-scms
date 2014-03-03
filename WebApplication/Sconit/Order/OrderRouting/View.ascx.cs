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
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using com.Sconit.Utility;

public partial class Order_OrderRouting_View : ModuleBase
{
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

    public void InitPageParameter(string orderNo)
    {
        OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(orderNo, false, true);
        if (orderHead.Routing != null)
        {
            this.lblRoutingCodeValue.Text = orderHead.Routing.Code;
            this.lblDescriptionValue.Text = orderHead.Routing.Description;

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(OrderOperation));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(OrderOperation))
                .SetProjection(Projections.Count("Id"));
            selectCriteria.CreateAlias("OrderHead", "oh");
            selectCountCriteria.CreateAlias("OrderHead", "oh");

            selectCriteria.Add(Expression.Eq("oh.OrderNo", orderNo));
            selectCountCriteria.Add(Expression.Eq("oh.OrderNo", orderNo));


            this.ucList.SetSearchCriteria(selectCriteria, selectCountCriteria);
            this.ucList.Visible = true;
            this.Visible = true;
            this.ucList.UpdateView();
        }
        else
        {
            this.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ucList.ModuleType = this.ModuleType;
            this.ucList.ModuleSubType = this.ModuleSubType;
        }
        this.lTitle.InnerText = FlowHelper.GetFlowRoutingLabel(this.ModuleType);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (this.BackEvent != null)
        {
            this.BackEvent(this, e);
        }
    }
}
