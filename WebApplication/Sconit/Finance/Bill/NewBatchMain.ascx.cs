using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;

public partial class Finance_Bill_NewBatchMain : MainModuleBase
{
    public event EventHandler BackEvent;

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
        this.ucSearch.SearchEvent += new EventHandler(Search_Render);
        this.ucSearch.BackEvent += new EventHandler(Back_Render);
        this.ucList.EditEvent += new EventHandler(ListEdit_Render);
        this.ucEdit.BackEvent += new EventHandler(ucEdit_BackEvent);

        if (!IsPostBack)
        {
            this.ucSearch.ModuleType = this.ModuleType;
            this.ucEdit.ModuleType = this.ModuleType;
            this.ucList.ModuleType = this.ModuleType;
        }
    }

    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucList.Visible = true;
        this.ucList.UpdateView();
    }

    void ListEdit_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucEdit.Visible = true;
        this.ucEdit.InitPageParameter((string)sender);
    }

    void ucEdit_BackEvent(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucEdit.Visible = false;
        this.ucList.UpdateView();
    }

    void Back_Render(object sender, EventArgs e)
    {
        this.BackEvent(sender, e);
    }
}
