using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;

public partial class Inventory_Stocktaking_EditMain : MainModuleBase
{

    public event EventHandler TabEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new EventHandler(Search_Render);
        this.ucSearch.NewEvent += new EventHandler(New_Render);
        this.ucNew.SaveEvent += new EventHandler(NewSave_Render);
        this.ucNew.BackEvent += new EventHandler(NewBack_Render);
        this.ucBusiness.BackEvent += new EventHandler(EditBack_Render);
        this.ucBusiness.TabEvent += new EventHandler(Tab_Render);

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
        this.ucList.Visible = true;
        this.ucList.UpdateView();
    }

    void New_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucNew.Visible = true;
        this.ucNew.UpdateView();
    }

    void NewSave_Render(object sender, EventArgs e)
    {
        this.ucNew.Visible = false;
        this.ucBusiness.Visible = true;
        this.ucBusiness.InitPageParameter((string)sender);
    }

    void NewBack_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucNew.Visible = false;
        this.ucList.Visible = true;
        this.ucList.UpdateView();
    }

    public void ListEdit_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucBusiness.Visible = true;
        string code = (string)sender;
        CycleCount cycleCount = TheCycleCountMgr.LoadCycleCount(code);
        if (cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL
            || cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
        {
            TabEvent(new object[] { false, "" }, e);
        }
        else
        {
            TabEvent(new object[] { true, code }, e);
        }
        this.ucBusiness.InitPageParameter((string)sender);
    }

    void EditBack_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucBusiness.Visible = false;
        this.ucList.Visible = true;

        TabEvent(new object[] { false, "" }, e);

        this.ucList.UpdateView();
    }

    void Tab_Render(object sender, EventArgs e)
    {
        TabEvent(sender, e);
    }
}
