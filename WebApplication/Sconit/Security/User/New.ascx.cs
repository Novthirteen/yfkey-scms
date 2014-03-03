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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using com.Sconit.Entity;

public partial class MasterData_User_New : NewModuleBase
{
    private User user;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    public void PageCleanup()
    {
        ((TextBox)(this.FV_User.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_User.FindControl("tbFirstName"))).Text = string.Empty;
        ((TextBox)(this.FV_User.FindControl("tbLastName"))).Text = string.Empty;
        ((TextBox)(this.FV_User.FindControl("tbPassword"))).Text = string.Empty;
        ((TextBox)(this.FV_User.FindControl("tbConfirmPassword"))).Text = string.Empty;
        ((TextBox)(this.FV_User.FindControl("tbEmail"))).Text = string.Empty;
        ((TextBox)(this.FV_User.FindControl("tbAddress"))).Text = string.Empty;
        ((TextBox)(this.FV_User.FindControl("tbPhone"))).Text = string.Empty;
        ((TextBox)(this.FV_User.FindControl("tbMobilePhone"))).Text = string.Empty;
        ((CheckBox)(this.FV_User.FindControl("tbIsActive"))).Checked = false;
    }

    protected void ODS_User_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        user = (User)e.InputParameters[0];
        RadioButtonList rblGender = (RadioButtonList)(this.FV_User.FindControl("rblGender"));
        string password = ((TextBox)(this.FV_User.FindControl("tbPassword"))).Text.Trim();
        password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
        user.Code = user.Code.ToLower().Trim();
        user.Gender = rblGender.SelectedValue.Trim();
        user.Password = password;
        user.LastModifyDate = DateTime.Now;
        user.LastModifyUser = this.CurrentUser;
    }

    protected void ODS_User_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        UserRole userRole = new UserRole();
        userRole.Role = TheRoleMgr.LoadRole("everyone");
        userRole.User = user;
        TheUserRoleMgr.CreateUserRole(userRole);
        if (CreateEvent != null)
        {
            CreateEvent(user.Code, e);
            ShowSuccessMessage("Security.User.AddUser.Successfully", user.Code);
        }
    }

    protected void checkUserExists(object source, ServerValidateEventArgs args)
    {
        string code = ((TextBox)(this.FV_User.FindControl("tbCode"))).Text.ToLower();
        User user = TheUserMgr.LoadUser(code);

        if (user != null)
        {
            args.IsValid = false;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
}
