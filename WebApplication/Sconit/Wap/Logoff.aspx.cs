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

public partial class Wap_Logoff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (FormsAuthentication.LoginUrl.Contains("Logon.aspx"))
        //{
        //    FormsAuthentication.SignOut(); Response.Redirect("logon.aspx", true);
        //}

        this.Session.Clear();
        this.Session.Abandon();
        //if (FormsAuthentication.LoginUrl.Contains("LoginCAS.aspx"))
        //{
        //    FormsAuthentication.SignOut(); Response.Redirect("LoginCAS.aspx", true);
        //}
        this.Response.Redirect("~/Wap/Default.aspx");
    }
}
