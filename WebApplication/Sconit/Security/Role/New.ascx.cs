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
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using com.Sconit.Entity;

public partial class Security_Role_New : NewModuleBase
{
    private Role role;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;


    public void PageCleanup()
    {
        //((TextBox)(this.FV_Role.FindControl("tbCode"))).Text = string.Empty;
        //((TextBox)(this.FV_Role.FindControl("tbFirstName"))).Text = string.Empty;
        //((TextBox)(this.FV_Role.FindControl("tbLastName"))).Text = string.Empty;
        //((TextBox)(this.FV_Role.FindControl("tbPassword"))).Text = string.Empty;
        //((TextBox)(this.FV_Role.FindControl("tbConfirmPassword"))).Text = string.Empty;
        //((TextBox)(this.FV_Role.FindControl("tbEmail"))).Text = string.Empty;
        //((TextBox)(this.FV_Role.FindControl("tbAddress"))).Text = string.Empty;
        //((TextBox)(this.FV_Role.FindControl("tbPhone"))).Text = string.Empty;
        //((TextBox)(this.FV_Role.FindControl("tbMobilePhone"))).Text = string.Empty;
        //((CheckBox)(this.FV_Role.FindControl("tbIsActive"))).Checked = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Init()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        ((TextBox)(this.FV_Role.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_Role.FindControl("tbDescription"))).Text = string.Empty;
        //((CheckBox)(this.FV_Role.FindControl("tbAllowDelete"))).Checked = false;

    }

    protected void ODS_Role_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        role = (Role)e.InputParameters[0];
    }

    protected void ODS_Role_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(role.Code, e);
            ShowSuccessMessage("Security.Role.AddRole.Successfully", role.Code);
        }
    }

    protected void checkRoleExists(object source, ServerValidateEventArgs args)
    {
        string code = ((TextBox)(this.FV_Role.FindControl("tbCode"))).Text;
        Role user = TheRoleMgr.LoadRole(code);

        if (user != null)
        {
            args.IsValid = false;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }


}
