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
using System.Collections.Generic;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;

public partial class Order_GoodsReceipt_OrderReceipt_Search : SearchModuleBase
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

    protected void Page_Load(object sender, EventArgs e)
    {

        this.lblOrderNo.Text = OrderHelper.GetOrderLabel(this.ModuleType) + ":";
        this.lblPartyFrom.Text = OrderHelper.GetOrderPartyFromLabel(this.ModuleType) + ":";
        this.lblPartyTo.Text = OrderHelper.GetOrderPartyToLabel(this.ModuleType) + ":";
        this.lblFlow.Text = FlowHelper.GetFlowLabel(this.ModuleType) + ":";

        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:false,bool:true,bool:false,bool:true,bool:true,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:true,bool:true,bool:false,bool:false,bool:false,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM;
        }        
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            this.lblPartyTo.Visible = false;
            this.tbPartyTo.Visible = false;
            this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:false,bool:false,bool:true,bool:false,bool:false,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH;
        }
        //else if (this.ModuleType == BusinessConstants.ORDER_MODULETYPE_VALUE_PROCUREMENTCONFIRM)
        //{
        //    this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:true,bool:false,bool:false;string:" + this.ModuleType;
        //    this.lblLocTo.Visible = false;
        //    this.tbLocTo.Visible = false;

        //}
        this.tbPartyFrom.ServiceParameter = "string:" + this.ModuleType + ",string:" + this.CurrentUser.Code;
        this.tbPartyTo.ServiceParameter = "string:" + this.ModuleType + ",string:" + this.CurrentUser.Code;
        this.tbLocTo.ServiceParameter = this.lblPartyTo.Visible ? "string:#tbPartyTo" : "string:#tbPartyFrom";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(OrderHead));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(OrderHead))
            .SetProjection(Projections.Count("OrderNo"));
        selectCriteria.CreateAlias("PartyFrom", "pf");
        selectCriteria.CreateAlias("PartyTo", "pt");
        selectCountCriteria.CreateAlias("PartyFrom", "pf");
        selectCountCriteria.CreateAlias("PartyTo", "pt");

        #region 设置订单Type查询条件
        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            selectCriteria.Add(Expression.In("Type",new string[]{BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, 
                BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER,BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING,
                BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS}));
            selectCountCriteria.Add(Expression.In("Type", new string[]{BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, 
                BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER,BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING,
                BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS}));
            //selectCriteria.Add(Expression.Or(Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT), Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)));
            //selectCountCriteria.Add(Expression.Or(Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT), Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)));
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            selectCriteria.Add(Expression.Or(Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION), Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)));
            selectCountCriteria.Add(Expression.Or(Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION), Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)));
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            selectCriteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));
            selectCountCriteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));
        }
        else
        {
            throw new TechnicalException("invalided module type:" + this.ModuleType);
        }

        #endregion

        if (tbOrderNo != null && tbOrderNo.Text != string.Empty)
        {
            selectCriteria.Add(Expression.Like("OrderNo", tbOrderNo.Text.Trim(), MatchMode.Start));
            selectCountCriteria.Add(Expression.Like("OrderNo", tbOrderNo.Text.Trim(), MatchMode.Start));
        }

        if (tbFlow != null && tbFlow.Text != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Flow", tbFlow.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("Flow", tbFlow.Text.Trim()));
        }

        
        #region partyFrom
        if (this.tbPartyFrom != null && this.tbPartyFrom.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("pf.Code", this.tbPartyFrom.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("pf.Code", this.tbPartyFrom.Text.Trim()));
        }
        else if (this.ModuleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            SecurityHelper.SetPartyFromSearchCriteria(
                selectCriteria, selectCountCriteria, (this.tbPartyFrom != null ? this.tbPartyFrom.Text : null), this.ModuleType, this.CurrentUser.Code);
        }
        #endregion
        
        #region partyTo
        if (this.tbPartyTo != null && this.tbPartyTo.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("pt.Code", this.tbPartyTo.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("pt.Code", this.tbPartyTo.Text.Trim()));
        }
        else if (this.ModuleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            SecurityHelper.SetPartyToSearchCriteria(
                selectCriteria, selectCountCriteria, (this.tbPartyTo != null ? this.tbPartyTo.Text : null), this.ModuleType, this.CurrentUser.Code);
        }
        #endregion

        if (tbLocFrom != null && tbLocFrom.Text != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("LocationFrom.Code", tbLocFrom.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("LocationFrom.Code", tbLocFrom.Text.Trim()));
        }

        if (tbLocTo != null && tbLocTo.Text != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("LocationTo.Code", tbLocTo.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("LocationTo.Code", tbLocTo.Text.Trim()));
        }

        selectCriteria.Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
        selectCountCriteria.Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));

        //过滤全部发完货的订单
        DetachedCriteria ordSubCriteria = DetachedCriteria.For<OrderDetail>();
        ordSubCriteria.Add(Expression.Or(Expression.GtProperty("OrderedQty", "ShippedQty"),Expression.IsNull("ShippedQty")));
        ordSubCriteria.SetProjection(Projections.ProjectionList().Add(Projections.GroupProperty("OrderHead.OrderNo")));

        selectCriteria.Add(Subqueries.PropertyIn("OrderNo", ordSubCriteria));
        selectCountCriteria.Add(Subqueries.PropertyIn("OrderNo", ordSubCriteria));

        SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        //todo
    }
}
