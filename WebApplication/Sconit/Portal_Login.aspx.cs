using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity.MasterData;
using System.Web.Security;
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;
using System.Threading;
using System.Globalization;
using System.IO;
using com.Sconit.Entity.Exception;

public partial class Portal_Login : System.Web.UI.Page
{
    private string language = "zh-CN";

    private User CurrentUser
    {
        get { return (new SessionHelper(this.Page)).CurrentUser; }
    }

    private IUserMgr TheUserMgr
    {
        get { return ServiceLocator.GetService<IUserMgr>("UserMgr.service"); }
    }

    private IEntityPreferenceMgr TheEntityPreferenceMgr
    {
        get { return ServiceLocator.GetService<IEntityPreferenceMgr>("EntityPreferenceMgr.service"); }
    }

    private IPermissionMgr ThePermissionMgr
    {
        get { return ServiceLocator.GetService<IPermissionMgr>("PermissionMgr.service"); }
    }

    private IUserPreferenceMgr TheUserPreferenceMgr
    {
        get { return ServiceLocator.GetService<IUserPreferenceMgr>("UserPreferenceMgr.service"); }
    }
    #region 多语言
    protected override void InitializeCulture()
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
    }
    #endregion
    #region 主题
    protected override void OnPreInit(EventArgs e)
    {
        this.Page.Theme = "Default";
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            //string entityCode = TheEntityPreferenceMgr.LoadEntityPreference("CompanyCode").Value;
            string entityCode = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_PORTAL_PARAM).Value;

            // read config
            string companyCode = string.Empty;
            string redirectUrl = string.Empty;
            string urlLogin = string.Empty;
            string userCode = string.Empty;
            string password = string.Empty;
            string partyCode = string.Empty;

            //string userCode = "su";
            //string password = "demo";
            //string partyCode = "s0000208";
            //?105=201027&language=en&redirectUrl=Main.aspx%3Fmid%3DOrder.OrderIssue__mp--ModuleType-SupplierDistribution_ModuleSubType-Nml&userCode=SupplyUser&password=supplyuser%40sconit.com
            string[] keys = Request.Params.AllKeys;
            foreach (string key in keys)
            {
                if (key.EndsWith(entityCode))
                {
                    partyCode = Request.Params.Get(key).ToLower();
                }
                else if (key.EndsWith("languageId"))
                {
                    language = Request.Params.Get(key);
                    if (language=="zh_CN")
                    {
                        language = "zh-CN";
                    }
                    else if (language == "en_US")
                    {
                        language = "en";
                    }
                    InitializeCulture();
                }
                else if (key.EndsWith("feature"))
                {
                    redirectUrl = Request.Params.Get(key);
                }
                else if (key.EndsWith("urlLogin"))
                {
                    urlLogin = Request.Params.Get(key);
                }
                else if (key.EndsWith("userid"))
                {
                    userCode = Request.Params.Get(key).ToLower();
                }
                else if (key.EndsWith("password"))
                {
                    password = Request.Params.Get(key);
                }
            }
            //LoginPage
            HttpCookie loginPageCookie = new HttpCookie("LoginPage");
            loginPageCookie.Value = urlLogin;
            Response.Cookies.Add(loginPageCookie);

            ////test
            //userCode = "SupplyUser";
            //password = "sconit.yfkey";
            //partyCode = "ADKJ";

            //this.message.Text = userCode + "|" + password + "|" + partyCode;
            //return;

            if (userCode != null && userCode.Trim() != string.Empty)
            {
                if ((userCode + password).ToLower() == @"supplyusersconit.yfkey")
                {
                    User user = TheUserMgr.CheckAndLoadUser(partyCode.ToLower());

                    bool isExistUserPreferenceLanguage = false;
                    if (user != null && user.UserPreferences != null)
                    {
                        foreach (UserPreference userPreference in user.UserPreferences)
                        {
                            if (userPreference.Code == BusinessConstants.CODE_MASTER_LANGUAGE)
                            {
                                userPreference.Value = language;
                                isExistUserPreferenceLanguage = true;
                            }
                        }
                    }
                    if (!isExistUserPreferenceLanguage || user.UserPreferences == null)
                    {
                        UserPreference up = new UserPreference();
                        up.Code = BusinessConstants.CODE_MASTER_LANGUAGE;
                        up.Value = language;
                        up.User = user;
                        user.UserPreferences.Add(up);
                    }
                    this.Session["Current_User"] = user;
                    this.Session["Hiddensmp"] = "Hiddensmp";
                    this.TreeView1.TreeNodeDataBound += new TreeNodeEventHandler(TreeView1_TreeNodeDataBound);
                    return;
                }
            }
            this.Response.Redirect("about:blank");
        }
        catch (BusinessErrorException ex)
        {
            this.Response.Redirect("about:blank");
        }
    }

    protected void TreeView1_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        TreeNode treeNode = e.Node;
        SiteMapNode siteMapNode = (SiteMapNode)treeNode.DataItem;

        if (siteMapNode.Url == string.Empty)
        {
            e.Node.SelectAction = TreeNodeSelectAction.Expand;
        }

        #region 生成Icon
        string ImageIco = "~/Images/Nav/" + siteMapNode.Description + ".png";
        if (File.Exists(Server.MapPath(ImageIco)))
        {
            treeNode.ImageUrl = ImageIco;
        }
        else
        {
            treeNode.ImageUrl = "~/Images/Nav/Default.png";
        }
        #endregion

        #region 判断权限
        if (this.CurrentUser.Code.ToLower() == "su" || HasPermission(siteMapNode))
        {
            treeNode.ToolTip = treeNode.Text;
            treeNode.Text = "&nbsp;" + treeNode.Text;
        }
        else
        {
            try
            {
                treeNode.Parent.ChildNodes.Remove(treeNode);
            }
            catch (Exception)
            {
                this.TreeView1.Nodes.Remove(treeNode);
            }
        }
        #endregion
    }

    private bool HasPermission(SiteMapNode siteMapNode)
    {
        string url = siteMapNode.Url.Trim();
        url = url.Contains("Main.aspx") ? ("~/" + url.Remove(0, siteMapNode.Url.IndexOf("Main.aspx"))) : string.Empty;

        if (this.CurrentUser.HasPermission(url))
        {
            return true;
        }
        else
        {
            if (siteMapNode.ChildNodes != null && siteMapNode.ChildNodes.Count > 0)
            {
                foreach (SiteMapNode childSiteMapNode in siteMapNode.ChildNodes)
                {
                    if (HasPermission(childSiteMapNode))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
