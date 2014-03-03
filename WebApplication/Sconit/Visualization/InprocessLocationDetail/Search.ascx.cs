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
using com.Sconit.Entity.View;


public partial class Visualization_InprocessLocationDetail_Search : ModuleBase
{

    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.tbStartDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

            OrderTypeDataBind();
        }
        this.tbPartyFrom.ServiceParameter = "string:" + this.ddlOrderType.SelectedValue + ",string:" + this.CurrentUser.Code;
        this.tbPartyTo.ServiceParameter = "string:" + this.ddlOrderType.SelectedValue + ",string:" + this.CurrentUser.Code;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ExportEvent != null)
        {
            DoSearch(true);
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (SearchEvent != null)
        {
            DoSearch(false);
        }

    }
    private void DoSearch(bool isExport)
    {


        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(InProcessLocationDetailView));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(InProcessLocationDetailView))
            .SetProjection(Projections.Count("Id"));
        selectCriteria.CreateAlias("InProcessLocation", "ip");
        selectCountCriteria.CreateAlias("InProcessLocation", "ip");
        selectCriteria.CreateAlias("ip.PartyFrom", "pf");
        selectCountCriteria.CreateAlias("ip.PartyFrom", "pf");
        selectCriteria.CreateAlias("ip.PartyTo", "pt");
        selectCountCriteria.CreateAlias("ip.PartyTo", "pt");


        selectCriteria.Add(Expression.In("ip.Status", new string[]{BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE,BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS}));
        selectCountCriteria.Add(Expression.In("ip.Status", new string[] { BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE, BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS }));

        selectCriteria.Add(Expression.In("ip.Status", new string[] { BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE, BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS }));
        selectCountCriteria.Add(Expression.In("ip.Status", new string[] { BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE, BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS }));


        #region date
        if (this.tbStartDate.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("ip.CreateDate", DateTime.Parse(this.tbStartDate.Text.Trim())));
            selectCountCriteria.Add(Expression.Ge("ip.CreateDate", DateTime.Parse(this.tbStartDate.Text.Trim())));
        }
        if (this.tbEndDate.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Lt("ip.CreateDate", DateTime.Parse(this.tbEndDate.Text.Trim()).AddDays(1)));
            selectCountCriteria.Add(Expression.Lt("ip.CreateDate", DateTime.Parse(this.tbEndDate.Text.Trim()).AddDays(1)));
        }
        #endregion

        string moduleType = this.ddlOrderType.SelectedValue;
        if (moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            selectCriteria.Add(Expression.In("ip.OrderType", new string[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS }));
            selectCountCriteria.Add(Expression.In("ip.OrderType", new string[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS }));
        }
        else if (moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            selectCriteria.Add(Expression.In("ip.OrderType", new string[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER }));
            selectCountCriteria.Add(Expression.In("ip.OrderType", new string[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER }));
        }
        #region partyFrom
        if (this.tbPartyFrom.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("pf.Code", this.tbPartyFrom.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("pf.Code", this.tbPartyFrom.Text.Trim()));
        }
        else if (moduleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            SecurityHelper.SetPartyFromSearchCriteria(
                selectCriteria, selectCountCriteria, this.tbPartyFrom.Text.Trim(), moduleType, this.CurrentUser.Code);
        }
        #endregion

        #region partyTo
        if (this.tbPartyTo.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("pt.Code", this.tbPartyTo.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("pt.Code", this.tbPartyTo.Text.Trim()));
        }
        else if (moduleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            SecurityHelper.SetPartyToSearchCriteria(
                selectCriteria, selectCountCriteria, this.tbPartyTo.Text.Trim(), moduleType, this.CurrentUser.Code);
        }
        #endregion

        #region AsnType
        if (this.ddlType.SelectedValue != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("ip.Type", this.ddlType.SelectedValue));
            selectCountCriteria.Add(Expression.Eq("ip.Type", this.ddlType.SelectedValue));
        }

        #endregion



        if (isExport)
        {
            ExportEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
        else
        {
            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
    }



    private void OrderTypeDataBind()
    {
        this.ddlOrderType.DataSource = this.GetOrderTypeGroup();
        this.ddlOrderType.DataBind();
    }

    private IList<CodeMaster> GetOrderTypeGroup()
    {
        IList<CodeMaster> orderTypeGroup = new List<CodeMaster>();

        orderTypeGroup.Add(GetrderType(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT));
        orderTypeGroup.Add(GetrderType(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION));

        return orderTypeGroup;
    }

    private CodeMaster GetrderType(string orderTypeValue)
    {
        return TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_ORDER_TYPE, orderTypeValue);
    }
}
