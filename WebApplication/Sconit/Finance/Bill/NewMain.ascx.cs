using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;

public partial class Finance_Bill_NewMain : MainModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;

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

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucNewTabNavigator.lbSingleClickEvent += new EventHandler(ucNewTabNavigator_lbSingleClickEvent);
        this.ucNewTabNavigator.lbBatchClickEvent += new EventHandler(ucNewTabNavigator_lbBatchClickEvent);
        this.ucNewTabNavigator.lbRecalculateClickEvent += new EventHandler(ucNewTabNavigator_lbRecalculateClickEvent);
        this.ucNewSearch.BackEvent += new EventHandler(NewBack_Render);
        this.ucNewSearch.CreateEvent += new EventHandler(Create_Render);
        this.ucNewBatchSearch.BackEvent += new EventHandler(NewBack_Render);
        this.ucNewRecalculateSearch.BackEvent += new EventHandler(NewBack_Render);
        if (!IsPostBack)
        {
            this.ucNewSearch.ModuleType = this.ModuleType;
            this.ucNewBatchSearch.ModuleType = this.ModuleType;
            this.ucNewRecalculateSearch.ModuleType = this.ModuleType;
        }
    }

    void ucNewTabNavigator_lbSingleClickEvent(object sender, EventArgs e)
    {
        this.ucNewSearch.Visible = true;
        this.ucNewBatchSearch.Visible = false;
        this.ucNewRecalculateSearch.Visible = false;
    }

    void ucNewTabNavigator_lbBatchClickEvent(object sender, EventArgs e)
    {
        this.ucNewBatchSearch.Visible = true;
        this.ucNewSearch.Visible = false;
        this.ucNewRecalculateSearch.Visible = false;
    }

    void ucNewTabNavigator_lbRecalculateClickEvent(object sender, EventArgs e)
    {
        this.ucNewRecalculateSearch.Visible = true;
        this.ucNewSearch.Visible = false;
        this.ucNewBatchSearch.Visible = false;
    }

    void NewBack_Render(object sender, EventArgs e)
    {
        this.BackEvent(sender, e);
    }

    void Create_Render(object sender, EventArgs e)
    {
        this.CreateEvent(sender, e);
    }
}
