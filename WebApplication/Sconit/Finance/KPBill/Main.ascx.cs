using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;
using NHibernate.Expression;

public partial class Finance_Bill_Main : MainModuleBase
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new EventHandler(Search_Render);

        this.ucEdit.BackEvent += new EventHandler(Back_Render);
        this.ucList.ViewEvent += new EventHandler(ListView_Render);

        if (!IsPostBack)
        {
            if (this.Action == BusinessConstants.PAGE_LIST_ACTION)
            {
                ucSearch.QuickSearch(this.ActionParameter);
            }
          
          
        }
    }

    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucList.IsExport = (bool)(((object[])sender)[2]);
        this.ucList.Visible = true;
        this.ucList.UpdateView();
    }

   

   

    void ListView_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucEdit.Visible = true;
        this.ucEdit.InitPageParameter((decimal)sender);
    }

    void Back_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucEdit.Visible = false;
        this.ucList.UpdateView();
    }
}
