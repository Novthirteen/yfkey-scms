using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;
using NHibernate.Expression;

public partial class Reports_IntransitDetail_Main : MainModuleBase
{
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
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucSearch.ExportEvent += new EventHandler(ucSearch_ExportEvent);
        this.ucList.DetailEvent += new EventHandler(ucSearch_DetailEvent);

        if (!IsPostBack)
        {
            this.ModuleType = this.ModuleParameter["ModuleType"];
            this.ucSearch.ModuleType = this.ModuleType;
            this.ucList.ModuleType = this.ModuleType;
            this.ucDetail.ModuleType = this.ModuleType;

            if (this.Action == BusinessConstants.PAGE_LIST_ACTION)
            {
                ucSearch.QuickSearch(this.ActionParameter);
            }
        }
    }

    void ucSearch_ExportEvent(object sender, EventArgs e)
    {
        //this.ucList.InitPageParameter(this.ModuleType, (string)((object[])sender)[0], (string)((object[])sender)[1], (string)((object[])sender)[2]);
        //this.ucList.Export();
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.InitPageParameter(sender.ToString());
        this.ucList.Visible = true;
    }

    void ucSearch_DetailEvent(object sender, EventArgs e)
    {
        this.ucDetail.Visible = true;

        object[] array = (object[])sender;
        string flowCode = array[0].ToString();
        string itemCode = array[1].ToString();
        string uom = array[2].ToString();
        decimal unitCount = decimal.Parse(array[3].ToString());
        int position = int.Parse(array[4].ToString());

        this.ucDetail.InitPageParameter(flowCode, itemCode, uom, unitCount, position);
    }
}
