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
        ((CheckBox)(this.FV_User.FindControl("IsEnforcePolicy"))).Checked = false;
        ((CheckBox)(this.FV_User.FindControl("IsEnforceExpiration"))).Checked = false;
    }

    protected void ODS_User_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        user = (User)e.InputParameters[0];
        RadioButtonList rblGender = (RadioButtonList)(this.FV_User.FindControl("rblGender"));
        string password = ((TextBox)(this.FV_User.FindControl("tbPassword"))).Text.Trim();

        #region   校验密码
        if (user.IsEnforcePolicy)
        {
            if (password.Length < 8)
            {
                throw new Exception("密码长度必须大于等于8，请重新输入。");
            }

            char[] letters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] numbers = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] specials = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '_' };

            bool existsUpLetter = false;
            bool existsLoLetter = false;
            bool existsNumber = false;
            bool existsSpecials = false;
            foreach (var letter in password)
            {
                if (letters.Where(l => l == letter).Count() > 0)
                {
                    existsUpLetter = true;
                    continue;
                }
                if (letters.Where(l => Convert.ToChar(l.ToString().ToLower()) == letter).Count() > 0)
                {
                    existsLoLetter = true;
                    continue;
                }
                if (numbers.Where(l => l == letter).Count() > 0)
                {
                    existsNumber = true;
                    continue;
                }
                if (specials.Where(l => l == letter).Count() > 0)
                {
                    existsSpecials = true;
                    continue;
                }
            }
            int count = 0;
            if (existsUpLetter) count++;
            if (existsLoLetter) count++;
            if (existsNumber) count++;
            if (existsSpecials) count++;

            if (count < 3)
            {
                throw new Exception("密码不符合复杂度要求（英大小写、数、特殊字符4取3）");
            }
        }
        #endregion

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
