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

public partial class MasterData_GeneralCode_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucTabNavigator.lbCodeMstrClickEvent += new System.EventHandler(this.TabCodeMstrClick_Render);
        this.ucTabNavigator.lbEntityOptClickEvent += new System.EventHandler(this.TabEntityOptClick_Render);
        this.ucCodeMstr.SearchEvent += new System.EventHandler(this.Search_Render);
    }

    private void TabCodeMstrClick_Render(object sender, EventArgs e)
    {
        this.ucCodeMstr.Visible = true;
        this.ucEntityOpt.Visible = false;
        this.ucCodeMstrList.Visible = false;
    }

    private void TabEntityOptClick_Render(object sender, EventArgs e)
    {
        this.ucCodeMstr.Visible = false;
        this.ucCodeMstrList.Visible = false;
        this.ucEntityOpt.Visible = true;    
    }

    private void Search_Render(object sender, EventArgs e)
    {
        this.ucCodeMstrList.Visible = true;
        this.ucCodeMstrList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucCodeMstrList.UpdateView();
        this.CleanMessage();
    }
}
