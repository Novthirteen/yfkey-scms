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

public partial class MasterData_User_EditMain : System.Web.UI.UserControl
{
    public event EventHandler BackEvent;

    public string UserCode
    {
        get
        {
            return (string)ViewState["UserCode"];
        }
        set
        {
            ViewState["UserCode"] = value;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.ucEdit.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucTabNavigator.lblUserClickEvent += new System.EventHandler(this.TabUserClick_Render);
        this.ucTabNavigator.lblUserRoleClickEvent += new System.EventHandler(this.TabUserRoleClick_Render);
        this.ucTabNavigator.lblUserPermissionClickEvent += new System.EventHandler(this.TabUserPermissionClick_Render);
        this.ucTabNavigator.lblUserPermissionListClickEvent += new System.EventHandler(this.TabUserPermissionListClick_Render);
        this.ucUserRole.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucUserPermission.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucUserPermissionList.BackEvent += new System.EventHandler(this.Back_Render);
    }

    public void InitPageParameter(string code)
    {
        this.UserCode = code;
        this.ucTabNavigator.Visible = true;
        this.ucEdit.Visible = true;
        this.ucEdit.InitPageParameter(code,false);
        this.ucUserRole.Visible = false;
        this.ucTabNavigator.UpdateView();
    }

    protected void Back_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void TabUserClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = true;
        this.ucUserRole.Visible = false;
        this.ucUserPermission.Visible = false;
        this.ucUserPermissionList.Visible = false;
    }

    protected void TabUserRoleClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucUserRole.Visible = true;
        this.ucUserPermission.Visible = false;
        this.ucUserPermissionList.Visible = false;
        this.ucUserRole.InitPageParameter(this.UserCode);
    }

    protected void TabUserPermissionClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucUserRole.Visible = false;
        this.ucUserPermission.Visible = true;
        this.ucUserPermissionList.Visible = false;
        this.ucUserPermission.InitPageParameter(this.UserCode);
    }

    protected void TabUserPermissionListClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucUserRole.Visible = false;
        this.ucUserPermission.Visible = false;
        this.ucUserPermissionList.Visible = true;
        this.ucUserPermissionList.InitPageParameter(this.UserCode);
    }
}
