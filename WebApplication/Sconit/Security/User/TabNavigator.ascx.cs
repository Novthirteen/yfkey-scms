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

public partial class MasterData_User_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lblUserClickEvent;
    public event EventHandler lblUserRoleClickEvent;
    public event EventHandler lblUserPermissionClickEvent;
    public event EventHandler lblUserPermissionListClickEvent;


    public void UpdateView()
    {
        lbUser_Click(this, null);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void lbUser_Click(object sender, EventArgs e)
    {
        if (lblUserClickEvent != null)
        {
            lblUserClickEvent(this, e);
        }

        this.tab_region.Attributes["class"] = "ajax__tab_active";
        this.tab_userrole.Attributes["class"] = "ajax__tab_inactive";
        this.tab_userpermission.Attributes["class"] = "ajax__tab_inactive";
        this.tab_userpermissionlist.Attributes["class"] = "ajax__tab_inactive";
    }


    protected void lbUserRole_Click(object sender, EventArgs e)
    {
        if (lblUserRoleClickEvent != null)
        {
            lblUserRoleClickEvent(this, e);

            this.tab_region.Attributes["class"] = "ajax__tab_inactive";
            this.tab_userrole.Attributes["class"] = "ajax__tab_active";
            this.tab_userpermission.Attributes["class"] = "ajax__tab_inactive";
            this.tab_userpermissionlist.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbUserPermission_Click(object sender, EventArgs e)
    {
        if (lblUserPermissionClickEvent != null)
        {
            lblUserPermissionClickEvent(this, e);

            this.tab_region.Attributes["class"] = "ajax__tab_inactive";
            this.tab_userrole.Attributes["class"] = "ajax__tab_inactive";
            this.tab_userpermission.Attributes["class"] = "ajax__tab_active";
            this.tab_userpermissionlist.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbUserPermissionList_Click(object sender, EventArgs e)
    {
        if (lblUserPermissionListClickEvent != null)
        {
            lblUserPermissionListClickEvent(this, e);

            this.tab_region.Attributes["class"] = "ajax__tab_inactive";
            this.tab_userrole.Attributes["class"] = "ajax__tab_inactive";
            this.tab_userpermission.Attributes["class"] = "ajax__tab_inactive";
            this.tab_userpermissionlist.Attributes["class"] = "ajax__tab_active";
        }
    }
}
