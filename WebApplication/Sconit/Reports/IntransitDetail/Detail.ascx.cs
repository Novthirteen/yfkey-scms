using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.View;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;
using com.Sconit.Entity;

public partial class Reports_IntransitDetail_Detail : ListModuleBase
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

    public void InitPageParameter(string flowCode, string itemCode, string uom, decimal unitCount, int position)
    {
        #region 根据Position来查找Operation
        int operation = 0;
        if (position > 0)
        {
            try
            {
                Flow flow = this.TheFlowMgr.CheckAndLoadFlow(flowCode);
                if (flow.Routing != null)
                {
                    IList<RoutingDetail>  routingDetailList = this.TheRoutingDetailMgr.GetRoutingDetail(flow.Routing,DateTime.Now);

                    if (routingDetailList != null && routingDetailList.Count > 0)
                    {
                        IListHelper.Sort<RoutingDetail>(routingDetailList, "Operation");

                        operation = routingDetailList[position - 1].Operation;
                    }
                }
            }
            catch (BusinessErrorException ex)
            {
                this.ShowErrorMessage(ex);
                return;
            }
        }
        #endregion

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(InProcessLocationDetailView));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(InProcessLocationDetailView))
            .SetProjection(Projections.Count("Id"));
        IDictionary<string, string> alias = new Dictionary<string, string>();
        selectCriteria.CreateAlias("OrderDetail", "od");
        selectCountCriteria.CreateAlias("OrderDetail", "od");
        selectCriteria.CreateAlias("od.Item", "i");
        selectCountCriteria.CreateAlias("od.Item", "i");
        selectCriteria.CreateAlias("od.Uom", "u");
        selectCountCriteria.CreateAlias("od.Uom", "u");
        selectCriteria.CreateAlias("od.OrderHead", "oh");
        selectCountCriteria.CreateAlias("od.OrderHead", "oh");
        //selectCriteria.CreateAlias("oh.Flow", "f");
        //selectCountCriteria.CreateAlias("oh.Flow", "f");
        selectCriteria.CreateAlias("InProcessLocation", "ip");
        selectCountCriteria.CreateAlias("InProcessLocation", "ip");

        alias.Add("OrderDetail", "od");
        alias.Add("OrderDetail.Item", "i");
        alias.Add("OrderDetail.Uom", "u");
        alias.Add("OrderDetail.OrderHead", "oh");
        alias.Add("OrderDetail.Flow", "f");
        alias.Add("InProcessLocation", "ip");

        selectCriteria.Add(Expression.In("ip.Status", new object[] { BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE, BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS }));
        selectCountCriteria.Add(Expression.In("ip.Status", new object[] { BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE, BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS }));

        selectCriteria.Add(Expression.Eq("ip.Type", BusinessConstants.CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_NORMAL ));
        selectCountCriteria.Add(Expression.Eq("ip.Type", BusinessConstants.CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_NORMAL));

        selectCriteria.Add(Expression.Eq("oh.Flow", flowCode));
        selectCountCriteria.Add(Expression.Eq("oh.Flow", flowCode));

        selectCriteria.Add(Expression.Eq("i.Code", itemCode));
        selectCountCriteria.Add(Expression.Eq("i.Code", itemCode));

        selectCriteria.Add(Expression.Eq("u.Code", uom));
        selectCountCriteria.Add(Expression.Eq("u.Code", uom));

        selectCriteria.Add(Expression.Eq("od.UnitCount", unitCount));
        selectCountCriteria.Add(Expression.Eq("od.UnitCount", unitCount));

        if (operation != 0)
        {
            selectCriteria.Add(Expression.Eq("ip.CurrentOperation", operation));
            selectCountCriteria.Add(Expression.Eq("ip.CurrentOperation", operation));
        }
        else
        {
            selectCriteria.Add(Expression.Or(Expression.Eq("ip.CurrentOperation", 0), Expression.IsNull("ip.CurrentOperation")));
            selectCountCriteria.Add(Expression.Or(Expression.Eq("ip.CurrentOperation", 0), Expression.IsNull("ip.CurrentOperation")));
        }

        this.SetSearchCriteria(selectCriteria, selectCountCriteria, alias);
        this.UpdateView();
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        this.Visible = false;
        this.GV_List.DataSource = null;
        this.GV_List.DataBind();
    }
}
