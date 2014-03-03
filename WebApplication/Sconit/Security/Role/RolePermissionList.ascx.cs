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
using NHibernate;
using NHibernate.Expression;


public partial class Security_Role_RolePermissionList : ModuleBase
{
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    public void InitPageParameter(string code)
    {
        this.lbCode.Text = code;
        this.GV_List.DataSource = (List<Permission>)(TheRolePermissionMgr.GetPermissionsByRoleCode(code));
        this.GV_List.DataBind();
    }

    protected void GV_List_OnDataBound(object sender, EventArgs e)
    {
        if (((GridView)(sender)).Rows.Count == 0)
            this.lblResult.Visible = true;
        else
            this.lblResult.Visible = false;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
}
