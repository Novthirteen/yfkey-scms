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

public partial class Security_Role_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lblRoleClickEvent;
    public event EventHandler lblRoleUserClickEvent;
    public event EventHandler lblRolePermissionClickEvent;
    public event EventHandler lblRolePermissionListClickEvent;


    public void UpdateView()
    {
        lbRole_Click(this, null);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void lbRole_Click(object sender, EventArgs e)
    {
        if (lblRoleClickEvent != null)
        {
            lblRoleClickEvent(this, e);
        }

        this.tab_region.Attributes["class"] = "ajax__tab_active";
        this.tab_userrole.Attributes["class"] = "ajax__tab_inactive";
        this.tab_userpermission.Attributes["class"] = "ajax__tab_inactive";
        this.tab_userpermissionlist.Attributes["class"] = "ajax__tab_inactive";
    }


    protected void lbRoleUser_Click(object sender, EventArgs e)
    {
        if (lblRoleUserClickEvent != null)
        {
            lblRoleUserClickEvent(this, e);

            this.tab_region.Attributes["class"] = "ajax__tab_inactive";
            this.tab_userrole.Attributes["class"] = "ajax__tab_active";
            this.tab_userpermission.Attributes["class"] = "ajax__tab_inactive";
            this.tab_userpermissionlist.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbRolePermission_Click(object sender, EventArgs e)
    {
        if (lblRolePermissionClickEvent != null)
        {
            lblRolePermissionClickEvent(this, e);

            this.tab_region.Attributes["class"] = "ajax__tab_inactive";
            this.tab_userrole.Attributes["class"] = "ajax__tab_inactive";
            this.tab_userpermission.Attributes["class"] = "ajax__tab_active";
            this.tab_userpermissionlist.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbRolePermissionList_Click(object sender, EventArgs e)
    {
        if (lblRolePermissionListClickEvent != null)
        {
            lblRolePermissionListClickEvent(this, e);

            this.tab_region.Attributes["class"] = "ajax__tab_inactive";
            this.tab_userrole.Attributes["class"] = "ajax__tab_inactive";
            this.tab_userpermission.Attributes["class"] = "ajax__tab_inactive";
            this.tab_userpermissionlist.Attributes["class"] = "ajax__tab_active";
        }
    }
}
