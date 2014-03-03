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
using com.Sconit.Utility;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Reflection;

public partial class Wap_Main : System.Web.UI.Page
{
    private User CurrentUser
    {
        get
        {
            return (new SessionHelper(this.Page)).CurrentUser;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnInit(EventArgs e)
    {
        LoadModule();
        base.OnInit(e);
    }

    private void LoadModule()
    {
        string mid = Request.Params["mid"];

        #region 权限认证
        /*
        if (this.CurrentUser.Code.ToLower() != "su")
        {
            string url = "~/Wap/Main.aspx" + Request.Url.Query;
            if (this.CurrentUser != null)
            {
                if (!this.CurrentUser.HasPermission(url) && url != "~/Wap/Main.aspx")
                {
                    Response.Redirect("~/Wap/Default.aspx");
                    return;
                }
            }
            else
            {
                Response.Redirect("~/Wap/Default.aspx");
                return;
            }
        }
         * */
        #endregion

        if (mid == null)
        {
            phModule.Controls.Add(Page.LoadControl("MainPage/Main.ascx"));
        }
        else
        {
            //加载Web Use Control
            //segment分4段,用__分隔,分别为mid,mp,ac,ap,多个参数之间用'_'
            string mp = string.Empty;
            string act = string.Empty;
            string ap = string.Empty;

            #region 用于命名查询
            if (this.Session["ACT"] != null)
            {
                mid += "__act--" + this.Session["ACT"].ToString();
                Session.Contents.Remove("ACT");
            }
            if (this.Session["AP"] != null)
            {
                mid += "__ap--" + this.Session["AP"].ToString();
                Session.Contents.Remove("AP");
            }
            #endregion

            string[] segment = Regex.Split(mid, "__", RegexOptions.IgnoreCase);
            for (int i = 1; i < segment.Length; i++)
            {
                if (segment[i].Substring(0, segment[i].IndexOf("--")) == "mp")
                {
                    mp = segment[i].Substring(segment[i].IndexOf("--") + 2);
                    continue;
                }
                if (segment[i].Substring(0, segment[i].IndexOf("--")) == "act")
                {
                    act = segment[i].Substring(segment[i].IndexOf("--") + 2);
                    continue;
                }
                if (segment[i].Substring(0, segment[i].IndexOf("--")) == "ap")
                {
                    ap = segment[i].Substring(segment[i].IndexOf("--") + 2);
                    continue;
                }
            }

            LoadModule(segment[0], mp, act, ap);
        }
    }

    private void LoadModule(string mid, string mp, string act, string ap)
    {
        string[] path = mid.Split('.');
        string sourceFile = string.Empty;

        foreach (string p in path)
        {
            sourceFile += p + "\\";
        }
        sourceFile += "Main.ascx";

        IDictionary<string, string> mpDic = new Dictionary<string, string>();
        IDictionary<string, string> apDic = new Dictionary<string, string>();

        if (mp != null && mp != string.Empty)
        {
            string[] splitedMp = mp.Split('_');
            foreach (string para in splitedMp)
            {
                mpDic.Add(para.Split('-')[0], para.Split('-')[1]);
            }
        }

        if (ap != null && ap != string.Empty)
        {
            string[] splitedAp = ap.Split('_');
            foreach (string para in splitedAp)
            {
                apDic.Add(para.Split('-')[0], para.Split('-')[1]);
            }
        }

        UserControl uc = LoadControl(sourceFile, mpDic, act, apDic);
        phModule.Controls.Add(uc);
    }

    private UserControl LoadControl(string userControlPath, params Object[] constructorParameters)
    {
        List<Type> constParamTypes = new List<Type>();
        foreach (object constParam in constructorParameters)
        {
            constParamTypes.Add(constParam.GetType());
        }

        UserControl ctl = Page.LoadControl(userControlPath) as UserControl;

        // Find the relevant constructor
        ConstructorInfo constructor = ctl.GetType().BaseType.GetConstructor(constParamTypes.ToArray());

        //And then call the relevant constructor
        if (constructor == null)
        {
            //Find the relevant constructor in MainModlueBase
            constructor = ctl.GetType().BaseType.BaseType.GetConstructor(constParamTypes.ToArray());

            if (constructor == null)
            {
                throw new MemberAccessException("The requested constructor was not found on : " + ctl.GetType().BaseType.ToString());
            }
        }

        constructor.Invoke(ctl, constructorParameters);

        // Finally return the fully initialized UC
        return ctl;
    }
}
