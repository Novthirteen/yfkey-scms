﻿using System;
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
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Procurement;
using com.Sconit.Utility;
using com.Sconit.Entity.Mes;

public partial class Mes_Shelf_Search : SearchModuleBase
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

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }

    protected override void DoSearch()
    {
        string code = this.tbCode.Text.Trim() != string.Empty ? this.tbCode.Text.Trim() : string.Empty;

        if (SearchEvent != null)
        {
            #region DetachedCriteria
            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Shelf));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Shelf))
                .SetProjection(Projections.Count("Code"));
            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Code"))
        {
            this.tbCode.Text = actionParameter["Code"];
        }
    }
}
