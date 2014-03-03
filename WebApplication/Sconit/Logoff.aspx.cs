using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Logoff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Session.Clear();
        this.Session.Abandon();

        if (FormsAuthentication.LoginUrl.Contains("Logon.aspx"))
        {
            FormsAuthentication.SignOut();
            Response.Redirect("logon.aspx", true);
        }

        if (FormsAuthentication.LoginUrl.Contains("LoginCAS.aspx"))
        {
            FormsAuthentication.SignOut();
            Response.Redirect("LoginCAS.aspx", true);
        }

        HttpCookie cookie = Request.Cookies["LoginPage"];
        if (cookie != null && cookie.Value != null)
        {
            string str = cookie.Value;
            if (str.StartsWith("~/") || str.StartsWith("http"))
            {
                this.Response.Redirect(cookie.Value.ToString());
            }
        }
        this.Response.Redirect("~/Login.aspx");
        //this.Response.Redirect("~/Index.aspx");
    }
}
