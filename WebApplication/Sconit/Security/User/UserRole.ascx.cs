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
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Web;
using System.Collections.Generic;

public partial class Security_User_UserRole : ModuleBase
{
    public event EventHandler BackEvent;
 
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(string code)
    {
       
        this.ODS_RolesInUser.SelectParameters["code"].DefaultValue = code;
        this.ODS_RolesNotInUser.SelectParameters["code"].DefaultValue = code;
        this.lbCode.Text = code;
    }

    protected void ToInBT_Click(object sender, EventArgs e)
    {
        List<Role> rList = new List<Role>();
        foreach (ListItem item in this.CBL_NotInRole.Items)
        {
            if (item.Selected)
            {
                rList.Add(TheRoleMgr.LoadRole(item.Value));
            }
        }
        if (rList.Count > 0) TheUserRoleMgr.CreateUserRoles(TheUserMgr.LoadUser(this.lbCode.Text), rList);
        this.CBL_NotInRole.DataBind();
        this.CBL_InRole.DataBind();
        this.cb_InRole.Checked = false;
        this.cb_NotInRole.Checked = false;
        UpdateUserLastModifyDate();
    }

    protected void ToOutBT_Click(object sender, EventArgs e)
    {
        List<UserRole> urList = new List<UserRole>();
        foreach (ListItem item in this.CBL_InRole.Items)
        {
            if (item.Selected)
            {
                UserRole userRole = TheUserRoleMgr.LoadUserRole(this.lbCode.Text, item.Value);
                urList.Add(userRole);
            }
        }
        if (urList.Count > 0) TheUserRoleMgr.DeleteUserRole(urList);
        this.CBL_NotInRole.DataBind();
        this.CBL_InRole.DataBind();
        this.cb_InRole.Checked = false;
        this.cb_NotInRole.Checked = false;
        UpdateUserLastModifyDate();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    private void UpdateUserLastModifyDate()
    {
        User user = TheUserMgr.LoadUser(this.lbCode.Text);
        user.LastModifyDate = DateTime.Now;
        user.LastModifyUser = this.CurrentUser;
        TheUserMgr.UpdateUser(user);
    }
}
