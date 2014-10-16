using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Configuration;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using System.Web.Security;
using com.Sconit.Utility;

public partial class UpdatePassword : System.Web.UI.Page
{
    private User CurrentUser
    {
        get
        {
            return (new SessionHelper(this.Page)).CurrentUser;
        }
    }

    private IUserMgr TheUserMgr
    {
        get
        {
            return ServiceLocator.GetService<IUserMgr>("UserMgr.service");
        }
    }

    private IEntityPreferenceMgr TheEntityPreferenceMgr
    {
        get
        {
            return ServiceLocator.GetService<IEntityPreferenceMgr>("EntityPreferenceMgr.service");
        }
    }

    protected string formBg;
    private IList<EntityPreference> entityPerferences;

    protected void Update_Click(object sender, EventArgs e)
    {
        try
        {


            string password = this.tbPassword.Text.Trim();
            string confirmPassword = this.tbConfirmPassword.Text.Trim();
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
                throw new Exception("密码要求(英大小写、数、特殊字符4取3)");
            }

            CurrentUser.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(tbPassword.Text.Trim(), "MD5");
            CurrentUser.LastModifyDate = DateTime.Now;
            CurrentUser.LastModifyUser = this.CurrentUser;
            TheUserMgr.UpdateUser(CurrentUser);
            Response.Redirect("~/Default.aspx");
        }
        catch (Exception ex)
        {
            errorLabel.Text = ex.Message;
        }

   
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (this.Session["Current_User"] == null)
        {
            this.Response.Redirect("~/Logoff.aspx");
        }
        else
        {
            if (!Page.IsPostBack)
            {
                this.Literal2.Text = "用户(" + CurrentUser.Code + ")密码不符合要求，请修改密码.";
            }
        }
    }
}