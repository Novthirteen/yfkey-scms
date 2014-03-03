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


public partial class MasterData_User_Search : SearchModuleBase
{

    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        string code = this.tbCode.Text != string.Empty ? this.tbCode.Text.Trim() : string.Empty;
        string firstName = this.tbFirstName.Text != string.Empty ? this.tbFirstName.Text.Trim() : string.Empty;
        string lastName = this.tbLastName.Text != string.Empty ? this.tbLastName.Text.Trim() : string.Empty;

        #region DetachedCriteria
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(User));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(User))
            .SetProjection(Projections.Count("Code"));

        if (SearchEvent != null)
        {
            if (code != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Code", code, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Code", code, MatchMode.Start));
            }

            if (firstName != string.Empty)
            {
                selectCriteria.Add(Expression.Like("FirstName", firstName, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("FirstName", firstName, MatchMode.Start));
            }
            if (lastName != string.Empty)
            {
                selectCriteria.Add(Expression.Like("LastName", lastName, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("LastName", lastName, MatchMode.Start));
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
