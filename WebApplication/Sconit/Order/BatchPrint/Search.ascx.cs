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


public partial class Order_BatchPrint_Search : ModuleBase
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


    protected void Page_Load(object sender, EventArgs e)
    {
        tbRegion.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION + ",string:" + this.CurrentUser.Code;
        tbRegion.DataBind();
    }

    protected void tbRegion_TextChanged(Object sender, EventArgs e)
    {
        if (this.tbRegion != null || this.tbRegion.Text.Trim() != string.Empty)
        {
            this.ddlShift.DataSource = TheShiftMgr.GetRegionShift(tbRegion.Text.Trim());
            this.ddlShift.DataBind();
            this.ddlShift.Items.Insert(0, new ListItem(string.Empty, string.Empty));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(OrderHead));
        selectCriteria.Add(Expression.Or(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT),
            Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)));

        if (this.tbRegion.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("PartyFrom.Code", this.tbRegion.Text.Trim()));
        }
        if (this.ddlShift.SelectedValue != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Shift.Code", this.ddlShift.SelectedValue));
        }
        if (this.tbStartDate.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("StartTime",  DateTime.Parse(this.tbStartDate.Text.Trim())));
            selectCriteria.Add(Expression.Lt("StartTime", DateTime.Parse(this.tbStartDate.Text.Trim()).AddDays(1)));
        }

        #region partyFrom
        SecurityHelper.SetPartySearchCriteria(selectCriteria, "PartyFrom.Code", this.CurrentUser.Code);
        #endregion

        #region partyTo
        SecurityHelper.SetPartySearchCriteria(selectCriteria, "PartyTo.Code", this.CurrentUser.Code);
        #endregion

        #region 设置订单Type查询条件
        selectCriteria.Add(Expression.Eq("Type", this.ModuleType));
        #endregion

        SearchEvent(selectCriteria, e);

    }
}
