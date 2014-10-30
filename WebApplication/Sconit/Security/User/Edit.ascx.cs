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
using com.Sconit.Web;
using com.Sconit.Entity;
using System.Collections.Generic;

public partial class MasterData_User_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected string UserCode
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

    protected bool IsUserPreference
    {
        get
        {
            return (bool)ViewState["IsUserPreference"];
        }
        set
        {
            ViewState["IsUserPreference"] = value;
        }
    }

    public void InitPageParameter(string code, bool isUserPreference)
    {
        this.UserCode = code;
        this.IsUserPreference = isUserPreference;
        this.ODS_User.SelectParameters["code"].DefaultValue = this.UserCode;
        this.ODS_User.DeleteParameters["code"].DefaultValue = this.UserCode;
    }

    protected void FV_User_DataBound(object sender, EventArgs e)
    {
        User user = TheUserMgr.LoadUser(this.UserCode);
        RadioButtonList gender = (RadioButtonList)(this.FV_User.FindControl("rblGender"));
        gender.Items[1].Selected = gender.Items[1].Value == user.Gender ? true : false;
        gender.Items[0].Selected = gender.Items[0].Value == user.Gender ? true : false;
        if(IsUserPreference)
        {
            this.FV_User.FindControl("btnBack").Visible = false;
            this.FV_User.FindControl("btnDelete").Visible = false;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
   
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        this.divPassword.Visible = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.divPassword.Visible = false;
    }

    protected void btnUpdatePassword_Click(object sender, EventArgs e)
    {
        try
        {

            User user = TheUserMgr.LoadUser(UserCode);
            TextBox tbPassword = (TextBox)(this.divPassword.FindControl("tbPassword"));
            if (user.IsEnforcePolicy)
            {
                if (tbPassword.Text.Trim().Length < 8)
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
                foreach (var letter in tbPassword.Text.Trim())
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

            user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(tbPassword.Text.Trim(), "MD5");
            user.LastModifyDate = DateTime.Now;
            user.LastModifyUser = this.CurrentUser;
            TheUserMgr.UpdateUser(user);
            ShowSuccessMessage("Security.User.UpdateUserPassword.Successfully", UserCode);
            this.divPassword.Visible = false;
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }


    protected void ODS_User_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        User user = (User)e.InputParameters[0];

        RadioButtonList gender = (RadioButtonList)(this.FV_User.FindControl("rblGender"));
        user.Gender = gender.Items[0].Selected ? gender.Items[0].Value : gender.Items[1].Value;
        user.LastModifyUser = this.CurrentUser;
        user.LastModifyDate = DateTime.Now;

        User oldUser = TheUserMgr.LoadUser(UserCode);
        user.Password = oldUser.Password;
    }

    protected void ODS_User_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("Security.User.UpdateUser.Successfully" ,UserCode);
    }

    protected void ODS_User_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Security.User.DeleteUser.Successfully", UserCode); 
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Security.User.DeleteUser.Fail", UserCode);
            e.ExceptionHandled = true;
        }
    }
}
