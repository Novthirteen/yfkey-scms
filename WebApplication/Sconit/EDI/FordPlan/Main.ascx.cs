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
using com.Sconit.Entity;
using com.Sconit.Web;
using NHibernate.Expression;

public partial class EDI_FordPlan_Main : MainModuleBase
{                                                                                              

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucList.SearchDetailEvent += new System.EventHandler(this.SearchDetail_Render);
        this.ucDetailList.BackEvent += new System.EventHandler(this.DetailBackEvent_Render);
    }


    void Search_Render(object sender, EventArgs e)
    {
        //this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        string searSql = (string)((object[])sender)[0];
        this.ucList.Visible = true;
        this.ucDetailList.Visible = false;
        //this.ucList.UpdateView();
        this.ucList.GetView(searSql);
    }

    void SearchDetail_Render(object sender, EventArgs e)
    {
        string searSql = (string)((object[])sender)[0];
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucDetailList.Visible = true;
        this.ucDetailList.GetView(searSql);
    }

    void DetailBackEvent_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucDetailList.Visible = false;
    }
}
