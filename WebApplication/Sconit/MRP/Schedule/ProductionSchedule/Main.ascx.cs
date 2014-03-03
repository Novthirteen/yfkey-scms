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

public partial class MRP_Schedule_ProductionSchedule_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.lbNewClickEvent += new EventHandler(this.NewClick_Render);
        this.ucNew.backClickEvent += new EventHandler(this.NewBackClick_Render);
    }

    //The event handler when user click tab "CustomerSchedule" 
    private void TabCustomerScheduleClick_Render(object sender, EventArgs e)
    {
        //this.ucPlan.Visible = true;
        //this.ucOrder.Visible = false;
    }

    //The event handler when user click tab "Order" 
    private void TabOrderClick_Render(object sender, EventArgs e)
    {
        //this.ucPlan.Visible = false;
        //this.ucOrder.Visible = true;
    }

    private void NewClick_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = false;
        this.ucNew.Visible = true;
        this.ucNew.InitPageParameter(Convert.ToInt32((string)sender));
    }

    private void NewBackClick_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucNew.Visible = false;
        this.ucSearch.DoSearch();
    }

}
