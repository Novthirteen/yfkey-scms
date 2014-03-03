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
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Web;
using System.Collections.Generic;
using com.Sconit.Entity;

public partial class Security_User_UserPermission : ModuleBase
{
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(string code)
    {
        this.tbCategoryType.Text = string.Empty;
        this.tbCategory.Text = string.Empty;
        this.lbCode.Text = code;

        this.tbCategoryType.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE;
        tbCategoryType.DataBind();

        this.ODS_PermissionsInUser.SelectParameters["code"].DefaultValue = string.Empty;
        this.ODS_PermissionsNotInUser.SelectParameters["code"].DefaultValue = string.Empty;
        this.ODS_PermissionsInUser.SelectParameters["categoryCode"].DefaultValue = string.Empty;
        this.ODS_PermissionsNotInUser.SelectParameters["categoryCode"].DefaultValue = string.Empty;
        this.CBL_NotInPermission.DataBind();
        this.CBL_InPermission.DataBind();

    }

    protected void ToInBT_Click(object sender, EventArgs e)
    {
        List<Permission> pList = new List<Permission>();
        foreach (ListItem item in this.CBL_NotInPermission.Items)
        {
            if (item.Selected)
            {
                pList.Add(ThePermissionMgr.LoadPermission(Convert.ToInt32(item.Value)));
            }
        }
        if (pList.Count > 0) TheUserPermissionMgr.CreateUserPermissions(TheUserMgr.LoadUser(this.lbCode.Text), pList);
        this.CBL_NotInPermission.DataBind();
        this.CBL_InPermission.DataBind();
        this.cb_InPermission.Checked = false;
        this.cb_NotInPermission.Checked = false;
    }

    protected void ToOutBT_Click(object sender, EventArgs e)
    {
        List<UserPermission> upList = new List<UserPermission>();
        foreach (ListItem item in this.CBL_InPermission.Items)
        {
            if (item.Selected)
            {
                UserPermission userPermission = TheUserPermissionMgr.LoadUserPermission(this.lbCode.Text, Convert.ToInt32(item.Value));
                upList.Add(userPermission);
            }
        }
        if (upList.Count > 0) TheUserPermissionMgr.DeleteUserPermission(upList);
        this.CBL_NotInPermission.DataBind();
        this.CBL_InPermission.DataBind();
        this.cb_InPermission.Checked = false;
        this.cb_NotInPermission.Checked = false;
        UpdateUserLastModifyDate();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (this.tbCategory.Text.Trim() != string.Empty && this.tbCategoryType.Text.Trim() != string.Empty)
        {
            this.ODS_PermissionsInUser.SelectParameters["code"].DefaultValue = this.lbCode.Text;
            this.ODS_PermissionsNotInUser.SelectParameters["code"].DefaultValue = this.lbCode.Text;
            this.ODS_PermissionsInUser.SelectParameters["categoryCode"].DefaultValue = this.tbCategory.Text;
            this.ODS_PermissionsNotInUser.SelectParameters["categoryCode"].DefaultValue = this.tbCategory.Text;
            this.CBL_NotInPermission.DataBind();
            this.CBL_InPermission.DataBind();
            UpdateUserLastModifyDate();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    private void UpdateUserLastModifyDate()
    {
        User user = TheUserMgr.LoadUser(this.lbCode.Text);
        user.LastModifyDate = DateTime.Now;
        user.LastModifyUser = this.CurrentUser;
        TheUserMgr.UpdateUser(user);
    }
}
