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

public partial class MasterData_User_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected string UserCode
    {
        get
        {
            return (string)ViewState["UserCode"];
        }
        set
        {
            ViewState["UserCode"] = value;
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

    public void InitPageParameter(string code, bool isUserPreference)
    {
        this.UserCode = code;
        this.IsUserPreference = isUserPreference;
        this.ODS_User.SelectParameters["code"].DefaultValue = this.UserCode;
        this.ODS_User.DeleteParameters["code"].DefaultValue = this.UserCode;
    }

    protected void FV_User_DataBound(object sender, EventArgs e)
    {
        User user = TheUserMgr.LoadUser(this.UserCode);
        RadioButtonList gender = (RadioButtonList)(this.FV_User.FindControl("rblGender"));
        gender.Items[1].Selected = gender.Items[1].Value == user.Gender ? true : false;
        gender.Items[0].Selected = gender.Items[0].Value == user.Gender ? true : false;
        if(IsUserPreference)
        {
            this.FV_User.FindControl("btnBack").Visible = false;
            this.FV_User.FindControl("btnDelete").Visible = false;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
   
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        this.divPassword.Visible = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.divPassword.Visible = false;
    }

    protected void btnUpdatePassword_Click(object sender, EventArgs e)
    {
        TextBox tbPassword = (TextBox)(this.divPassword.FindControl("tbPassword"));
        User user = TheUserMgr.LoadUser(UserCode);
        user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(tbPassword.Text.Trim(), "MD5");
        user.LastModifyDate = DateTime.Now;
        user.LastModifyUser = this.CurrentUser;
        TheUserMgr.UpdateUser(user);
        ShowSuccessMessage("Security.User.UpdateUserPassword.Successfully", UserCode);
        this.divPassword.Visible = false;
    }


    protected void ODS_User_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        User user = (User)e.InputParameters[0];

        RadioButtonList gender = (RadioButtonList)(this.FV_User.FindControl("rblGender"));
        user.Gender = gender.Items[0].Selected ? gender.Items[0].Value : gender.Items[1].Value;
        user.LastModifyUser = this.CurrentUser;
        user.LastModifyDate = DateTime.Now;

        User oldUser = TheUserMgr.LoadUser(UserCode);
        user.Password = oldUser.Password;
    }

    protected void ODS_User_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("Security.User.UpdateUser.Successfully" ,UserCode);
    }

    protected void ODS_User_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Security.User.DeleteUser.Successfully", UserCode); 
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Security.User.DeleteUser.Fail", UserCode);
            e.ExceptionHandled = true;
        }
    }
}
