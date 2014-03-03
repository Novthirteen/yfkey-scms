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
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;
using System.Reflection;
using com.Sconit.Entity.Exception;

public partial class Security_NamedQuery : ListModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(NamedQuery));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(NamedQuery))
                .SetProjection(Projections.Count("QueryName"));

            selectCriteria.Add(Expression.Eq("User.Code", this.CurrentUser.Code));
            selectCountCriteria.Add(Expression.Eq("User.Code", this.CurrentUser.Code));

            this.SetSearchCriteria(selectCriteria, selectCountCriteria);
            this.UpdateView();
        }
    }

    protected void lbtnOpen_Click(object sender, EventArgs e)
    {
        string queryName = ((LinkButton)sender).CommandArgument;
        NamedQuery namedQuery = TheNamedQueryMgr.LoadNamedQuery(this.CurrentUser, queryName);

        if (namedQuery == null)
        {
            this.ShowErrorMessage("MasterData.NamedQuery.QueryNameNotExist", queryName);
        }
        else
        {
            string url = "~/Main.aspx?mid=" + namedQuery.UserControlPath
                + "__mp--" + namedQuery.ModuleParameter;
                //+ "__act--" + BusinessConstants.PAGE_LIST_ACTION;
            this.Session["ACT"] = BusinessConstants.PAGE_LIST_ACTION;

            if (namedQuery.ActionParameter != null && namedQuery.ActionParameter.Trim() != string.Empty)
            {
                this.Session["AP"] = namedQuery.ActionParameter;
                //url += "__ap--" + namedQuery.ActionParameter;
            }

            this.Page.Response.Redirect(url);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string queryName = ((LinkButton)sender).CommandArgument;
        try
        {
            TheNamedQueryMgr.DeleteNamedQuery(this.CurrentUser, queryName);
            this.ShowErrorMessage("MasterData.NamedQuery.DeleteSuccessful", queryName);
            this.UpdateView();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }
}
