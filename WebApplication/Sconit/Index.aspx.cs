using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Configuration;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using System.Web.Security;
using com.Sconit.Utility;

public partial class Index : System.Web.UI.Page
{
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

    protected void Login_Click(object sender, EventArgs e)
    {
        string ipFilter = (from en in entityPerferences where en.Code == "isEnableIPFilter" select en.Value).FirstOrDefault();
        bool isEnableIPFilter = bool.TryParse(ipFilter, out isEnableIPFilter);

        string ipAdd = Request.UserHostAddress.ToString();
        if (isEnableIPFilter && !IPHelper.IsInnerIP(ipAdd))
        {
            return;
        }

        string userCode = this.txtUsername.Value.Trim().ToLower();
        string password = this.txtPassword.Value.Trim();
        string tempPassword = password;
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
                #region   检查密码
                if (tempPassword.Length < 8)
                {
                    Response.Redirect("~/UpdatePassword.aspx");
                }

                char[] letters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
                char[] numbers = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                char[] specials = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '_' };

                bool existsUpLetter = false;
                bool existsLoLetter = false;
                bool existsNumber = false;
                bool existsSpecials = false;
                foreach (var letter in tempPassword)
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
                    Response.Redirect("~/UpdatePassword.aspx");
                }
                #endregion
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                errorLabel.Text = "Identification failure. Try again!";
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        entityPerferences = TheEntityPreferenceMgr.GetAllEntityPreference();
        this.Literal1.Text = Resources.Language.IndexWelcome;
        this.Title = (from en in entityPerferences where en.Code == "CompanyName" select en.Value).FirstOrDefault();
        this.Documentation.NavigateUrl = (from en in entityPerferences where en.Code == "DocumentationURL" select en.Value).FirstOrDefault();
        this.Wiki.NavigateUrl = (from en in entityPerferences where en.Code == "WikiURL" select en.Value).FirstOrDefault();
        this.Forum.NavigateUrl = (from en in entityPerferences where en.Code == "ForumURL" select en.Value).FirstOrDefault();
        string companyCode = (from en in entityPerferences where en.Code == "CompanyCode" select en.Value).FirstOrDefault();
        companyCode = (companyCode == null || companyCode.Trim() == string.Empty) ? string.Empty : (companyCode + "/");
        string imagePath = "Images/OEM/" + companyCode;

        this.imgLogo.ImageUrl = imagePath + "Logo.png";
        this.formBg = imagePath + "bg_form.png";

        if (!Page.IsPostBack)
        {
            string LoginImagePath = imagePath + "Banner.jpg";
            HttpCookie randomPicDate = new HttpCookie("RandomPicDate");
            randomPicDate.Value = LoginImagePath;
            Response.Cookies.Add(randomPicDate);
            //OEM
            string OEM = imagePath;
            HttpCookie httpCookie = new HttpCookie("OEM");
            httpCookie.Value = OEM;
            Response.Cookies.Add(httpCookie);
            //LoginPage
            HttpCookie loginPageCookie = new HttpCookie("LoginPage");
            loginPageCookie.Value = "~/Index.aspx";
            Response.Cookies.Add(loginPageCookie);
        }
    }
}