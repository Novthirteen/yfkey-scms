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

public partial class Security_Role_EditMain : System.Web.UI.UserControl
{
    public event EventHandler BackEvent;

    public string RoleCode
    {
        get
        {
            return (string)ViewState["RoleCode"];
        }
        set
        {
            ViewState["RoleCode"] = value;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {    
        this.ucEdit.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucTabNavigator.lblRoleClickEvent += new System.EventHandler(this.TabRoleClick_Render);
        this.ucTabNavigator.lblRoleUserClickEvent += new System.EventHandler(this.TabRoleUserClick_Render);
        this.ucTabNavigator.lblRolePermissionClickEvent += new System.EventHandler(this.TabRolePermissionClick_Render);
        this.ucTabNavigator.lblRolePermissionListClickEvent += new System.EventHandler(this.TabRolePermissionListClick_Render);
        this.ucRoleUser.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucRolePermission.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucRolePermissionList.BackEvent += new System.EventHandler(this.Back_Render);
    }

    public void InitPageParameter(string code)
    {
        this.RoleCode = code;
        this.ucTabNavigator.Visible = true;
        this.ucEdit.Visible = true;
        this.ucEdit.InitPageParameter(code,false);
        this.ucRoleUser.Visible = false;
        this.ucTabNavigator.UpdateView();
    }

    protected void Back_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void TabRoleClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = true;
        this.ucRoleUser.Visible = false;
        this.ucRolePermission.Visible = false;
        this.ucRolePermissionList.Visible = false;
    }

    protected void TabRoleUserClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucRoleUser.Visible = true;
        this.ucRolePermission.Visible = false;
        this.ucRolePermissionList.Visible = false;
        this.ucRoleUser.InitPageParameter(this.RoleCode);
    }

    protected void TabRolePermissionClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucRoleUser.Visible = false;
        this.ucRolePermission.Visible = true;
        this.ucRolePermissionList.Visible = false;
        this.ucRolePermission.InitPageParameter(this.RoleCode);
    }

    protected void TabRolePermissionListClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucRoleUser.Visible = false;
        this.ucRolePermission.Visible = false;
        this.ucRolePermissionList.Visible = true;
        this.ucRolePermissionList.InitPageParameter(this.RoleCode);
    }
}
