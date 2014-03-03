using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;

public partial class Order_OrderIssue_TabNavigator : ModuleBase
{
    public event EventHandler lbOrderIssueClickEvent;
    public event EventHandler lbPickListIssueClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void lblOrderIssue_Click(object sender, EventArgs e)
    {
        if (lbOrderIssueClickEvent != null)
        {
            lbOrderIssueClickEvent(this, e);
        }

        this.tab_OrderIssue.Attributes["class"] = "ajax__tab_active";
        this.tab_PickListIssue.Attributes["class"] = "ajax__tab_inactive";
    }

    public void lbPickListIssue_Click(object sender, EventArgs e)
    {
        if (lbPickListIssueClickEvent != null)
        {
            lbPickListIssueClickEvent(this, e);
        }

        this.tab_OrderIssue.Attributes["class"] = "ajax__tab_inactive";
        this.tab_PickListIssue.Attributes["class"] = "ajax__tab_active";
    }

    public void UpdateView()
    {
        this.tab_OrderIssue.Attributes["class"] = "ajax__tab_inactive";
        this.tab_PickListIssue.Attributes["class"] = "ajax__tab_active";
    }
}
