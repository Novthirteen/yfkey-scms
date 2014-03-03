using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using System.Globalization;
using System.Threading;
using com.Sconit.Entity;

public partial class Nav : System.Web.UI.Page
{
    private bool result = false;

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

    private IUserPreferenceMgr TheUserPreferenceMgr
    {
        get
        {
            return ServiceLocator.GetService<IUserPreferenceMgr>("UserPreferenceMgr.service");
        }
    }

    #region 多语言
    protected override void InitializeCulture()
    {
        if (this.CurrentUser.UserLanguage == null || this.CurrentUser.UserLanguage.Trim() == string.Empty)
        {
            string userLanguage = TheCodeMasterMgr.GetDefaultCodeMaster(BusinessConstants.CODE_MASTER_LANGUAGE).Value;
            this.CurrentUser.UserLanguage = userLanguage;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(userLanguage);

            UserPreference usrpf = new UserPreference();
            usrpf.User = this.CurrentUser;
            usrpf.Code = BusinessConstants.CODE_MASTER_LANGUAGE;
            usrpf.Value = userLanguage;
            TheUserPreferenceMgr.CreateUserPreference(usrpf);
        }
        else
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.CurrentUser.UserLanguage);
        }

        //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
    }
    #endregion

    #region 主题
    protected override void OnPreInit(EventArgs e)
    {
        if (Request.Cookies["ThemePage"] == null)
        {
            this.Page.Theme = "Default";
        }
        else
        {
            this.Page.Theme = Request.Cookies["ThemePage"].Value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["Current_User"] == null)
        {
            this.Response.Redirect("~/Logoff.aspx");
        }
        else
        {
            this.TreeView1.TreeNodeDataBound += new TreeNodeEventHandler(TreeView1_TreeNodeDataBound);
            this.id_hideUser.Value = this.CurrentUser.Code;

            if (!Page.IsPostBack)
            {
                result = true;
            }
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

        if (result)
        {
            try
            {
                this.TreeView1.Nodes[0].Expand();
            }
            catch (Exception)
            {
                //throw;
            }
        }
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (this.Session["Current_User"] == null)
        {
            this.Response.Redirect("~/Logoff.aspx");
        }
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