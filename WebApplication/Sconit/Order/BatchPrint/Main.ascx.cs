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

public partial class MasterData_Order_BatchPrint_Main : MainModuleBase
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

    public string ModuleSubType
    {
        get
        {
            return (string)ViewState["ModuleSubType"];
        }
        set
        {
            ViewState["ModuleSubType"] = value;
        }
    }

  

 
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            this.ucSearch.ModuleType = this.ModuleParameter["ModuleType"];
            this.ucSearch.ModuleSubType = this.ModuleParameter["ModuleSubType"];
            this.ucList.ModuleType = this.ModuleParameter["ModuleType"];
            this.ucList.ModuleSubType = this.ModuleParameter["ModuleSubType"];
           
        }
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {
        this.ucList.Visible = true;
        this.ucList.InitPageParameter((DetachedCriteria)sender);
    }

   

}
