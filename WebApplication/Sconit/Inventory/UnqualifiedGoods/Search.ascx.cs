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


public partial class Inventory_UnqualifiedGoods_Search : ModuleBase
{

    public event EventHandler SearchEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.tbStartDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
        }
        this.tbLocation.ServiceParameter = "string:" + this.CurrentUser.Code;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(InspectResult));
        selectCriteria.CreateAlias("InspectOrderDetail", "d");
        selectCriteria.CreateAlias("d.InspectOrder", "o");
        selectCriteria.CreateAlias("d.LocationLotDetail", "l");
        selectCriteria.CreateAlias("l.Item", "i");
        selectCriteria.Add(Expression.Gt("RejectedQty", new Decimal(0)));
        

        if (this.tbStartDate.Text.Trim() != string.Empty)
        {
            DateTime startDate = DateTime.Parse(this.tbStartDate.Text.Trim());
            selectCriteria.Add(Expression.Ge("CreateDate", startDate));
        }

        if (this.tbEndDate.Text.Trim() != string.Empty)
        {
            DateTime endDate = DateTime.Parse(this.tbEndDate.Text.Trim());
            selectCriteria.Add(Expression.Lt("CreateDate", endDate.AddDays(1)));
        }

        if (this.tbItemCode.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("i.Code", tbItemCode.Text.Trim()));
        }
        if (this.tbCreateUser.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("CreateUser.Code", tbCreateUser.Text.Trim()));
        }
        if (this.tbInspectNo.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("PrintNo", tbInspectNo.Text.Trim()));
        }
        else
        {
            selectCriteria.Add(Expression.IsNull("PrintNo"));
        }
        if ( ddlDisposition.SelectedValue != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("d.Disposition", ddlDisposition.SelectedValue));
        }
        if (tbLocation.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("d.LocationFrom.Code", tbLocation.Text.Trim()));
        }
        SearchEvent(selectCriteria, e);

    }
}
