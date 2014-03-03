using System;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Collections;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;

public partial class _Default : System.Web.UI.Page
{
    private User CurrentUser
    {
        get
        {
            return (new SessionHelper(this.Page)).CurrentUser;
        }
    }

    private ICodeMasterMgr TheCodeMasterMgr
    {
        get
        {
            return ServiceLocator.GetService<ICodeMasterMgr>("CodeMasterMgr.service");
        }
    }

    private IEntityPreferenceMgr TheEntityPreferenceMgr
    {
        get
        {
            return ServiceLocator.GetService<IEntityPreferenceMgr>("EntityPreferenceMgr.service");
        }
    }

    private IFavoritesMgr TheFavoritesMgr
    {
        get
        {
            return ServiceLocator.GetService<IFavoritesMgr>("FavoritesMgr.service");
        }
    }

    private IUserPreferenceMgr TheUserPreferenceMgr
    {
        get
        {
            return ServiceLocator.GetService<IUserPreferenceMgr>("UserPreferenceMgr.service");
        }
    }

    protected string url = "Main.aspx";
    protected int leftdownHeight = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["Current_User"] == null)
        {
            this.Response.Redirect("~/Logoff.aspx");
        }
        else
        {
            this.Title = TheEntityPreferenceMgr.LoadEntityPreference("CompanyName").Value;

            if (!Page.IsPostBack)
            {
                string ThemePage = string.Empty;

                HttpCookie cookieThemePage = new HttpCookie("ThemePage");
                if (this.CurrentUser.UserThemePage == null || this.CurrentUser.UserThemePage.Trim() == string.Empty)
                {
                    cookieThemePage.Value = TheCodeMasterMgr.GetDefaultCodeMaster(BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE).Value;
                    Response.Cookies.Add(cookieThemePage);

                    this.CurrentUser.UserThemePage = cookieThemePage.Value;

                    UserPreference usrpf = new UserPreference();
                    usrpf.User = this.CurrentUser;
                    usrpf.Code = BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE;
                    usrpf.Value = cookieThemePage.Value;
                    TheUserPreferenceMgr.CreateUserPreference(usrpf);
                }
                else
                {
                    UserPreference userPreferenceThemePage = TheUserPreferenceMgr.LoadUserPreference(this.CurrentUser.Code, "ThemePage");
                    if (userPreferenceThemePage != null && userPreferenceThemePage.Value == BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE_RANDOM)
                    {
                        ThemePage = TheCodeMasterMgr.GetRandomTheme(BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE);
                    }
                    else
                    {
                        ThemePage = userPreferenceThemePage.Value;
                    }
                    cookieThemePage.Value = ThemePage;
                    Response.Cookies.Add(cookieThemePage);

                }

                #region 随机框架主题
                HttpCookie cookieThemeFrame = new HttpCookie("ThemeFrame");
                if (this.CurrentUser.UserThemeFrame == null || this.CurrentUser.UserThemeFrame.Trim() == string.Empty)
                {
                    cookieThemeFrame.Value = string.Empty;
                    Response.Cookies.Add(cookieThemeFrame);

                    this.CurrentUser.UserThemeFrame = TheCodeMasterMgr.GetDefaultCodeMaster(BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEFRAME).Value;

                    UserPreference usrpf = new UserPreference();
                    usrpf.User = this.CurrentUser;
                    usrpf.Code = BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEFRAME;
                    usrpf.Value = this.CurrentUser.UserThemeFrame;
                    TheUserPreferenceMgr.CreateUserPreference(usrpf);
                }
                else
                {
                    string themeFrame = TheUserPreferenceMgr.LoadUserPreference(this.CurrentUser.Code, "ThemeFrame").Value;
                    switch (themeFrame)
                    {
                        case "Picture":
                            cookieThemeFrame.Value = string.Empty;
                            Response.Cookies.Add(cookieThemeFrame);
                            break;
                        case "Random":
                            cookieThemeFrame.Value = TheCodeMasterMgr.GetRandomTheme("ThemeFrame");
                            Response.Cookies.Add(cookieThemeFrame);
                            break;
                        default:
                            cookieThemeFrame.Value = themeFrame;
                            Response.Cookies.Add(cookieThemeFrame);
                            break;
                    }
                }
                #endregion
            }

            //确定MainFrame的页面为退出前的页面
            if (Request.Params.Get("rightFrameUrl") == null)
            {
                IList<Favorites> listFavorites = TheFavoritesMgr.GetFavorites(this.CurrentUser.Code, "History");

                if (listFavorites.Count != 0)
                {
                    Favorites favorite = listFavorites[0];
                    url = "Main.aspx" + favorite.PageUrl;
                }
                else
                {
                    url = "Main.aspx?mid=Security.UserPreference";
                }
            }
            else
            {
                url = Request.Params.Get("rightFrameUrl");
            }

        }
    }

}