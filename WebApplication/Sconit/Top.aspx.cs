using System;
using System.Web.UI.HtmlControls;
using com.Sconit.CasClient.Utils;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using System.Threading;
using System.Globalization;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using System.Drawing;
using System.Web;
using System.Collections.Generic;

public partial class Top : System.Web.UI.Page
{
    private IEntityPreferenceMgr TheEntityPreferenceMgr
    {
        get
        {
            return ServiceLocator.GetService<IEntityPreferenceMgr>("EntityPreferenceMgr.service");
        }
    }

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

    private IItemMgr TheItemMgr
    {
        get
        {
            return ServiceLocator.GetService<IItemMgr>("ItemMgr.service");
        }
    }

    protected override void InitializeCulture()
    {
        if (this.CurrentUser.UserLanguage == null || this.CurrentUser.UserLanguage.Trim() == string.Empty)
        {
            string userLanguage = TheCodeMasterMgr.GetDefaultCodeMaster(BusinessConstants.CODE_MASTER_LANGUAGE).Value;
            this.CurrentUser.UserLanguage = userLanguage;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(userLanguage);
        }
        else
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.CurrentUser.UserLanguage);
        }

        //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["OEM"];
        if (cookie != null)
        {
            string imagePath = (cookie.Value == null || cookie.Value.Trim() == string.Empty) ? "Images/" : cookie.Value.Trim();
            this.LeftImage.ImageUrl = imagePath + "Logo_Lit.png";
        }
        this.Info.ForeColor = Color.Black;

        if (this.Session["Current_User"] == null)
        {
            this.Response.Redirect("~/Logoff.aspx");
        }
        this.Info.Text = TheEntityPreferenceMgr.LoadEntityPreference("CompanyName").Value;

        if (Session[AbstractCasModule.CONST_CAS_PRINCIPAL] != null)
        {
            this.logoffHL.NavigateUrl = "https://sso.hoternet.cn:8443/cas/logout?service=http://demo.sconit.com/Logoff.aspx";
            this.logoffHL.Target = "_parent";
        }
        else
        {
            this.logoffHL.NavigateUrl = "~/Logoff.aspx";
            this.logoffHL.Target = "_parent";
        }

        //[{ desc: '愛彼思塑膠-原材料仓库', value: 'ABSS' },{ desc: '上海阿仨希-外购件二楼仓库', value: 'ABXG' }]
        //IList<Item> items = TheItemMgr.GetAllItem();
        //string data = "[";
        //for (int i = 0; i < items.Count; i++)
        //{
        //    Item item = items[i];
        //    string desc = item.Desc1;
        //    desc = desc.Replace("'", "");
        //    data += TextBoxHelper.GenSingleData(desc, item.Code) + (i < (items.Count - 1) ? "," : string.Empty);
        //}
        //data += "]";
        this.data.Value = TheItemMgr.GetCacheAllItemString();
    }
}
