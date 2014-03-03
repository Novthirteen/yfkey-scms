using System;
using System.Web;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;

public partial class Security_Theme : ModuleBase
{
    //todu 重构
    private string tempThemePage, tempThemeFrame, tempLanguage;
    private UserPreference up, upCS;

    protected void Page_Load(object sender, EventArgs e)
    {
        tempThemePage = this.DDL_ThemePage.SelectedValue;
        tempThemeFrame = this.DDL_ThemeFrame.SelectedValue;

        if (TheUserPreferenceMgr.LoadUserPreference(this.CurrentUser.Code, BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE) != null)
        {
            this.DDL_ThemePage.SelectedValue = TheUserPreferenceMgr.LoadUserPreference(this.CurrentUser.Code, BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE).Value;
        }
        else
        {
            this.DDL_ThemePage.SelectedValue = TheCodeMasterMgr.GetDefaultCodeMaster(BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE).Value;
        }

        if (TheUserPreferenceMgr.LoadUserPreference(this.CurrentUser.Code, BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEFRAME) != null)
        {
            this.DDL_ThemeFrame.SelectedValue = TheUserPreferenceMgr.LoadUserPreference(this.CurrentUser.Code, BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEFRAME).Value;
        }
        else
        {
            this.DDL_ThemeFrame.SelectedValue = TheCodeMasterMgr.GetDefaultCodeMaster(BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEFRAME).Value;

        }

        this.DDL_ThemeFrame.DataSource = TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEFRAME);
        this.DDL_ThemeFrame.DataBind();

