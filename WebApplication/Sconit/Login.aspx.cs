using System;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using System.Net;

public partial class Login : System.Web.UI.Page
{
    protected string PicDate
    {
        get
        {
            return (string)ViewState["PicDate"];
        }
        set
        {
            ViewState["PicDate"] = value;
        }
    }

    protected string LoginImagePath
    {
        get
        {
            return (string)ViewState["LoginImagePath"];
        }
        set
        {
            ViewState["LoginImagePath"] = value;
        }
    }

    private IList<EntityPreference> entityPerferences;

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

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");

        entityPerferences = TheEntityPreferenceMgr.GetAllEntityPreference();
        if (!Page.IsPostBack)
        {
            string LoginImageDir = (from en in entityPerferences where en.Code == "LoginImageDir" select en.Value).FirstOrDefault();
            LoginImageDir = LoginImageDir.EndsWith("/") ? LoginImageDir : LoginImageDir + "/";
            string random;
            if (LoginImageDir.ToLower().StartsWith("http"))//远程图片
            {
                random = ThemeHelper.GetRandomDate();
            }
            else //本地图片,只初始化10月至11月的图片
            {
                DateTime dateTimeMin = Convert.ToDateTime("2009-10-31");
                DateTime dateTimeMax = Convert.ToDateTime("2009-11-30");
                random = ThemeHelper.GetRandomDate(dateTimeMin, dateTimeMax);
                LoginImageDir = LoginImageDir.ToLower().StartsWith("~/") ? LoginImageDir.Substring(2) : LoginImageDir;
            }

            if (Request.Cookies["PicDate"] == null)
            {
                PicDate = random;
            }
            else
            {
                PicDate = Request.Cookies["PicDate"].Value;
            }
            LoginImagePath = LoginImageDir + PicDate + ".jpg";
            HttpCookie randomPicDate = new HttpCookie("RandomPicDate");
            randomPicDate.Value = LoginImagePath;
            Response.Cookies.Add(randomPicDate);

            //LoginPage
            HttpCookie loginPageCookie = new HttpCookie("LoginPage");
            loginPageCookie.Value = "~/Login.aspx";
            Response.Cookies.Add(loginPageCookie);
        }

        this.Title = (from en in entityPerferences where en.Code == "CompanyName" select en.Value).FirstOrDefault();
        this.Documentation.NavigateUrl = (from en in entityPerferences where en.Code == "DocumentationURL" select en.Value).FirstOrDefault();
        this.Wiki.NavigateUrl = (from en in entityPerferences where en.Code == "WikiURL" select en.Value).FirstOrDefault();
        this.Forum.NavigateUrl = (from en in entityPerferences where en.Code == "ForumURL" select en.Value).FirstOrDefault();
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        string ipFilter = (from en in entityPerferences where en.Code == "isEnableIPFilter" select en.Value).FirstOrDefault();
        bool isEnableIPFilter = bool.TryParse(ipFilter, out isEnableIPFilter);

        string ipAdd = Request.UserHostAddress.ToString();
        if (!IPHelper.IsInnerIP(ipAdd) && isEnableIPFilter)
        {
            return;
        }

        string userCode = this.txtUsername.Value.Trim().ToLower();
        string password = this.txtPassword.Value.Trim();
        password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");

        if (userCode.Equals(string.Empty))
        {
            errorLabel.Text = "Please enter your Account!";
            return;
        }

        User user = TheUserMgr.LoadUser(userCode, false, false);

        if (user == null)
        {
            errorLabel.Text = "Identification failure. Try again!";
        }
        else
        {
            if (password == user.Password && user.IsActive)
            {
                this.Session["Current_User"] = TheUserMgr.LoadUser(userCode, true, true);
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                errorLabel.Text = "Identification failure. Try again!";
            }
        }
    }

   

}