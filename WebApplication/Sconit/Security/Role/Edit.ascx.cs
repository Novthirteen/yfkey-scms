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
using com.Sconit.Web;
using com.Sconit.Entity;
using System.Collections.Generic;

public partial class Security_Role_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected string RoleCode
    {
        get
        {
            return (string)ViewState["RoleCode"];
        }
        set
        {
            ViewState["RoleCode"] = value;
        }
    }
    protected bool IsUserPreference
    {
        get
        {
            return (bool)ViewState["IsUserPreference"];
        }
        set
        {
            ViewState["IsUserPreference"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string code, bool isUserPreference)
    {
        this.RoleCode = code;
        this.IsUserPreference = isUserPreference;
        this.ODS_Role.SelectParameters["code"].DefaultValue = this.RoleCode;
        this.ODS_Role.DeleteParameters["code"].DefaultValue = this.RoleCode;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }


    protected void ODS_Role_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {

    }

    protected void ODS_Role_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("Security.Role.UpdateRole.Successfully", RoleCode);
        //UpdateUserLastModifyDate();
    }

    protected void ODS_Role_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Security.Role.DeleteRole.Successfully", RoleCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Security.Role.DeleteRole.Fail", RoleCode);
            e.ExceptionHandled = true;
        }
    }

    protected void FV_Role_DataBound(object sender, EventArgs e)
    {
        Role role = TheRoleMgr.LoadRole(this.RoleCode);
    }

    private void UpdateUserLastModifyDate()
    {
        IList<User> userList = TheUserRoleMgr.GetUsersByRoleCode(this.RoleCode);
        foreach (User user in userList)
        {
            user.LastModifyDate = DateTime.Now;
            user.LastModifyUser = this.CurrentUser;
            TheUserMgr.UpdateUser(user);
        }
    }
}
