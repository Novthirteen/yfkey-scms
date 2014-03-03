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
using com.Sconit.Service.Criteria;
using com.Sconit.Web;
using com.Sconit.Entity;
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Entity.Mes;


public partial class Mes_ProductLineUser_Search : SearchModuleBase
{

    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true";
        this.tbFlow.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        string code = this.tbCode.Text.Trim();
        string firstName = this.tbFirstName.Text.Trim();
        string lastName = this.tbLastName.Text.Trim();
        string flow = this.tbFlow.Text.Trim();

        #region DetachedCriteria
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(ProductLineUser));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(ProductLineUser))
            .SetProjection(Projections.Count("Id"));

        selectCriteria.CreateAlias("DeliveryUser", "u");
        selectCountCriteria.CreateAlias("DeliveryUser", "u");

        if (SearchEvent != null)
        {
            if (code != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("u.Code", code));
                selectCountCriteria.Add(Expression.Eq("u.Code", code));
            }

            if (flow != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("ProductLine.Code", flow));
                selectCountCriteria.Add(Expression.Eq("ProductLine.Code", flow));
            }

            if (firstName != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("u.FirstName", firstName));
                selectCountCriteria.Add(Expression.Eq("u.FirstName", firstName));
            }
            if (lastName != string.Empty)
            {
                selectCriteria.Add(Expression.Like("u.LastName", lastName));
                selectCountCriteria.Add(Expression.Like("u.LastName", lastName));
            }

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
        #endregion
    }


    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {

        if (actionParameter.ContainsKey("Code"))
        {
            this.tbCode.Text = actionParameter["Code"];
        }
        if (actionParameter.ContainsKey("FirstName"))
        {
            this.tbFirstName.Text = actionParameter["FirstName"];
        }
        if (actionParameter.ContainsKey("LastName"))
        {
            this.tbLastName.Text = actionParameter["LastName"];
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }
}