        this.DDL_ThemePage.DataSource = TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE);
        this.DDL_ThemePage.DataBind();

        //CSModule
        upCS = TheUserPreferenceMgr.LoadUserPreference(this.CurrentUser.Code, BusinessConstants.PERMISSION_CATEGORY_TERMINAL);
        if (!IsPostBack)
        {
            this.ddlCSModule.DataSource = ThePermissionMgr.GetALlPermissionsByCategory(BusinessConstants.PERMISSION_CATEGORY_TERMINAL, this.CurrentUser);
            this.ddlCSModule.DataBind();
            if (upCS != null)
            {
                this.ddlCSModule.Text = upCS.Value == null ? string.Empty : upCS.Value;
            }
        }

        //Language
        up = TheUserPreferenceMgr.LoadUserPreference(this.CurrentUser.Code, BusinessConstants.CODE_MASTER_LANGUAGE);
        tempLanguage = this.ddlLanguage.SelectedValue;
        if (up != null)
        {
            this.ddlLanguage.Text = up.Value == null ? string.Empty : up.Value;
        }
        else
        {
            this.ddlLanguage.Text = TheCodeMasterMgr.GetDefaultCodeMaster(BusinessConstants.CODE_MASTER_LANGUAGE).Value;
        }

        //清除Tab状态
        if (Request.Cookies["TabStatus"] != null && Request.Cookies["TabStatus"].Value == "Theme")
        {
            this.divRefresh.Visible = true;
            Response.Cookies["TabStatus"].Values.Remove("Theme");
            ShowSuccessMessage("Security.UserPreference.Update.Successfully");
        }
        else
        {
            this.divRefresh.Visible = false;
        }


    }

    protected void SaveBt_Click(object sender, EventArgs e)
    {
        //this.DDL_ThemeFrame_SelectedIndexChanged(this, null);
        //this.DDL_ThemePage_SelectedIndexChanged(this, null);
        //this.DDL_Language_SelectedIndexChanged(this, null);
        //this.DDL_CSModule_SelectedIndexChanged(this, null);

        //Response.Redirect("~/Main.aspx?mid=Security.UserPreference");
    }

    protected void DDL_ThemeFrame_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.DDL_ThemeFrame.SelectedValue != tempThemeFrame)
        {
            this.DDL_ThemeFrame.SelectedValue = tempThemeFrame;
            UserPreference usrpf = new UserPreference();
            usrpf.User = this.CurrentUser;
            usrpf.Code = BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEFRAME;
            usrpf.Value = this.DDL_ThemeFrame.SelectedValue;
            TheUserPreferenceMgr.UpdateUserPreference(usrpf);

            HttpCookie cookiePicDate = new HttpCookie("RandomPicDate");
            HttpCookie cookieThemeFrame = new HttpCookie("ThemeFrame");
            switch (this.DDL_ThemeFrame.SelectedValue)
            {
                case "Picture":
                    string picDate = (Request.Cookies["PicDate"] == null) ? Request.Cookies["RandomPicDate"].Value : Request.Cookies["PicDate"].Value;
                    picDate = (picDate == null) ? ThemeHelper.GetRandomDate() : picDate;
                    cookiePicDate.Value = picDate;
                    Response.Cookies.Add(cookiePicDate);
                    cookieThemeFrame.Value = string.Empty;
                    Response.Cookies.Add(cookieThemeFrame);
                    break;
                case "Random":
                    cookieThemeFrame.Value = TheCodeMasterMgr.GetRandomTheme("ThemeFrame");
                    Response.Cookies.Add(cookieThemeFrame);
                    break;
                default:
                    cookieThemeFrame.Value = this.DDL_ThemeFrame.SelectedValue;
                    Response.Cookies.Add(cookieThemeFrame);
                    break;
            }
            this.CurrentUser.UserThemeFrame = this.DDL_ThemeFrame.SelectedValue;
        }
        SavetabStatus();
    }

    protected void DDL_ThemePage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.DDL_ThemePage.SelectedValue != tempThemePage)
        {
            this.DDL_ThemePage.SelectedValue = tempThemePage;
            UserPreference pf = new UserPreference();
            pf.User = this.CurrentUser;
            pf.Code = BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE;
            pf.Value = this.DDL_ThemePage.SelectedValue;
            if (this.CurrentUser.UserThemePage == string.Empty || this.CurrentUser.UserThemePage == null)
            {
                try
                {
                    TheUserPreferenceMgr.CreateUserPreference(pf);
                }
                catch (Exception)
                {
                    TheUserPreferenceMgr.UpdateUserPreference(pf);
                }

            }
            else
            {
                TheUserPreferenceMgr.UpdateUserPreference(pf);
            }

            if (this.DDL_ThemePage.SelectedValue != BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE_RANDOM)
            {
                HttpCookie cookieThemePage = new HttpCookie("ThemePage");
                cookieThemePage.Value = this.DDL_ThemePage.SelectedValue;
                Response.Cookies.Add(cookieThemePage);
            }
            this.CurrentUser.UserThemePage = this.DDL_ThemePage.SelectedValue;
        }
        SavetabStatus();
    }

    protected void DDL_Language_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (up == null)
        {
            up = new UserPreference();
            up.User = this.CurrentUser;
            up.Code = BusinessConstants.CODE_MASTER_LANGUAGE;
            up.Value = tempLanguage;
            TheUserPreferenceMgr.CreateUserPreference(up);
            this.ddlLanguage.Text = tempLanguage;
        }
        else
        {
            up.Value = tempLanguage;
            TheUserPreferenceMgr.UpdateUserPreference(up);
            this.ddlLanguage.Text = tempLanguage;
        }
        this.CurrentUser.UserLanguage = tempLanguage;
        SavetabStatus();
    }

    protected void DDL_CSModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (upCS == null)
        {
            upCS = new UserPreference();
            upCS.User = this.CurrentUser;
            upCS.Code = BusinessConstants.PERMISSION_CATEGORY_TERMINAL;
            upCS.Value = this.ddlCSModule.SelectedValue;
            TheUserPreferenceMgr.CreateUserPreference(upCS);
        }
        else
        {
            upCS.Value = this.ddlCSModule.SelectedValue;
            TheUserPreferenceMgr.UpdateUserPreference(upCS);
        }
        ShowSuccessMessage("Security.UserPreference.Update.Successfully");
        //SavetabStatus();
    }

    private void SavetabStatus()
    {
        //记录Tab状态,50秒钟后失效
        HttpCookie tabStatus = new HttpCookie("TabStatus");
        tabStatus.Value = "Theme";
        tabStatus.Expires = DateTime.Now.AddSeconds(50);
        Response.Cookies.Add(tabStatus);
        Response.Redirect("~/Main.aspx?mid=Security.UserPreference");
    }
}
