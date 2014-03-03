using System;
using System.Web.UI.HtmlControls;

public partial class Lefttop : System.Web.UI.Page
{
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
        if (this.Session["Current_User"] != null)
            this.SUser.Text = ((com.Sconit.Entity.MasterData.UserBase)(this.Session["Current_User"])).Code;
        else
            this.Response.Redirect("~/Logoff.aspx");
    }
}
