﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;
using NHibernate.Expression;

public partial class Reports_PassRate_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);

        if (!IsPostBack)
        {
            if (this.Action == BusinessConstants.PAGE_LIST_ACTION)
            {
                ucSearch.QuickSearch(this.ActionParameter);
            }
        }
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucList.Visible = true;
        this.ucList.UpdateView();
    }
}
