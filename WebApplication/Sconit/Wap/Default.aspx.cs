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
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;

public partial class Wap_Default : System.Web.UI.Page
{
    private IUserMgr TheUserMgr
    {
        get
        {
            return ServiceLocator.GetService<IUserMgr>("UserMgr.service");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login_Click(object sender, EventArgs e)
    {
        string userCode = this.inputUsername.Value.Trim();
        string password = this.inputPassword.Value.Trim();
        password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");

        if (userCode.Equals(string.Empty))
        {
            lblError.Text = "Please enter your Account!";
            return;
        }

        User user = TheUserMgr.LoadUser(userCode, false, false);

        if (user == null)
        {
            lblError.Text = "Identification failure. Try again!";
        }
        else
        {
            if (password == user.Password)
            {
                this.Session["Current_User"] = TheUserMgr.LoadUser(userCode, true, true);
                Response.Redirect("~/Wap/Main.aspx");
            }
            else
            {
                lblError.Text = "Identification failure. Try again!";
            }
        }
    }
}
