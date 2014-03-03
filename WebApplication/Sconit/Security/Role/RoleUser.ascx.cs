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

public partial class Security_Role_RoleUser : ModuleBase
{
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(string code)
    {
       
        this.ODS_UsersInRole.SelectParameters["code"].DefaultValue = code;
        this.ODS_UsersNotInRole.SelectParameters["code"].DefaultValue = code;
        this.lbCode.Text = code;
    }

    protected void ToInBT_Click(object sender, EventArgs e)
    {
        List<User> uList = new List<User>();
        foreach (ListItem item in this.CBL_NotInUser.Items)
        {
            if (item.Selected)
            {
                uList.Add(TheUserMgr.LoadUser(item.Value));
            }
        }
        if (uList.Count > 0)
        {
            TheUserRoleMgr.CreateUserRoles(uList, TheRoleMgr.LoadRole(this.lbCode.Text));
            UpdateUserLastModifyDate(uList);
        }
        this.CBL_NotInUser.DataBind();
        this.CBL_InUser.DataBind();
        this.cb_InRole.Checked = false;
        this.cb_NotInRole.Checked = false;
    }

    protected void ToOutBT_Click(object sender, EventArgs e)
    {
        List<UserRole> urList = new List<UserRole>();
        List<User> uList = new List<User>();
        foreach (ListItem item in this.CBL_InUser.Items)
        {
            if (item.Selected)
            {
                UserRole userRole = TheUserRoleMgr.LoadUserRole(item.Value, this.lbCode.Text);
                urList.Add(userRole);
                uList.Add(userRole.User);
            }
        }
        if (urList.Count > 0) 
        {
            TheUserRoleMgr.DeleteUserRole(urList);
            UpdateUserLastModifyDate(uList);
        }
        this.CBL_NotInUser.DataBind();
        this.CBL_InUser.DataBind();
        this.cb_InRole.Checked = false;
        this.cb_NotInRole.Checked = false;
    }

    protected void CBL_NotInUser_DataBinding(object sender, EventArgs e)
    {
        //this.CBL_InUser.DataTextFormatString="Item Value:[{0}]";
    }
    protected void CBL_InUser_DataBinding(object sender, EventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    private void UpdateUserLastModifyDate(IList<User> userList)
    {
        foreach (User user in userList)
        {
            user.LastModifyDate = DateTime.Now;
            user.LastModifyUser = this.CurrentUser;
            TheUserMgr.UpdateUser(user);
        }
    }
}
